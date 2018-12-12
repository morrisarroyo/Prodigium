﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashDetection : MonoBehaviour {

    GameObject player;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        Destroy(gameObject, .1f);
    }

    private void OnCollisionEnter(Collision collider) {
        GameObject col = collider.gameObject;
        Debug.Log(col.name);
        if (col.tag == "Enemy") {
            Debug.Log("hit: " + col.name);
            player.GetComponent<PlayerController>().BasicAttack(col.GetComponent<BaseCreature>());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject col = other.gameObject;
        Debug.Log(col.name);
        if (col.tag == "EnemyBoss")
        {
            Debug.Log("hit: " + col.name);
            player.GetComponent<PlayerController>().BasicAttack(col.GetComponent<BaseCreature>());
        }
    }
}