using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BillboardCanvas : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(Camera.main.transform,Camera.main.transform.up);
    }
}