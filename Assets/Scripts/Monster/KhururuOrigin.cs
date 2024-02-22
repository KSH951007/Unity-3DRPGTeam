using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class KhururuOrigin : BossMonsters
{
	//protected void OnEnable()
	//{
	//	base.OnEnable();
	//	patience = 1;
	//	nav.isStopped = true;
	//	StartCoroutine(StateControl());
	//}

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
				if (CanSeePlayer() && NextChaseCoolTime())
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
				nav.isStopped = true;
				if (hasAttacked || shieldBroken)
				{
					ChangeState(State.Idle);
					hasAttacked = false;
					shieldBroken = false;
					animator.SetBool("ShieldBroken", false);
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

	private bool NextChaseCoolTime()
	{
		if (timeForNextChase < Time.time)
		{
			return true;
		}
		else return false;
	}

	//public void HasAttacked()
	//{
	//	hasAttacked = true;
	//	// �� �Լ��� skill�ִϸ��̼� event�� �߰�
	//}

	//public void ShieldBroken()
	//{
	//       if (curShieldAmount / maxShieldAmount <= 0)
	//       {
	//           shieldBroken = true;
	//		animator.SetBool("ShieldBroken", true);
	//       }
	//   }
	#endregion


	//private void Update()
	//{
	//	if (Input.GetKeyDown(KeyCode.A)) 
	//	{
	//		animator.SetTrigger("Attack");
	//	}

	//	if (Input.GetKeyDown(KeyCode.S)/*ī���Ͱ� �����ߴٸ�*/)
	//	{
	//		animator.SetTrigger("Counter");
	//	}

	//	if(Input.GetMouseButtonDown(0))
	//	{
	//		TakeHit(1, 0);
	//	}

	//	//if (nextState != currentState.DoState(this))
	//	//{
	//	//	currentState = nextState;
	//	//}
	//   }

	//private IEnumerator StateControl()
	//{
	//       AppearAnimation();

	//       while (!isDead)
	//	{
	//		switch (currentState)
	//		{
	//			case State.Appear:

	//				nav.isStopped = true;
	//				AfterAppear();
	//				yield return null;

	//				break;

	//			case State.Idle:

	//                   Idle();
	//                   yield return new WaitForSeconds(1f);

	//                   break;

	//			case State.Chase:

	//                   StartCoroutine(Chase());
	//                   yield return new WaitForSeconds(1f);

	//                   break;

	//			case State.Attack:

	//				nav.isStopped = true;
	//				StartCoroutine(Attack());
	//                   yield return new WaitForSeconds(1f);

	//                   break;
	//		}

	//		print(currentState);
	//		print(nav.remainingDistance);
	//	}

	//       yield return null;
	//   }

	//   protected void AfterAppear()
	//   {
	//	if (patience == 1)
	//	{
	//		currentState = State.Appear;
	//	}
	//	else
	//	{
	//           animator.SetTrigger("FirstHit");
	//		animator.SetTrigger("Trip");
	//		currentState = State.Idle;
	//       }
	//   }

	//   private void Idle()
	//{
	//	if (target != null)
	//	{
	//		currentState = State.Chase;
	//	}
	//}

	//private IEnumerator Chase()
	//{
	//       nav.SetDestination(target.position);

	//       if (nav.remainingDistance > 3f)
	//	{
	//           nav.isStopped = false;
	//           MoveAnimation();
	//		yield return new WaitForSeconds(3f);
	//	}
	//	else if (nav.remainingDistance > 1f && nav.remainingDistance <= 3f)
	//	{
	//		SetChasingTime();
	//		if (Time.time < chasingTime)
	//		{
	//               nav.isStopped = false;
	//               MoveAnimation();
	//               yield return new WaitForSeconds(chasingTime - Time.time);
	//           }
	//	}

	//	yield return new WaitForSeconds(0.1f);
	//       animator.SetBool("Move", false);
	//       nav.isStopped = true;
	//       nav.velocity = Vector3.zero;
	//	currentState = State.Attack;
	//   }

	//public IEnumerator Attack()
	//{
	//	if (currentHp / maxHp > 0.8f && nav.remainingDistance < 0.1f)
	//	{
	//		animator.SetTrigger("Attack");
	//		// TODO : �÷��̾��� TakeHit �Լ� ȣ��, basicDamage, HitType.None ����.
	//	}
	//	else if (currentHp / maxHp > 0.3f && currentHp / maxHp < 0.8f && nav.remainingDistance < 1f)
	//	{
	//		animator.SetTrigger("Skill1");
	//	}
	//	else if (currentHp / maxHp < 0.3f && nav.remainingDistance < 2f)
	//	{
	//		animator.SetTrigger("Skill2");
	//	}
	//	else if (nav.remainingDistance > 2f)
	//	{
	//		animator.SetTrigger("Skill3");
	//	}

	//       yield return new WaitForSeconds(1f);

	//       currentState = State.Chase;
	//}

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
