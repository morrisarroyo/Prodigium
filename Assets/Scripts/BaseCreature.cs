using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCreature : MonoBehaviour
{
	protected int health { get; set; }
	protected int basicAttackDamage { get; set; }
	protected int movementSpeed { get; set;}

	public enum creatureState {
		idle,
		walking,
		attacking,
		dead,
		spawning
	}

	protected abstract void Move ();

	protected void TakeDamage(int damage) {
		health = health - damage;
	}

	protected abstract void BasicAttack ();

	protected abstract void Die ();

}
