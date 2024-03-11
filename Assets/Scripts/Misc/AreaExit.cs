using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private int nextSceneIndex;
    [SerializeField] private DungeonSelectUI dungeonSelectUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            dungeonSelectUI.gameObject.SetActive(true);

        }
    }
}
