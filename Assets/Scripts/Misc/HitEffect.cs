using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HitEffect : MonoBehaviour
{

    private ParticleSystem[] hitPatricle;
    private bool allParticleStopping;

    private void Awake()
    {
        hitPatricle = GetComponentsInChildren<ParticleSystem>();

    }
    private void OnEnable()
    {
        allParticleStopping = false;
        hitPatricle[0].Play();
    }   

    private void Update()
    {

        allParticleStopping = true;
        foreach (ParticleSystem particle in hitPatricle)
        {
            if (particle.isPlaying)
            {
                allParticleStopping = false;
                break;
            }
        }
        if (allParticleStopping == true)
        {
            PoolManager.Instance.ReturnPool(gameObject);
            return;
        }

    }
}
