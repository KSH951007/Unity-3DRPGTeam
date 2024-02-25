using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAnimEvent : MonoBehaviour
{
    public event Action onProgressAttack;

    public void StartAttack()
    {
    }
    public void ProgressAttack()
    {
        Debug.Log("attack");
        onProgressAttack?.Invoke();
    }

    public void EndAttack()
    {
    }
}
