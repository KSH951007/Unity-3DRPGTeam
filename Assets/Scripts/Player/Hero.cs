using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    protected Animator animator;
    protected CapsuleCollider myCollider;

   
    private void Awake()
    {
        animator = GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider>();
    }

}
