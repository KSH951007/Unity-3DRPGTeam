using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertSceneBGM : MonoBehaviour
{
    [SerializeField] private GameObject bossAppear;
    bool afterSceneLoaded = false;
    bool bgmChanged = false;

    private void Update()
    {
        if (!afterSceneLoaded)
        {
            SoundManager.instance.PlaySound("BGM");
            SoundManager.instance.PlaySound("AmbBGM");
            afterSceneLoaded = true;
        }

        if (bossAppear.activeSelf && !bgmChanged)
        {
            bgmChanged = true;
            StartCoroutine(EnterBoss());
        }
    }

    IEnumerator EnterBoss()
    {
        SoundManager.instance.StopSound("BGM");
        SoundManager.instance.StopSound("AmbBGM");
        yield return new WaitForSeconds(1);
        SoundManager.instance.PlaySound("BossBGM");
    }
}
