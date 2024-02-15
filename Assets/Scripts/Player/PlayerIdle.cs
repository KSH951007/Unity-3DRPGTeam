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
        owner.Inputs.Player.Move.performed += _ => MoveTo();
    }

    public override void Enter()
    {
        stateMachine.GetAnimator.SetFloat("Move",mover.Agent.remainingDistance);
    }

    public override void Exit()
    {
       
    }

    public override void Update()
    {


    }
    public void MoveTo()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.value);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                Debug.Log("move");
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
