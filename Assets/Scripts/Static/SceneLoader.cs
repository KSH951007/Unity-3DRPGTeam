using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : Singleton<SceneLoader>
{
    [SerializeField] private SceneSO[] sceneDatas;
    private void Awake()
    {
        if (false == Init())
            return;


    }
}
