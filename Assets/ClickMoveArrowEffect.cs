using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMoveArrowEffect : MonoBehaviour
{

    private ParticleSystem particle;
    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
        gameObject.SetActive(false);
    }
    public void Play(Vector3 point)
    {
        if(particle.isPlaying)
        {
            particle.Stop();

        }

        transform.position = point+ Vector3.up *0.1f;
        gameObject.SetActive(true);
        particle.Play();
    }


    private void Update()
    {
        if(particle.time >= particle.main.startLifetime.constant)
        {
            particle.Stop();
            gameObject.SetActive(false);
            return;
        }

    }
}
