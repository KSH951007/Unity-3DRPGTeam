using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    private Canvas canvas;
    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
    }
    private void Update()
    {
        Quaternion rot = Quaternion.FromToRotation(transform.transform.forward, Camera.main.transform.forward);
        if(rot != Quaternion.identity)
        {
            transform.rotation = rot;
        }
       
    }
}
