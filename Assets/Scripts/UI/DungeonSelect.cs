using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DungeonSelect : MonoBehaviour
{
    [SerializeField]
    Button desert;
    [SerializeField]
    Button Dungeon;

    private void Start()
    {
        desert.onClick.AddListener(selectDesert);
        Dungeon.onClick.AddListener(selectDungeon);
        gameObject.SetActive(false);
    }

    public void selectDungeon()
    {
        Singleton<SceneLoader>.Instance.LoadScene(2);
        gameObject.SetActive(false);

    }
    public void selectDesert()
    {
        Singleton<SceneLoader>.Instance.LoadScene(3);
        gameObject.SetActive(false);
    }
}