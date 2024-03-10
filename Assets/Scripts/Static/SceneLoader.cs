using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{

    [SerializeField] private LoadingUI loadingUI;
    public enum SceneType { None, Village, Dungeon }
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
    public IEnumerator LoadScene(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;
       
        loadingUI.StartLoadingUI();
        float currentTIme = 0f;
        float maxTime = 10f;
        while (!operation.isDone)
        {
            float percent = currentTIme / maxTime;
            float result = Mathf.Min(percent, operation.progress * maxTime);

            loadingUI.LoadingProgress(result);


            if(currentTIme >= maxTime)
            {
                operation.allowSceneActivation = true;
                loadingUI.gameObject.SetActive(false);
                break;
            }

            currentTIme += Time.deltaTime;
            yield return null;
        }

    }
}
