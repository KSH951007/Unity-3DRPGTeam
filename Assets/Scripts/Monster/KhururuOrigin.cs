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

		if (Input.GetKeyDown(KeyCode.S)/*카운터가 성공했다면*/)
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
	//		// TODO : 플레이어의 TakeHit 함수 호출, basicDamage, HitType.None 전달.
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

	//private void OnTriggerEnter(Collider other)
	//{
	//	if (other.CompareTag("PlayerSkill") && !isAwake)
	//	{
	//		isAwake = true;
	//	}

	//	// KhururuOrigin을 깨운 이후부터 데미지를 받음
	//	if (other.CompareTag("PlayerSkill") && isAwake)
	//	{
	//		// TODO : 플레이어의 공격에 따른 데미지를 파라미터로 받아와서 TakeHit함수 실행
	//		// var (damage, hitType) = Player.Attack();
	//		//TakeHit(damage, hitType, hitParticle);
	//	}
	//}
}
