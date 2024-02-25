using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class KhururuOrigin : BossMonsters
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
		base.OnEnable();
		maxShieldAmount = 15;
	}

	private void Start()
	{
		_curState = State.Appear;
		_fsm = new FSM(new KhururuOrigin_AppearState(this));
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			TakeHit(1, 0);
		}

		if (isDead)
		{
			_curState = State.None;
		}

		switch (_curState)
		{
			case State.Appear:
				if (GetFirstHit())
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
				if ((ShortDistancePlayer() || ChaseTimeOut()) && NextAttackCoolTime())
				{
					ChangeState(State.Attack);
				}
				else if (ShortDistancePlayer())
				{
					ChangeState(State.Idle);
				}

				break;

			case State.Attack:
				if ((hasAttacked || shieldBroken) && NextIdleCoolTime())
				{
					ChangeState(State.Idle);
				}
				break;
		}

		_fsm.UpdateState();
		//print(_curState);
	}

	private void ChangeState(State nextState)
	{
		_curState = nextState;
		switch (_curState)
		{
			case State.Appear:
				_fsm.ChangeState(new KhururuOrigin_AppearState(this));
				break;
			case State.Idle:
				_fsm.ChangeState(new KhururuOrigin_IdleState(this));
				break;
			case State.Chase:
				_fsm.ChangeState(new KhururuOrigin_ChaseState(this));
				break;
			case State.Attack:
				_fsm.ChangeState(new KhururuOrigin_AttackState(this));
				break;
		}
	}

	#region State�� �ٲٴ� ���ǵ�
	private bool GetFirstHit()
	{
		if (patience != 0)
		{
			return false;
		}
		else { return true; }
	}

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
		if (nav.remainingDistance < 1f)
		{
			return true;
		}
        else
        {
			return false;
        }
    }
	private bool ChaseTimeOut()
	{
		if (chasingTime < Time.time)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	private bool NextAttackCoolTime()
	{
		if (timeForNextAttack < Time.time)
		{
			return true;
		}
		else return false;
	}

	private bool NextIdleCoolTime()
	{
		if (timeForNextIdle < Time.time)
		{
			return true;
		}
		else return false;
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


    // ���Ͱ� Ư�� ��ų�� ����� �� �÷��̾ Ÿ�̹��� ���� (����)���ݿ� �����ϸ� ���� ���� ���¿� ����
    protected void CounterStart()
	{
		// TODO : ���� ������ ���� ��½�̸鼭 ī���� Ÿ�̹����� �˸��� �ð�ȿ��
		// TODO : ���� �� �տ� ī���Ͱ� �����ߴ��� ������ �� �ݶ��̴� �ѱ�
	}

	protected void CounterSuccess()
	{
		// TODO : isStunned = true;
		animator.SetTrigger("Counter");
		isStunned = true;
	}

	protected void CounterEnd()
	{
		// TODO : ī���� ���� �ݶ��̴� ����
	}
}
