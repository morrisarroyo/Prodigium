using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteractable : Interactable {

    BaseCreature creature;
    GameObject pl;
    PlayerController playerManager;

    private void Start()
    {
        creature = GetComponent<BaseCreature>();
        pl = GameObject.FindGameObjectWithTag("Player");
        playerManager = pl.GetComponent<PlayerController>();
    }

    public override void Interact()
    {
        print("Interact");
        playerManager.BasicAttack();
    }

}
