using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhururuOrigin : BossMonsters
{
	protected override void OnEnable()
	{
		patience = 1;
		AppearAnimation();
	}

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
	}

	private IEnumerator StateControl()
	{
		while (!isDead)
		{
			switch (state)
			{
				case State.Idle:
					Idle();
					break;

				case State.Chase:
					yield return new WaitForSeconds(0.5f);
					Chase();
					break;

				case State.Attack:
					yield return new WaitForSeconds(0.5f);
					Attack();
					break;
			}
		}
	}

	private void Idle()
	{
		if (patience == 0)
		{
			state = State.Chase;
		}
	}

	private void Chase()
	{
		nav.SetDestination(target.position);

		if (nav.remainingDistance > 3f)
		{
			transform.position = nav.destination * moveSpeed * Time.deltaTime;
			animator.SetBool("Move", true);
		}
		else if (nav.remainingDistance > 1f && nav.remainingDistance <= 3f)
		{
			SetChasingTime();
			if (Time.time < chasingTime)
			{
				transform.position = nav.destination * moveSpeed * Time.deltaTime;
				animator.SetBool("Move", true);
			}
			else
			{
				state = State.Attack;
			}
		}
		else
		{
			state = State.Attack;
		}
	}

	public void Attack()
	{
		if (currentHp / maxHp > 0.8f && nav.remainingDistance < 0.1f)
		{
			animator.SetTrigger("Attack");
			// TODO : 플레이어의 TakeHit 함수 호출, basicDamage, HitType.None 전달.
		}
		else if (currentHp / maxHp > 0.3f && currentHp / maxHp < 0.8f && nav.remainingDistance < 1f)
		{
			animator.SetTrigger("Skill1");
		}
		else if (currentHp / maxHp < 0.3f)
		{
			animator.SetTrigger("Skill2");
		}
		else if (nav.remainingDistance > 2f)
		{
			animator.SetTrigger("Skill3");
		}

		state = State.Chase;
	}

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
