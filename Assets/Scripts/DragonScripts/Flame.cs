using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    public ParticleSystem flameParticle;

    private void Start()
    {
    }
    public void Deactivate()
    {
        var em = flameParticle.emission;
        em.enabled = false;
        AudioManager.instance.Stop("DragonFlame");
    }

    public void Activate()
    {
        var em = flameParticle.emission;
        em.enabled = true;
        AudioManager.instance.Play("DragonFlame");
    }

}
