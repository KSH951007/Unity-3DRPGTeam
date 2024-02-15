using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhururuTrans : Monsters
{
	private void Awake()
	{
		animator = GetComponentInChildren<Animator>();
	}

	private void OnEnable()
	{
		AppearAnimation();
	}

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
