using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    public ParticleSystem particleSystem1;
    public ParticleSystem particleSystem2;

    void Start()
    {
        Debug.Log("ParticleSystemController started");
    }

    public void TriggerParticles()
    {
        Debug.Log("TriggerParticles called");
        if (particleSystem1 != null && particleSystem2 != null)
        {
            Debug.Log("Playing particle systems");
            particleSystem1.Play();
            particleSystem2.Play();
        }
        else
        {
            Debug.LogWarning("Particle systems not assigned");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TriggerParticles();
        }
    }
}
