using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayClickSound : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            SoundManager.instance.PlaySound("ClickSound");
        }
        if (Input.GetMouseButton(1))
        {
            SoundManager.instance.PlaySound("BGM");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SoundManager.instance.StopSound("BGM");
        }
    }
}
