using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAnimEvent : MonoBehaviour
{

    public bool isAttack;

    private void Awake()
    {
        isAttack = false;
    }
    public void StartAttack()
    {
        isAttack = true;
    }
    public void EndAttack()
    {
        isAttack = false;
    }
}
