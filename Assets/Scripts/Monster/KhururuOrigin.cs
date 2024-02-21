using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhururuOrigin : BossMonsters
{
	private bool isAwake = false;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A)) 
		{
			animator.SetTrigger("Skill3");
		}

		if (Input.GetKeyDown(KeyCode.S)/*카운터가 성공했다면*/)
		{
			animator.SetTrigger("Counter");
		}

		if(Input.GetMouseButtonDown(0))
		{
			TakeHit(1);
		}
	}

	private void StateControl()
	{
		while (!isDead)
		{
			switch (state)
			{
				case State.Idle:
					Idle();
					break;

				case State.Chase:
					Chase();
					break;

				case State.Attack:
					break;

			}
		}
	}

	private void Idle()
	{
		if (isAwake)
		{
			state = State.Chase;
		}
	}

	private void Chase()
	{
		if (nav.remainingDistance > 3f)
		{
			nav.SetDestination(target.position);
			rb.velocity = nav.destination * moveSpeed;
			animator.SetBool("Move", true);
		}
		else if (nav.remainingDistance > 1f && nav.remainingDistance <= 3f)
		{
			SetChasingTime();
			if (Time.time < chasingTime)
			{
				nav.SetDestination(target.position);
				rb.velocity = nav.destination * moveSpeed;
				animator.SetBool("Move", true);
			}
		}
		else
		{
			state = State.Attack;
		}
	}

	private void Attack()
	{

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

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("PlayerSkill") && !isAwake)
		{
			isAwake = true;
		}

		// KhururuOrigin을 깨운 이후부터 데미지를 받음
		if (other.CompareTag("PlayerSkill") && isAwake)
		{
			//TakeHit();
		}
	}
}
