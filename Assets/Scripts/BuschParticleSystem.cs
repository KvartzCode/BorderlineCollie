using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuschParticleSystem : MonoBehaviour
{
    private ParticleSystem[] particleSystems;
    public bool particleSystemsEnabled = false; 

    void Start()
    {
        // Get all ParticleSystems attached to the GameObject
        particleSystems = GetComponentsInChildren<ParticleSystem>(true);

        // Set the initial state of ParticleSystems based on the public bool
        UpdateParticleSystemsState();
    }

    void Update()
    {
       
        if (particleSystemsEnabled)
        {
            EnableParticleSystems();
        }
        else
        {
            DisableParticleSystems();
        }
    }

    void EnableParticleSystems()
    {
        if (particleSystems != null)
        {
            // Enable each ParticleSystem
            foreach (ParticleSystem ps in particleSystems)
            {
                ps.gameObject.SetActive(true);
            }
        }
    }

    void DisableParticleSystems()
    {
        if (particleSystems != null)
        {
            // Disable each ParticleSystem
            foreach (ParticleSystem ps in particleSystems)
            {
                ps.gameObject.SetActive(false);
            }
        }
    }

    void UpdateParticleSystemsState()
    {
        if (particleSystemsEnabled)
        {
            EnableParticleSystems();
        }
        else
        {
            DisableParticleSystems();
        }
    }

}

