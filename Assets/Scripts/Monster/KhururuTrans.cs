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
}
