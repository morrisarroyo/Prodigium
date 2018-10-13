using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]

public class Zombie : BaseCreature {

	protected int health = 100;
	protected int basicAttackDamage = 10;
	protected int movementSpeed = 5;
	public bool walking;

	Transform target;
	Animator anim;
	NavMeshAgent nav;

	public float lookRadius;
	public float attackRadius;

	private float nextAttack;
	public float attackSpeed = 2f;

	// Use this for initialization
	void Awake ()
	{
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		nav = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();
		Physics.OverlapSphere (transform.position, 5f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		anim.SetBool("walking", walking);

		if (!walking) {
			anim.SetBool ("idle", true);
		} else {
			anim.SetBool ("idle", false);
		}
		float distance = Vector3.Distance (transform.position, target.position);
		if (distance <= lookRadius) {
			Move ();
		}

	}
		
	protected override void Move()
	{
		nav.destination = target.position;
		if (!nav.pathPending && nav.remainingDistance > attackRadius) {
			nav.isStopped = false;
			walking = true;
		} else if (!nav.pathPending && nav.remainingDistance <= attackRadius) {
			BasicAttack ();
		}
	}
		
	protected void TakeDamage(int damage)
	{
		base.TakeDamage (damage);
	}

	protected override void BasicAttack()
	{
		transform.LookAt (target);
		if (Time.time > nextAttack) {
			nextAttack = Time.time + attackSpeed;
			anim.SetTrigger ("attack");
		}
		nav.isStopped = true;
		walking = false;
	}

	protected override void Die ()
	{
		
	}

	void OnDrawGizmos(){
		Handles.color = Color.yellow;
		Handles.DrawWireArc (transform.position+new Vector3(0,0.2f,0), transform.up, transform.right, 360, lookRadius);
		Handles.color = Color.red;
		Handles.DrawWireArc (transform.position+new Vector3(0,0.2f,0), transform.up, transform.right, 360, attackRadius);
	}
}
