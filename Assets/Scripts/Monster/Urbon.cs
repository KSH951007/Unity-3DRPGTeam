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
}
