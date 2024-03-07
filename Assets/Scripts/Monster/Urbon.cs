using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Urbon : BossMonsters
{
	private enum State
	{
		None,
		Appear,
		Idle,
		Chase,
		Attack
	}

	private State _curState;
	private FSM _fsm;

	protected override void OnEnable()
	{
		timeForNextChange = Time.time + 3f;
		_curState = State.Appear;
		_fsm = new FSM(new Urbon_AppearState(this));
	}

	private void Update()
	{
		if (isDead)
		{
			nav.isStopped = true;
			_curState = State.None;
		}

		switch (_curState)
		{
			case State.Appear:
				if (NextChangeCoolTime())
				{
					ChangeState(State.Idle);
				}
				break;

			case State.Idle:
				if (CanSeePlayer() && NextChangeCoolTime())
				{
					ChangeState(State.Chase);
				}
				break;

			case State.Chase:
				if (NextChangeCoolTime())
				{
					ChangeState(State.Attack);
				}
				else if (ShortDistancePlayer())
				{
					ChangeState(State.Attack);
				}
				break;

			case State.Attack:
				if (hasAttacked && NextChangeCoolTime())
				{
					ChangeState(State.Idle);
				}
				break;
		}

		_fsm.UpdateState();
		//print(_curState);
		//print(nav.remainingDistance);
	}

	private void ChangeState(State nextState)
	{
		_curState = nextState;
		switch (_curState)
		{
			case State.Appear:
				_fsm.ChangeState(new Urbon_IdleState(this));
				break;
			case State.Idle:
				_fsm.ChangeState(new Urbon_IdleState(this));
				break;
			case State.Chase:
				_fsm.ChangeState(new Urbon_ChaseState(this));
				break;
			case State.Attack:
				_fsm.ChangeState(new Urbon_AttackState(this));
				break;
		}
	}

	#region State�� �ٲٴ� ���ǵ�

	private bool CanSeePlayer()
	{
		// TODO:: �÷��̾� Ž�� ����
		if (target != null)
		{
			return true;
		}
		else return false;

	}

	private bool ShortDistancePlayer()
	{
		// TODO:: �����Ÿ� üũ ����
		if (nav.remainingDistance < 2f)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	private bool NextChangeCoolTime()
	{
		if (timeForNextChange < Time.time)
		{
			return true;
		}
		else return false;
	}
	#endregion
}
