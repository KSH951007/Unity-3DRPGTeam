using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Urbon : BossMonsters
{
	private void Update()
	{
		// skill3�� �̵��ϸ鼭 ���
		if (Input.GetKeyDown(KeyCode.O))
		{
			animator.SetTrigger("Skill3");
		}

		if (Input.GetKeyDown(KeyCode.P)/*�̵��Ÿ��� x �̻��̸�*/)
		{
			animator.SetTrigger("ExitSkill3");
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
