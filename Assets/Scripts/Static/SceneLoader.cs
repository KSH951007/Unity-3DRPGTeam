using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    public enum SceneType { Village, Dungeon }
    private SceneType sceneType;
    [SerializeField] private SceneSO[] sceneDatas;
    public event Action onSceneChanged;

    public SceneType GetSceneType()
    {
        return sceneType;
    }
    private void Awake()
    {
        if (false == Init())
            return;

        SceneChange();
        SceneManager.sceneLoaded += (_, _) => { SceneChange(); };
    }

    public void SceneChange()
    {
        for (int i = 0; i < sceneDatas.Length; i++)
        {
            if (sceneDatas[i].GetSceneIndex() == SceneManager.GetActiveScene().buildIndex)
            {
                if (sceneDatas[i].GetSceneType() == SceneType.Village)
                {
                    sceneType = SceneType.Village;
                }
                else
                {
                    sceneType = SceneType.Dungeon;
                }

                break;
            }
        }
        onSceneChanged?.Invoke();
    }
}
