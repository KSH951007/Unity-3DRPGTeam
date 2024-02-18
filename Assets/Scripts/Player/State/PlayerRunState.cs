using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : BaseState<EnumType.PlayerState,PlayerController>
{
    private PlayerMover mover;
    public PlayerRunState(PlayerController owner, StateMachine<EnumType.PlayerState, PlayerController> stateMachine) : base(owner, stateMachine)
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
        stateMachine.GetAnimator.SetFloat("Move", mover.Agent.remainingDistance);
    }
    public override void Update()
    {
        stateMachine.GetAnimator.SetFloat("Move", mover.Agent.remainingDistance);

        if (mover.Whetherstatus())
        {
            Debug.Log("stop");
            mover.ResetPath();
            Debug.Log(mover.Agent.remainingDistance);
            stateMachine.ChangeState(EnumType.PlayerState.Idle);
            return;
        }
    }


}
