using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    [SerializeField] private GameObject loginCanvas;
    [SerializeField] private GameObject pressButtonGuide;
    private bool pressed;

    private void Update()
    {
        if (Input.anyKeyDown && !pressed)
        {
            pressed = true;
            pressButtonGuide.SetActive(false);
            loginCanvas.SetActive(true);
        }
    }
}
