using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private int helath;


    public event Action onHit;
    public event Action onDie;

    public void SetHealth(int newHealth)
    {
        helath = newHealth;
    }

    public void TakeHit(int damage)
    {
        helath = Mathf.Max(helath - damage, 0);
        if(helath <= 0)
        {
            onDie?.Invoke();
            return;
        }

        onHit?.Invoke();

    }
}
