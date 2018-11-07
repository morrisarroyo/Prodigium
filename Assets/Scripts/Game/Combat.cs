using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseCreature))]
public class Combat : MonoBehaviour {

    BaseCreature creature;

    private void Start()
    {
        creature = GetComponent<BaseCreature>();
    }

    public void dealDamage(BaseCreature c, int damage) {
        c.TakeDamage(damage);
    }
}
