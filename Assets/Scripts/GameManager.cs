using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Hero player;
    public PlayerInteract plin;
    public UiManager ui;
    public DialogueWindow dialogueWindow;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
     
        ui = GetComponentInChildren<UiManager>();
    }
}