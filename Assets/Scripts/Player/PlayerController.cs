using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private PlayerControls inputs;
    private CharacterManager character;
    private StateMachine<EnumType.PlayerState, PlayerController> stateMachine;
    public PlayerControls Inputs { get { return inputs; } }
    private void Awake()
    { 
        character = GetComponentInChildren<CharacterManager>();
        stateMachine = new StateMachine<EnumType.PlayerState, PlayerController>();

    }
    private void OnEnable()
    {
        if (inputs == null)
        {
            inputs = new PlayerControls();
        }
        inputs.Enable();
        inputs.Player.Move.performed += _ =>
        {
           
        };
     
    }
    private void OnDisable()
    {
        if (inputs != null)
        {
            inputs.Disable();
        }
    }

}
