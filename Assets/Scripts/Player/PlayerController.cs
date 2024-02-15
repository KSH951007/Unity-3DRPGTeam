using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private PlayerControls inputs;
    private CharacterManager character;
    private PlayerMover mover;
    private StateMachine<EnumType.PlayerState, PlayerController> stateMachine;
    public PlayerControls Inputs { get { return inputs; } }
    public PlayerMover Mover { get { return mover; } }
    private void Awake()
    {
        character = GetComponentInChildren<CharacterManager>();
        mover = GetComponent<PlayerMover>();
        stateMachine = new StateMachine<EnumType.PlayerState, PlayerController>(character.GetMainHero().HeroAnimator);

    }
    private void Start()
    {
        stateMachine.AddState(new PlayerIdle(this, stateMachine));
        stateMachine.AddState(new PlayerRun(this, stateMachine));

        stateMachine.ChangeState(EnumType.PlayerState.Idle);
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
    private void Update()
    {
        stateMachine?.Update();
    }
}
