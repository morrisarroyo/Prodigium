using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Consumable {

	public string _name;
	public string _desc;
	public Sprite _icon;

	// Use this for initialization
	void Awake(){
		this.name = _name;
		this.desc = _desc;
		this.icon = _icon;
	}

	public override void Equip(){
		Destroy (gameObject);
	}
}
