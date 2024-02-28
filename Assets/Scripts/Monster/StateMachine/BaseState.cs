using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public abstract class BaseState
{
	protected BossMonsters _monster;

	protected BaseState(BossMonsters monster)
	{
		_monster = monster;
	}

	public abstract void OnStateEnter();
	public abstract void OnStateUpdate();
	public abstract void OnStateExit();
}
