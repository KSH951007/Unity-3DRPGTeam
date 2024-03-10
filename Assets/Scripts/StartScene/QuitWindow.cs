using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitWindow : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			gameObject.SetActive(false);
		}
	}

	public void ClickYesButton()
	{
		Application.Quit();
	}

	public void ClickNoButton()
	{
        SoundManager.instance.PlaySound("Cancel");
        gameObject.SetActive(false);
	}
}
