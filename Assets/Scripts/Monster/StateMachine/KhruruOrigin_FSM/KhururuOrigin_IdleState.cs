using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class KhururuOrigin_IdleState : BaseState
{
	public KhururuOrigin_IdleState(BossMonsters monster) : base(monster) { }

	public override void OnStateEnter()
	{
        _monster.timeForNextChange = Time.time + 2f;

        _monster.nav.isStopped = true;
	}

	public override void OnStateUpdate()
	{
		SetTarget();
	}

	public override void OnStateExit()
	{
		
	}

	private void SetTarget()
	{
		Vector3 collCenter = _monster.detectColl.transform.position + _monster.detectColl.center;
		if (Physics.OverlapSphere(collCenter, _monster.detectColl.radius, _monster.attackTargetLayer).Length >= 1)
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
}
