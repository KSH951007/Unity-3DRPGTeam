using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhururuTrans : BossMonsters
{
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			animator.SetTrigger("Skill4");
		}

		if (Input.GetKeyDown(KeyCode.W)/*카운터가 성공했다면*/)
		{
			animator.SetTrigger("Counter");
		}
	}

	private void StateControl()
	{
		while (!isDead)
		{
			switch (state)
			{
				case State.Idle:
					break;
				case State.Chase:
					break;
				case State.Attack:
					break;

			}
		}
	}

	private void Idle()
	{
		if (playerFound)
		{
			state = State.Chase;
		}
	}

	private void Chase()
	{

	}

	private void Attack()
	{

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
}
