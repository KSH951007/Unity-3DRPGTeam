using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsters : MonoBehaviour
{
	[SerializeField] protected float maxHp;
	[SerializeField] protected float currentHp;
	[SerializeField] protected float basicDamage;
	[SerializeField] protected float moveSpeed;
	[SerializeField] protected float exp;
	[SerializeField] protected Transform target;

	protected GameObject[] dropItem;
	protected float dropCoin;

	protected Animator animator;
	protected Rigidbody rb;

	[SerializeField] protected bool isMelee;
	[SerializeField] protected bool isStunned;
	[SerializeField] protected bool isChase;
	[SerializeField] protected bool isAttack;
	[SerializeField] protected bool isDead;

	protected enum State
	{
		Idle,
		Chase,
		Attack,
		RunAway
	}

	protected void Awake()
	{
		animator = GetComponentInChildren<Animator>();
		rb = GetComponent<Rigidbody>();
	}

	protected void OnEnable()
	{
		AppearAnimation();
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
