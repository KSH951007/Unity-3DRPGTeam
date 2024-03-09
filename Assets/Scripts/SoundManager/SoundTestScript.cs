using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTestScript : MonoBehaviour
{
    private void Start()
    {
        SoundManager.instance.AddAllSounds(gameObject);
    }
}
