using System.Collections;
using System.Collections.Generic;
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

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A)) 
		{
			animator.SetTrigger("Attack");
		}

		if (Input.GetKeyDown(KeyCode.S)/*ī���Ͱ� �����ߴٸ�*/)
		{
			animator.SetTrigger("Counter");
		}

		if(Input.GetMouseButtonDown(0))
		{
			TakeHit(1, 0);
		}

        currentState = currentState.DoState(this);
    }

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

	private IEnumerator Skill1()
	{
		yield return null;
	}
	private IEnumerator Skill2()
	{
		yield return null;
	}
	private IEnumerator Skill3()
	{
		yield return null;
	}
	private IEnumerator Skill4()
	{
		yield return null;
	}

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

	//private void OnTriggerEnter(Collider other)
	//{
	//	if (other.CompareTag("PlayerSkill") && !isAwake)
	//	{
	//		isAwake = true;
	//	}

	//	// KhururuOrigin�� ���� ���ĺ��� �������� ����
	//	if (other.CompareTag("PlayerSkill") && isAwake)
	//	{
	//		// TODO : �÷��̾��� ���ݿ� ���� �������� �Ķ���ͷ� �޾ƿͼ� TakeHit�Լ� ����
	//		// var (damage, hitType) = Player.Attack();
	//		//TakeHit(damage, hitType, hitParticle);
	//	}
	//}
}
