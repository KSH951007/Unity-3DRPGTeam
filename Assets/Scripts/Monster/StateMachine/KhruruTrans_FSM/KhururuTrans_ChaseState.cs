using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhururuTrans_ChaseState : BaseState
{
    public KhururuTrans_ChaseState(BossMonsters monster) : base(monster) { }

    public override void OnStateEnter()
    {
		_monster.SetChasingTime();

		_monster.timeForNextChange = _monster.chasingTime - 1f;
		Debug.Log(_monster.chasingTime);
		Debug.Log(_monster.timeForNextChange);

		_monster.nav.isStopped = false;
		_monster.animator.SetBool("Move", true);
	}

    public override void OnStateUpdate()
    {
		_monster.nav.SetDestination(_monster.target.position);

		FaceTarget();
	}

	public override void OnStateExit()
	{
		_monster.animator.SetBool("Move", false);
		_monster.nav.isStopped = true;
	}

	private void FaceTarget()
	{
		var targetDirection = (_monster.nav.steeringTarget - _monster.transform.position).normalized;
		if (targetDirection != Vector3.zero)
		{
			Quaternion lookRotation = Quaternion.LookRotation(new Vector3(targetDirection.x, 0, targetDirection.z));
			_monster.transform.rotation = Quaternion.Slerp(_monster.transform.rotation, lookRotation, Time.deltaTime * 5);
		}
	}
}
