using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhururuOrigin : BossMonsters
{	
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A)) 
		{
			animator.SetTrigger("Skill3");
		}

		if (Input.GetKeyDown(KeyCode.S)/*ī���Ͱ� �����ߴٸ�*/)
		{
			animator.SetTrigger("Counter");
		}
	}
}
