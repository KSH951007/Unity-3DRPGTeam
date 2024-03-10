using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PreviewModel : MonoBehaviour
{
    private Quaternion originRot;
    
    

    private void Awake()
    {
        originRot = transform.localRotation;
    }
    private void OnDisable()
    {
        transform.localRotation = originRot;
    }
    public void SetRotation(Vector2 delta)
    {
        transform.localRotation = Quaternion.AngleAxis(delta.x, Vector3.up);
    }



}
