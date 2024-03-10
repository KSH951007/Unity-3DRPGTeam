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
        timeForNextChange = Time.time + 0.5f;
        _curState = State.Appear;
        _fsm = new FSM(new KhururuTrans_AppearState(this));
    }

	private void Update()
    {
        if (isDead)
        {
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
    protected override IEnumerator Die()
    {
        SoundManager.instance.StopSound("TransStep");
        SoundManager.instance.PlaySound("TransDieVoice");
        SoundManager.instance.PlaySound("TransDie");
        yield return StartCoroutine(base.Die());
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