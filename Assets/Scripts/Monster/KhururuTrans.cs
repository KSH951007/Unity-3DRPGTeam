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
}
