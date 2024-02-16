using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Urbon : BossMonsters
{
	private void Update()
	{
		// skill3은 이동하면서 사용
		if (Input.GetKeyDown(KeyCode.O))
		{
			animator.SetTrigger("Skill3");
		}

		if (Input.GetKeyDown(KeyCode.P)/*이동거리가 x 이상이면*/)
		{
			animator.SetTrigger("ExitSkill3");
		}
	}
}
