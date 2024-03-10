using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginCanvas : MonoBehaviour
{
    [SerializeField] private GameObject quitWindow;
    [SerializeField] private GameObject soundWindow;

    public void ClickQuitButton()
    {
        if (!quitWindow.activeSelf)
        {
			quitWindow.SetActive(true);
            SoundManager.instance.PlaySound("QuitButton");
		}
	}

    public void ClickSoundButton()
    {
        if (!soundWindow.activeSelf)
        {
            soundWindow.SetActive(true);
            SoundManager.instance.PlaySound("OpenSetting");
        }
    }

    public void ClickGuestLogin()
    {
        // TODO : ���Ӿ����� �Ѿ��
        SoundManager.instance.PlaySound("Login");
        print("���Ӿ� LoadScene");
    }
}
