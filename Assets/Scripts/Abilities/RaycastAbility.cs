using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Abilities/RaycastAbility")]
public class RayCastAbility : Ability {
	public int damage;
	public float range;
	public float hitForce;
	public Color laserColor;

	private RaycastShootTriggerable rcShoot;

	public override void Initialize(GameObject obj){
		rcShoot = obj.GetComponent<RaycastShootTriggerable> ();
		rcShoot.Initialize ();

		rcShoot.gunDamage = damage;
		rcShoot.weaponRange = range;
		rcShoot.hitForce = hitForce;
		rcShoot.laserLine.material = new Material (Shader.Find ("Unlit/Color"));
		rcShoot.laserLine.material.color = laserColor;
	}

	public override void TriggerAbility(){
		rcShoot.Fire ();
	}
}
