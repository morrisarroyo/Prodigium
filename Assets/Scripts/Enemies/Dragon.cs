using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : BaseCreature
{
    public new int health;
    public new int basicAttackDamage;
    public new float movementSpeed = 6f;
    public new float rotationSpeed = 6f;

    GameObject player;
    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        player = getPlayerToTrack();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

	public override void BasicAttack()
	{
		throw new System.NotImplementedException ();
    }

	public override void Move()
    {
        if (player != null)
        {
            // The step size is equal to speed times frame time.
            float movementStep = movementSpeed * Time.deltaTime;
            float rotationStep = rotationSpeed * Time.deltaTime;

            // Move our position a step closer to the target.

            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementStep);
            Vector3 targetDir = player.transform.position - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotationStep, 0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }

    public override void TakeDamage(int damage)
    {

    }

	public override void Die ()
	{
		throw new System.NotImplementedException ();
	}

    private GameObject getPlayerToTrack()
    {
        GameObject player = null;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            player = GameObject.FindGameObjectsWithTag("Player")[0];
        }
        return player;
    }
}
