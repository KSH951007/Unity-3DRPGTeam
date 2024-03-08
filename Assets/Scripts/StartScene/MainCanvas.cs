using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    [SerializeField] private GameObject loginCanvas;
    [SerializeField] private GameObject pressButtonGuide;
    AudioSource pressAudioSource;
    private bool pressed;

	private void Awake()
	{
		pressAudioSource = GetComponent<AudioSource>();
	}

	private void Update()
    {
        if (Input.anyKeyDown && !pressed)
        {
            pressed = true;
            pressAudioSource.Play();
            pressButtonGuide.SetActive(false);
            loginCanvas.SetActive(true);
        }
    }
}
