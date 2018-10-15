using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]

public class ZombieBase : BaseCreature {

	public int health = 100;
	public int basicAttackDamage = 10;
	public int movementSpeed = 1;

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
        transform.rotation = Random.rotation;
        nav.speed = Random.Range((float)(movementSpeed - movementSpeed * .2), (float)(movementSpeed + movementSpeed * .2));
		Physics.OverlapSphere (transform.position, 5f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		float distance = Vector3.Distance (transform.position, target.position);
		if (!anim.GetCurrentAnimatorStateInfo(0).IsName("riseFromTheGroundNormal")){
			if (distance <= lookRadius) {
				Move ();
                if(distance <= attackRadius){

                }
			}
		}
	}
		
	public override void Move()
	{
		nav.destination = target.position;
		if (!nav.pathPending && nav.remainingDistance > attackRadius) {
			nav.isStopped = false;
		}
	}
		
	public override void TakeDamage(int damage)
    {
        health = health - damage;
        Debug.Log(health);
        if (health <= 0)
            Die();
        anim.SetTrigger ("attacked");
	}

	public override void BasicAttack()
	{
		transform.LookAt (target);
		if (Time.time > nextAttack) {
			nextAttack = Time.time + attackSpeed;
			anim.SetTrigger ("attack");
		}
		nav.isStopped = true;
	}

	public override void Die ()
	{
		anim.SetTrigger ("dead");
        Destroy(this.gameObject, 1f);
	}

	void OnDrawGizmos(){
		Handles.color = Color.yellow;
		Handles.DrawWireArc (transform.position+new Vector3(0,0.2f,0), transform.up, transform.right, 360, lookRadius);
		Handles.color = Color.red;
		Handles.DrawWireArc (transform.position+new Vector3(0,0.2f,0), transform.up, transform.right, 360, attackRadius);
	}
}
