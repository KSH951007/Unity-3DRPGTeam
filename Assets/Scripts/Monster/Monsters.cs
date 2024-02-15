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

	protected bool isStunned;

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

	// ���Ͱ� Ư�� ��ų�� ����� �� �÷��̾ Ÿ�̹��� ���� (����)���ݿ� �����ϸ� ���� ���� ���¿� ����
	protected void CounterStart()
	{
		// TODO : ���� ������ ���� ��½�̸鼭 ī���� Ÿ�̹����� �˸��� �ð�ȿ��
		// TODO : ���� �� �տ� ī���Ͱ� �����ߴ��� ������ �� �ݶ��̴� �ѱ�
	}

	protected void CounterEnd()
	{
		// TODO : ī���� ���� �ݶ��̴� ����
	}
}
