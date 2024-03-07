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
<<<<<<< HEAD
		timeForNextChange = Time.time + 3f;
=======
		timeForNextChange = Time.time + 0.5f;
>>>>>>> Sample
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
<<<<<<< HEAD
=======
					bossHpBarUI.SetActive(true);

>>>>>>> Sample
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
<<<<<<< HEAD
				if (hasAttacked && NextChangeCoolTime())
=======
				if (hasAttacked && NextChangeCoolTime() && nav.isStopped)
>>>>>>> Sample
				{
					ChangeState(State.Idle);
				}
				break;
		}

		_fsm.UpdateState();
		//print(_curState);
<<<<<<< HEAD
		//print(nav.remainingDistance);
=======
		//print(timeForNextChange);
>>>>>>> Sample
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

	#region State를 바꾸는 조건들

	private bool CanSeePlayer()
	{
		// TODO:: 플레이어 탐지 구현
		if (target != null)
		{
			return true;
		}
		else return false;

	}

	private bool ShortDistancePlayer()
	{
		// TODO:: 사정거리 체크 구현
<<<<<<< HEAD
		if (nav.remainingDistance < 2f)
=======
		if (nav.remainingDistance < 3f)
>>>>>>> Sample
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
