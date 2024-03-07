using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SceneData", menuName = "ScriptableObject/SceneData")]
public class SceneSO : ScriptableObject
{
    [SerializeField] private SceneLoader.SceneType sceneType;
    [SerializeField] private int sceneIndex;
    [SerializeField] private string sceneName;

    public SceneLoader.SceneType GetSceneType() { return sceneType; }
    public int GetSceneIndex() { return sceneIndex;}
    public string GetSceneName() {  return sceneName;}

}
