using System.Collections;
using System.Collections.Generic;

interface BaseCreature
{
    // Can't have properties in Interfaces
    //int health { get; set; }
    //int basicAttackDamage { get; set; }
    //int movementSpeed { get; set; }

    void Move();
    void TakeDamage(int damage);
    void BasicAttack();
}
