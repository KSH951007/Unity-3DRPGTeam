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

		if (Input.GetKeyDown(KeyCode.X)/*�÷��̾��� ü���� �ִ� ü���� 10���� �����̸�*/)
		{
			animator.SetTrigger("HpPropotionalAttack");
		}
	}
}