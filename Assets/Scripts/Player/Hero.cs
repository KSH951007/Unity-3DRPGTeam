using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{


    protected Animator animator;
    protected CapsuleCollider myCollider;

    public Animator HeroAnimator { get => animator; }
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider>();
    }

}
