using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhoulBase : BaseCreature {

	public int healthValue = 100;
	public int basicAttackDamageValue = 10;
	public float movementSpeedValue = 5f;
    public int scoreValue = 1000;
    public bool destroyOnDeath;
	private bool isSinking;

	GameObject player;
	Transform target;
	Animator anim;
	NavMeshAgent nav;
	Combat combat;

	public float lookRadius;
	public float attackRadius;

	private float nextAttack;
	public float attackSpeed = 2f;

	// Use this for initialization
	void Awake ()
	{
		health = healthValue;
		basicAttackDamage = basicAttackDamageValue;
		movementSpeed = movementSpeedValue;
		player = GameObject.FindGameObjectWithTag ("Player");
		target = player.transform;
		nav = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();
		transform.rotation = Random.rotation;
		combat = GetComponent<Combat> ();
		nav.speed = Random.Range((float)(movementSpeed - movementSpeed * .2), (float)(movementSpeed + movementSpeed * .2));
		Physics.OverlapSphere (transform.position, 5f);
	}
	
	// Update is called once per frame
	void Update () {
		float distance = Vector3.Distance (transform.position, target.position);
		if (!player.GetComponent<PlayerController>().dead) {
			if (!isSinking) {
				if (distance <= lookRadius) {
					Move();
					if (distance <= attackRadius) {
						BasicAttack();
					}
				}
			} else {
				transform.Translate(-Vector3.up * .1f * Time.deltaTime);
			}
		}
	}

	public override void Move()
	{
		if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("attack")) {
			nav.destination = target.position;
			if (!nav.pathPending && nav.remainingDistance > attackRadius) {
				nav.isStopped = false;
			}
		}
	}

	public override void TakeDamage(int damage)
	{
		health = health - damage;
		if (health <= 0)
			Die();
		else
			anim.SetTrigger ("attacked");
	}

	public override void BasicAttack()
	{
		transform.LookAt (target);
		if (Time.time > nextAttack) {
			nextAttack = Time.time + attackSpeed;
			anim.SetTrigger ("attack");
			combat.dealDamage(player.GetComponent<BaseCreature>(), basicAttackDamage);
		}
		nav.isStopped = true;
	}

	public override void Die ()
	{
		anim.SetTrigger ("dead");
		isSinking = true;
		GetComponent<NavMeshAgent>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;
		GetComponent<CapsuleCollider>().isTrigger = true;
		GetComponent<EnemyInteractable>().enabled = false;
		GetComponent<ItemDrop> ().DropItem ();
        ScoreManager.instance.score += scoreValue;
        if (destroyOnDeath)
		{
			Destroy(this.gameObject, 3f);
		}
	}
}
