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

		if (Input.GetKeyDown(KeyCode.W)/*ī���Ͱ� �����ߴٸ�*/)
		{
			animator.SetTrigger("Counter");
		}
	}
}
