using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    private EnumType.SceneType sceneType;
    [SerializeField] private SceneSO[] sceneDatas;

    public EnumType.SceneType GetSceneType()
    {
        return sceneType;
    }
    private void Awake()
    {
        if (false == Init())
            return;
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        for (int i = 0; i < sceneDatas.Length; i++)
        {
            if (sceneDatas[i].GetSceneIndex() == SceneManager.GetActiveScene().buildIndex)
            {
                if (sceneDatas[i].GetSceneType() == EnumType.SceneType.Village)
                {
                    sceneType = EnumType.SceneType.Village;
                }
                else
                {
                    sceneType = EnumType.SceneType.Dungeon;
                }

                break;
            }
        }
    }

    public void SceneChange()
    {

    }
}
