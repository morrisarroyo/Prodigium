using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderIceProdigy : BaseCharacter {

    public LayerMask movementMask;

    Camera cam;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //Moves player to what the raycast hits
            if (Physics.Raycast(ray, out hit, 100, movementMask)) {
                Debug.Log("We hit " + hit.collider.name + " " + hit.point);      

                //Move player to what we hit
                
                //Stop focusing any objects
            }
        }
	}
}
