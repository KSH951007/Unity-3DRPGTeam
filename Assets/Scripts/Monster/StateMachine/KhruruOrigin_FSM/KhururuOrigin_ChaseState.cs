using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhururuOrigin_ChaseState : BaseState
{
	public KhururuOrigin_ChaseState(BossMonsters monster) : base(monster) { }

	public override void OnStateEnter()
	{
		_monster.nav.SetDestination(_monster.target.position);
		_monster.nav.isStopped = false;
		//_monster.nav.
		_monster.animator.SetBool("Move", true);
		_monster.SetChasingTime();
	}

	public override void OnStateUpdate()
	{

	}

	public override void OnStateExit()
	{
		_monster.nav.isStopped = true;
		_monster.nav.velocity = Vector3.zero;
		_monster.animator.SetBool("Move", false);
	}
}
