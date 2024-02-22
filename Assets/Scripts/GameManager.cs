using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Hero player;
    public PlayerInteract plin;
    public UiManager ui;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
            return;
        }
     
        ui = GetComponentInChildren<UiManager>();
    }
}