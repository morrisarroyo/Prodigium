using System.Collections;
using UnityEngine;

public abstract class Ability : ScriptableObject {

	public string name;
	public Sprite sprite;
	public AudioClip sound;
	public float baseCooldown;

	public abstract void Initialize (GameObject obj);
	public abstract void TriggerAbility();
}
