using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour {

	void Footstep () {
        AudioManager.instance.Play("Footstep");
    }

    void Attack () {
        AudioManager.instance.Play("Attack");
    }
}
