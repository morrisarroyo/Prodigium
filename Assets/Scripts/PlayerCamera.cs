using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

	[Header("Target")]
	public Transform target;
	[Header("Distance Settings")]
	public float distance = 5f;
	public Vector3 offset;
	public float minDistance = 1f;
	public float maxDistance = 7f;
	[Header("Speed Settings")]
	public float smoothSpeed = 5f;
	public float scrollSensitivity = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (!target) {
			print ("NO TARGET SET FOR CAMERA");
			return;
		}

		float num = Input.GetAxis ("Mouse ScrollWheel");
		distance -= num * scrollSensitivity;
		distance = Mathf.Clamp (distance, minDistance, maxDistance);

		Vector3 pos = target.position + offset;
		pos -= transform.forward * distance;

		transform.position = Vector3.Lerp (transform.position, pos, smoothSpeed * Time.deltaTime);
	}
}
