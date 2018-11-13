using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateArmour : Armour {

	public string _name;
	public string _desc;
	public Sprite _icon;
	public float _armourModifier;
	public float _speedModifier;

	// Use this for initialization
	void Awake(){
		this.name = _name;
		this.desc = _desc;
		this.icon = _icon;
		this.armourModifier = _armourModifier;
		this.speedModifier = _speedModifier;
	}
	
	public override void Equip ()
	{
		transform.parent = GameObject.FindGameObjectWithTag ("Chest").transform;
		transform.localPosition = new Vector3(0f,.024f,-.395f);
		transform.localEulerAngles = new Vector3(-80f,0f,0f);
	}
}
