using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSceneBGM : MonoBehaviour
{
    [SerializeField] private GameObject door;
    bool afterSceneLoaded = false;
    bool bgmChanged = false;
    bool bossClear  = false;
    private void Update()
    {
        if (!afterSceneLoaded)
        {
            SoundManager.instance.PlaySound("BGM");
            SoundManager.instance.PlaySound("AmbBGM");
            afterSceneLoaded = true;
        }

        if (door.activeSelf && !bgmChanged)
        {
            bgmChanged = true;
            StartCoroutine(EnterBoss());
        }
        
        if (bossClear)
        {
            SoundManager.instance.StopSound("BossBGM");
        }
    }

    IEnumerator EnterBoss()
    {
        SoundManager.instance.StopSound("BGM");
        SoundManager.instance.StopSound("AmbBGM");
        SoundManager.instance.PlaySound("DoorShut");
        yield return new WaitForSeconds(2);
        SoundManager.instance.PlaySound("BossBGM");
    }
}
