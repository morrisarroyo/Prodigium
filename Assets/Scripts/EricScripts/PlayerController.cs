using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    // Used to set focus
    public Interactable focus;

    // Used to specify a layer
    public LayerMask movementMask;

    // Camera variable
    Camera cam;

    // Used to move player
    PlayerMotor motor;

    // Use this for initialization
    void Start() {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update() {
        //On LEFT mouse click
        if (Input.GetMouseButtonUp(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Moves player to what the raycast hits without focusing on any objects
            if (Physics.Raycast(ray, out hit, 100, movementMask)) {
                // Move to where we hit
                motor.MoveToPoint(hit.point);
                
                // Stop focusing any objects
                RemoveFocus();
            }
        }

        // On RIGHT mouse click
        if (Input.GetMouseButtonUp(1)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Moves player to what the raycast hits when focusing on an object
            if (Physics.Raycast(ray, out hit, 100)) {
                //C heck if we hit interactable
                Interactable interactable = hit.collider.GetComponent<Interactable>(); 
               
                // Set focus if the player finds an interactable 
                if (interactable != null) {
                    SetFocus(interactable);
                }
            }
        }
    }

    // Sets focus to interactable
    void SetFocus (Interactable newFocus) {
        // If focus is already set then defocus that first
        if (newFocus != focus) {
            if (focus != null) {
                focus.onDefocused();
            }

            focus = newFocus;
            motor.FollowTarget(newFocus);
        }
        newFocus.onFocused(transform);

    }

    // Removes focus from interactable
    void RemoveFocus () {
        if (focus != null) {
            focus.onDefocused();
        }

        focus = null;
        motor.StopFollowingTarget();
    }
}
