using System;
using UnityEngine;

[Serializable]
public abstract class Item : MonoBehaviour {

	public int id { get; set;}
	public string name { get; set;}
	public string desc { get; set;}
	public Sprite icon { get; set;}

	public abstract void Equip();
}
