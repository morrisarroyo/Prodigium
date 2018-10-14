using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerMotor))]
public class BaseCharacter : BaseCreature
{
    //public int health = 1000;           // Health of player
    //public int basicAttackDamage = 20;  // Damage per hit of basic attack
    // public float movementSpeed = 4f;    // Speed the player moves at
    public bool isWalking = false;      // Controls whether the player is walking

    public Interactable focus;          // Used to set focus

    public LayerMask movementMask;      // Used to specify a layer

    Camera cam;                         // Camera variable

    PlayerMotor motor;                  // Used to move player

    Animator anim;                      // Used to animate player

    private Vector3 targetPosition;

    // Use this for initialization
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
        anim = GetComponent<Animator>();
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        BasicAttack();
        Die();
    }

    protected override void Move()
    {
        //On LEFT mouse click
        if (Input.GetMouseButtonUp(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Moves player to what the raycast hits without focusing on any objects
            if (Physics.Raycast(ray, out hit, 100, movementMask)) {
                // Move to where we hit
                motor.MoveToPoint(hit.point);
                //targetPosition = hit.point;
                // Stop focusing any objects
                RemoveFocus();
                targetPosition = transform.position;

                //isWalking = true;
            }

        }

        // On RIGHT mouse click
        if (Input.GetMouseButtonUp(1)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Moves player to what the raycast hits when focusing on an object
            if (Physics.Raycast(ray, out hit, 100)) {
                //Check if we hit interactable
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                targetPosition = transform.position;
                // Set focus if the player finds an interactable 
                if (interactable != null) {
                    SetFocus(interactable);
                }

               // isWalking = true;
            }

        }

        Debug.Log(targetPosition);


       /* if (transform.position == targetPosition) {
            isWalking = false;
        }

        anim.SetBool("walking", isWalking); */
    }

    protected override void BasicAttack()
    {
        if(Input.GetKeyUp(KeyCode.W)) {
            anim.Play("Attack1");
            anim.Play("Stand");
            //anim.SetTrigger(Animator.StringToHash("attacking"));
        }
    }

    protected void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    protected override void Die()
    {
        if (Input.GetKeyUp(KeyCode.S)) {
            anim.SetTrigger(Animator.StringToHash("dead"));
        }
    }

    // Sets focus to interactable
    void SetFocus(Interactable newFocus)
    {
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
    void RemoveFocus()
    {
        if (focus != null) {
            focus.onDefocused();
        }

        focus = null;
        motor.StopFollowingTarget();
    }

}
