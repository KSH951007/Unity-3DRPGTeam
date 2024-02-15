using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhururuOrigin : Monsters
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
