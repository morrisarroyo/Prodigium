using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour, BaseCreature
{
    public int health { get; set; }
    public int basicAttackDamage { get; set; }
    public int movementSpeed { get; set; }

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

    public void BasicAttack()
    {
        throw new System.NotImplementedException();
    }

    public void Move()
    {

    }

    public void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

}
