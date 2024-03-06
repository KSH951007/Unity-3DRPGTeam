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
		}
	}

    public void ClickSoundButton()
    {
        if (!soundWindow.activeSelf)
        {
            soundWindow.SetActive(true);
        }
    }

    public void ClickGuestLogin()
    {
        // TODO : ���Ӿ����� �Ѿ��
        print("���Ӿ� LoadScene");
    }
}
