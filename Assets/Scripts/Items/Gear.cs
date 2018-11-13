using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Gear : MonoBehaviour {

	public static Gear instance;

	void Awake(){
		if (instance != null) {
			Debug.Log ("More than one isntance of Gear found!");
			return;
		}
		instance = this;
		
	}

	public Weapon currentWeapon;
	public Armour currentArmour;
	public int healthPotions;

	public void Equip(Item item){
		item.GetComponent<Rigidbody> ().isKinematic = true;
		if (item is Weapon)
			EquipWeapon (item as Weapon);
		else if (item is Armour)
			EquipArmour (item as Armour);
		else if (item is HealthPotion)
			AddHealthPotion (item as HealthPotion);
	}

	public void EquipWeapon (Weapon w){
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().RemoveFocus ();
		if (currentWeapon != null)
			currentWeapon.GetComponent<ItemInteractable> ().DestroyItem ();
		currentWeapon = w;
		currentWeapon.GetComponent<Collider> ().enabled = false;
		currentWeapon.Equip ();
	}

	public void EquipArmour (Armour a){
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		player.GetComponent<PlayerController> ().RemoveFocus ();
		player.GetComponent<NavMeshAgent> ().speed = a.speedModifier;
		if (currentArmour != null)
			currentArmour.GetComponent<ItemInteractable> ().DestroyItem ();
		currentArmour = a;
		currentArmour.GetComponent<Collider> ().enabled = false;
		currentArmour.Equip ();
	}

	public bool RemoveHealthPotion(){
		if (healthPotions > 0) {
			healthPotions--;
			Debug.Log ("REMOVING: "+healthPotions);
			return true;
		} else
			return false;
	}

	public void AddHealthPotion(HealthPotion hp){
		hp.Equip ();
		healthPotions++;
		Debug.Log ("ADDING: "+healthPotions);
	}
}
