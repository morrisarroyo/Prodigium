using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour {

    Transform target;    // Target the player follows

    NavMeshAgent agent;  // Component attached to player to navigate using NavMesh

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update () {
        if (target != null) {
            agent.SetDestination(target.position);
            FaceTarget();
        }
    }
	
    // Move player to destination set by a raycast in another script
	public void MoveToPoint (Vector3 point) {
        agent.SetDestination(point);
    }

    // Tracks moving target
    public void FollowTarget (Interactable newTarget) {
        // Player stops outside stopping distance
        agent.stoppingDistance = newTarget.radius * .8f;

        // Does not need to update automatic rotation
        agent.updateRotation = false;
        
        // Sets target as interactable
        target = newTarget.transform;
    }

    public void StopFollowingTarget () {
        // Resetting stopping distance
        agent.stoppingDistance = 0f;
        
        // No automatic rotation so rotates to face target
        agent.updateRotation = true;
        
        // Stops setting target
        target = null;
    }
    
    // Always face target regardless of stopping distance
    public void FaceTarget() {
        // Find direction from player to target
        Vector3 direction = (target.position - transform.position).normalized;
        
        // Find rotation by turning direction into rotation through LookRotation and store in quaternion
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        
        // Smoothly interpolate towards the rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

    }
}
