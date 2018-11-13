using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon {

	public string _name;
	public string _desc;
	public Sprite _icon;
	public float _attackModifier;

	void Awake(){
		this.name = _name;
		this.desc = _desc;
		this.icon = _icon;
		this.attackModifier = _attackModifier;
	}
	
	public override void Equip ()
	{
		transform.parent = GameObject.FindGameObjectWithTag ("RightHand").transform;
		transform.localPosition = new Vector3(.2f,0f,-.02f);
		transform.localEulerAngles = new Vector3(0f,-80f, 160);
	}
}
