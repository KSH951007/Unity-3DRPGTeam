using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class PlayerIdleState : BaseState<EnumType.PlayerState, PlayerController>
{
    private PlayerMover mover;
    public PlayerIdleState(PlayerController owner, StateMachine<EnumType.PlayerState, PlayerController> stateMachine) : base(owner, stateMachine)
    {
        mover = owner.Mover;

    }

    public override void Enter()
    {
        owner.Inputs.Player.Move.performed += _ => owner.PointerClickMove();
        owner.Inputs.Player.Attack.performed += _ => owner.PointerClickAttack();
        stateMachine.GetAnimator.SetFloat("Move", mover.Agent.remainingDistance);
    }

    public override void Exit()
    {
        owner.Inputs.Player.Move.performed -= _ => owner.PointerClickMove();
        owner.Inputs.Player.Attack.performed -= _ => owner.PointerClickAttack();
    }

    public override void Update()
    {
       

    }


}
