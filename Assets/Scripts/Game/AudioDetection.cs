using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDetection : MonoBehaviour {

    public GameObject player;
    int enemies;
    int bosses;

    // Use this for initialization
    void Start () {
        enemies = 0;
        bosses = 0;
	}
	
	// Update is called once per frame
	void Update () {
        EnemyDetect(player.transform.position, 15);
	}

    void EnemyDetect(Vector3 center, float radius) {
        bosses = 0;
        enemies = 0;
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach(Collider obj in hitColliders) {
            if (obj.tag == "EnemyBoss") {
                bosses++;
            } else if (obj.tag == "Enemy") {
                enemies++;
            }
        }

        if (bosses > 0) {
            AudioManager.instance.bossMusic = true;
            AudioManager.instance.combatMusic = false;
            AudioManager.instance.backgroundMusic = false;
        } else if (enemies > 0) {
            AudioManager.instance.bossMusic = false;
            AudioManager.instance.combatMusic = true;
            AudioManager.instance.backgroundMusic = false;
        } else {
            AudioManager.instance.bossMusic = false;
            AudioManager.instance.combatMusic = false;
            AudioManager.instance.backgroundMusic = true;
        }

    }
}
