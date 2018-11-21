using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour {

	void EnemyFootstep1 () {
        AudioManager.instance.Play("EnemyFootstep1");
    }

    void EnemyFootstep2 () {
        AudioManager.instance.Play("EnemyFootstep2");
    }

    void EnemyFootstep3 () {
        AudioManager.instance.Play("EnemyFootstep3");
    }

    void EnemyFootstep4 () {
        AudioManager.instance.Play("EnemyFootstep4");
    }

    void EnemyFootstep5 () {
        AudioManager.instance.Play("EnemyFootstep5");
    }

    void DragonAttack () {
        AudioManager.instance.Play("DragonAttack");
    }

    void DragonFlight () {
        AudioManager.instance.Play("DragonFlight");
    }

    void GhoulAttack () {
        AudioManager.instance.Play("GhoulAttack");
    }


    void HydraAttack () {
        AudioManager.instance.Play("HydraAttack");
    }

    
    void SkeletonAttack () {
        AudioManager.instance.Play("SkeletonAttack");
    }

    
    void ZombieAttack () {
        AudioManager.instance.Play("ZombieAttack");
    }
}
