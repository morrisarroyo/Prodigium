using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameCollision : MonoBehaviour
{
    public ParticleSystem flameParticle;
    public List<ParticleCollisionEvent> collisionEvents;
    private int damage = 1;
    private int particlePerDamage = 1;

    void Start()
    {
        flameParticle = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        damage = 10;
        particlePerDamage = 10;
    }

    void OnParticleCollision(GameObject other)
    {
        //Debug.Log(other.tag);
        if (other.gameObject.tag.Equals("Player"))
        {
            int numCollisionEvents = flameParticle.GetCollisionEvents(other, collisionEvents);
            int i = 0;
            int count = particlePerDamage;
            while (i < numCollisionEvents)
            {

                --count;
                if (count == 0)
                {
                    other.GetComponent<PlayerController>().TakeDamage(damage);
                    Debug.Log(damage);
                }
                i++;
            }
        }
    }
}
