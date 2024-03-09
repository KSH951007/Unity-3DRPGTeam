using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    private Camera mainCamera;
    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.TryGetComponent(out Renderer renderer))
        {
             Color fadeColor = renderer.material.color;
             renderer.material.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 0.5f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Renderer>(out Renderer renderer))
        {
            Color fadeColor = renderer.material.color;
            renderer.material.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 1f);
        }
    }
}
