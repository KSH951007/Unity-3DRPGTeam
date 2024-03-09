using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSources : MonoBehaviour
{
    private void Start()
    {
        SoundManager.instance.AddAllSounds(gameObject);
    }
}
