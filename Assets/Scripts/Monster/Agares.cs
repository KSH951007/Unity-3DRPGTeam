using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agares : BossMonsters
{
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Z))
		{
			animator.SetTrigger("Skill4");
		}

		if (Input.GetKeyDown(KeyCode.X)/*플레이어의 체력이 최대 체력의 10프로 이하이면*/)
		{
			animator.SetTrigger("HpPropotionalAttack");
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
}
