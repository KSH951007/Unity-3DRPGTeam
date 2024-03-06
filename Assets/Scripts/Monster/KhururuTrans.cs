using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhururuTrans : BossMonsters
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

    private void Start()
    {
        timeForNextChange = Time.time + 3f;
        _curState = State.Appear;
        _fsm = new FSM(new KhururuTrans_AppearState(this));
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
					bossHpBarUI.SetActive(true);

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
				_fsm.ChangeState(new KhururuTrans_IdleState(this));
				break;
			case State.Idle:
                _fsm.ChangeState(new KhururuTrans_IdleState(this));
                break;
            case State.Chase:
                _fsm.ChangeState(new KhururuTrans_ChaseState(this));
                break;
            case State.Attack:
                _fsm.ChangeState(new KhururuTrans_AttackState(this));
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
        if (nav.remainingDistance < 2f)
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

    // 몬스터가 특정 스킬을 사용할 때 플레이어가 타이밍을 맞춰 (스턴)공격에 성공하면 몬스터 스턴 상태에 돌입
    protected void CounterStart()
	{
		// TODO : 몬스터 몸에서 빛이 번쩍이면서 카운터 타이밍임을 알리는 시각효과
		// TODO : 몬스터 몸 앞에 카운터가 성공했는지 판정을 할 콜라이더 켜기
	}

	protected void CounterSuccess()
	{
		// TODO : isStunned = true;
		animator.SetTrigger("Counter");
		isStunned = true;
	}

	protected void CounterEnd()
	{
		// TODO : 카운터 판정 콜라이더 끄기
	}
}
