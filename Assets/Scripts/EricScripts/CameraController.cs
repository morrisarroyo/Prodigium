﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;          // Keeps track of player

    public Vector3 offset;            // Set distance from the player


    public float zoomSpeed = 4f;      // Mouse scroll zoom speed
    public float minZoom = 5f;        // Minimum mouse scroll zoom distance
    public float maxZoom = 15f;       // Maximum mouse scroll zoom distance
    public float pitch = 2f;          // Set rotation of up and down
    public float yawSpeed = 100f;     // Set rotation of left and right based on player


    private float currentZoom = 10f;  // Initial zoom setting

    private float currentYaw = 0f;    // Initial yaw setting

    void Update() {
        // Sets what the zoom will be based on mouse scroll
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        
        // Boundaries for minimum and maximum zoom
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        // Sets what the rotation will be around player using left/right arrows or A/D keys
        currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
    }
	
	void LateUpdate () {
        // Zooms in and out based on mouse scroll
        transform.position = target.position - offset * currentZoom;

        // Sets the up and down angle the camera looks at the player with 
        transform.LookAt(target.position + Vector3.up * pitch);

        // Rotate around player using left/right arrows or A/D keys
        transform.RotateAround(target.position, Vector3.up, currentYaw);
	}
}