using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginCanvas : MonoBehaviour
{
    [SerializeField] private GameObject quitWindow;
    [SerializeField] private GameObject soundWindow;
    public AudioSource soundSource;

    public void ClickQuitButton()
    {
        if (!quitWindow.activeSelf)
        {
			quitWindow.SetActive(true);
			soundSource.Play();
		}
	}

    public void ClickSoundButton()
    {
        if (!soundWindow.activeSelf)
        {
            soundWindow.SetActive(true);
            soundSource.Play();
        }
    }

    public void ClickGuestLogin()
    {
        // TODO : 게임씬으로 넘어가기
        print("게임씬 LoadScene");
    }
}
