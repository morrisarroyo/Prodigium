using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]

public class Zombie : BaseCreature {

	public int health = 100;
	public int basicAttackDamage = 10;
	public int movementSpeed = 5;
	public creatureState state;

	Transform target;
	Animator anim;
	NavMeshAgent nav;

	public float lookRadius;
	public float attackRadius;

	private float nextAttack;
	public float attackSpeed = 1f;

	// Use this for initialization
	void Awake ()
	{
		state = creatureState.spawning;
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		nav = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();
		Physics.OverlapSphere (transform.position, 5f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//if (state == creatureState.walking) {
			Move ();
		
	}
		
	protected override void Move()
	{
		nav.SetDestination (target.position);
	}
		
	protected void TakeDamage(int damage)
	{
		base.TakeDamage (damage);
	}

	protected override void BasicAttack()
	{
		
	}

	protected override void Die ()
	{
		
	}
}
