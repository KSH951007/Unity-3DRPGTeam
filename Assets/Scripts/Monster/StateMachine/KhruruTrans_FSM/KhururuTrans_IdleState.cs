using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhururuTrans_IdleState : BaseState
{
    public KhururuTrans_IdleState(BossMonsters monster) : base(monster) { }

    public override void OnStateEnter()
    {
		_monster.timeForNextChange = Time.time + 1f;

		_monster.nav.isStopped = true;
	}

	public override void OnStateUpdate()
	{
		SetTarget();
		FaceTarget();
	}

	public override void OnStateExit()
    {
		
    }


	private void SetTarget()
	{
		Vector3 collCenter = _monster.detectColl.transform.position + _monster.detectColl.center;
		if (Physics.OverlapSphere(collCenter, _monster.detectColl.radius, _monster.attackTargetLayer).Length >= 1 &&
				_monster.target == null)
		{
			Collider[] detectedColl =
			Physics.OverlapSphere(collCenter, _monster.detectColl.radius, _monster.attackTargetLayer);
			_monster.target = detectedColl[0].transform;
			//Debug.Log(detectedColl[0].name);
		}
		else
		{
			return;
		}
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
