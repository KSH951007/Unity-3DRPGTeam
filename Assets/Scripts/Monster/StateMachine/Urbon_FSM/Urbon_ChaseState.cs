using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Urbon_ChaseState : BaseState
{
	public Urbon_ChaseState(BossMonsters monster) : base(monster) { }

	public override void OnStateEnter()
	{
        _monster.animator.SetBool("Move", true);
        _monster.nav.isStopped = false;
		SoundManager.instance.PlaySound("UrbonStep");

        _monster.SetChasingTime();
		_monster.timeForNextChange = _monster.chasingTime - 1f;
	}

	public override void OnStateUpdate()
	{
		if (_monster.nav.enabled)
		{
            _monster.nav.SetDestination(_monster.target.position);
        }

        FaceTarget();
	}

	public override void OnStateExit()
	{
        SoundManager.instance.StopSound("UrbonStep");

        _monster.animator.SetBool("Move", false);
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
