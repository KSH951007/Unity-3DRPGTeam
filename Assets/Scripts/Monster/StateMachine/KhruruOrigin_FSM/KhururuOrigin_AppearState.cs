using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class KhururuOrigin_AppearState : BaseState
{
	public KhururuOrigin_AppearState(BossMonsters monster) : base(monster) { }

	public override void OnStateEnter()
	{
		_monster.nav.isStopped = true;
	}

	public override void OnStateUpdate()
	{
		
	}

	public override void OnStateExit()
	{
		_monster.animator.SetTrigger("FirstHit");
		_monster.animator.SetTrigger("Trip");
	}
}
