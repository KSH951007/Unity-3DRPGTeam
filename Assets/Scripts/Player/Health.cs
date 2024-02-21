using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private int helath;
    public enum hitType { NONE, DOWN }

    public event Action onHit;
    public event Action onDie;
    public event Action onDown;

    public void SetHealth(int newHealth)
    {
        helath = newHealth;
    }
    public void TakeHit(int damage, hitType newType = hitType.NONE, GameObject hitParticle = null)
    {
        helath = Mathf.Max(helath - damage, 0);
        if (hitParticle != null)
        {

        }

        if (helath <= 0)
        {
            onDie?.Invoke();
            return;
        }


        onHit?.Invoke();

    }
}
