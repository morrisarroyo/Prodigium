using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    public ParticleSystem flameParticle;
    public List<ParticleCollisionEvent> collisionEvents;

    private void Start()
    {
    }
    public void Deactivate()
    {
        var em = flameParticle.emission;
        em.enabled = false;
    }

    public void Activate()
    {
        var em = flameParticle.emission;
        em.enabled = true;
    }

}
