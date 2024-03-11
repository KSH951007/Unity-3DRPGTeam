using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAnimEvent : MonoBehaviour
{
    public event Action onStartAttack;
    public event Action onProgressAttack;
    public event Action onEndAttack;
    public event Action onStep;

    public void StartAttack()
    {
        onStartAttack?.Invoke();
    }
    public void ProgressAttack()
    {
        onProgressAttack?.Invoke();
    }

    public void EndAttack()
    {
        onEndAttack?.Invoke();
    }
    public void Step()
    {
        onStep?.Invoke();
    }
}
