using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTrigger : MonoBehaviour
{


    public event Action<Collider> onTrigger;
    private void Start()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        onTrigger?.Invoke(other);
    }
}
