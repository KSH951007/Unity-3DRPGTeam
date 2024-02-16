using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
    private bool isControl;
    private void Awake()
    {
        character = GetComponentInChildren<CharacterManager>();
        mover = GetComponent<PlayerMover>();
        isControl = true;
        stateMachine = new StateMachine<EnumType.PlayerState, PlayerController>(character.GetMainHero().HeroAnimator);

    }
    private void Start()
    {
        stateMachine.AddState(new PlayerIdle(this, stateMachine));
        stateMachine.AddState(new PlayerRun(this, stateMachine));

        stateMachine.ChangeState(EnumType.PlayerState.Idle);
        inputs.Player.ClickAction.performed += _ => ClickActions();
        inputs.Player.ChangeCharacter.performed += _ => character.ChangeCharacter(character.nextCharacter());
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
    public void ClickActions()
    {
        if (!isControl)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.value);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                mover.MoveTo(hit.point);
                stateMachine.ChangeState(EnumType.PlayerState.Run);
                return;
            }
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {

            }
        }
    }
}
