using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : BaseState<EnumType.PlayerState,PlayerController>
{
    private PlayerMover mover;
    public PlayerRun(PlayerController owner, StateMachine<EnumType.PlayerState, PlayerController> stateMachine) : base(owner, stateMachine)
    {
        mover = owner.Mover;
    }

    public override void Enter()
    {
        stateMachine.GetAnimator.SetFloat("Move", mover.Agent.remainingDistance);
    }

    public override void Exit()
    {
        stateMachine.GetAnimator.SetFloat("Move", mover.Agent.remainingDistance);
    }
    public override void Update()
    {
        stateMachine.GetAnimator.SetFloat("Move", mover.Agent.remainingDistance);
 
        if (mover.Whetherstatus())
        {
            Debug.Log("stop");
            stateMachine.ChangeState(EnumType.PlayerState.Idle);
            return;
        }
    }


}
