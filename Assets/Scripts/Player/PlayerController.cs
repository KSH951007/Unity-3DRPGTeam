using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private PlayerControls inputs;
    private CharacterManager character;
    public PlayerControls Inputs { get { return inputs; } }
    private void Awake()
    {
        character = GetComponentInChildren<CharacterManager>();
    }
    private void OnEnable()
    {
        if (inputs == null)
        {
            inputs = new PlayerControls();
        }
        inputs.Enable();
    }
    private void OnDisable()
    {
        if (inputs != null)
        {
            inputs.Disable();
        }
    }

}
