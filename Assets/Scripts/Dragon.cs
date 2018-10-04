using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : BaseCreature
{
	public int health;
	public int basicAttackDamage;
	public int movementSpeed;

    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
		
    }

	protected override void BasicAttack()
	{
		throw new System.NotImplementedException ();
    }

    protected override void Move()
	{
		throw new System.NotImplementedException ();
    }

    protected void TakeDamage(int damage)
    {
		base.TakeDamage(damage);
    }

	protected override void Die ()
	{
		throw new System.NotImplementedException ();
	}

}
