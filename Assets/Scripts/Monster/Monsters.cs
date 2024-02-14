using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monsters : MonoBehaviour
{
	protected float maxHp;
	protected float currentHp;
	protected float basicDamage;
	protected float moveSpeed;
	protected float exp;

	protected GameObject[] dropItem;
	protected float dropCoin;

	protected Animator animator;

	protected enum State
	{
		Idle,
		Chase,
		Attack,
		RunAway
	}

	protected void AppearAnimation()
	{
		animator.SetTrigger("Appear");
	}

	protected void MoveAnimation()
	{
		animator.SetBool("Move", true);
	}
}
