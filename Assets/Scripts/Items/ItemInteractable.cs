using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractable : Interactable {

	public Item item;

	public override void Interact(){
		base.Interact ();
		PickUp ();
	}

	void PickUp(){
		Debug.Log ("Picking up item");
		Gear.instance.Equip(item);
	}

	public void DestroyItem(){
		Destroy (gameObject);
	}
}
