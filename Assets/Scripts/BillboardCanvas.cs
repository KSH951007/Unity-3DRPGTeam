using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BillboardCanvas : MonoBehaviour
{
    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
    }
    private void Update()
    {
        Quaternion rot = Quaternion.FromToRotation(transform.forward, Camera.main.transform.forward);
        if(rot != Quaternion.identity)
        {
            transform.rotation = rot;
        }
    }
}