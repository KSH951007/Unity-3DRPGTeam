using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSelectUI : MonoBehaviour
{
    
  

    public void PressDesertButton()
    {
        StartCoroutine(SceneLoader.Instance.LoadScene(2));
    }
    public void PressDungeonButton()
    {
        StartCoroutine(SceneLoader.Instance.LoadScene(3));
    }
    public void PressCancleButton()
    {
        this.gameObject.SetActive(false);
    }
}
