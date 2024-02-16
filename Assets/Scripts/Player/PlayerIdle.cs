using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class PlayerIdle : BaseState<EnumType.PlayerState, PlayerController>
{
    private PlayerMover mover;
    public PlayerIdle(PlayerController owner, StateMachine<EnumType.PlayerState, PlayerController> stateMachine) : base(owner, stateMachine)
    {
        mover = owner.Mover;
        // owner.Inputs.Player.ClickAction.performed += _ => MoveTo();
    }

    public override void Enter()
    {
        stateMachine.GetAnimator.SetFloat("Move", mover.Agent.remainingDistance);
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        

    }


}
