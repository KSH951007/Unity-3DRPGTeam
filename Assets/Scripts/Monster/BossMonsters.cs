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

	// 몬스터가 특정 스킬을 사용할 때 플레이어가 타이밍을 맞춰 (스턴)공격에 성공하면 몬스터 스턴 상태에 돌입
	protected void CounterStart()
	{
		// TODO : 몬스터 몸에서 빛이 번쩍이면서 카운터 타이밍임을 알리는 시각효과
		// TODO : 몬스터 몸 앞에 카운터가 성공했는지 판정을 할 콜라이더 켜기
	}

	protected void CounterEnd()
	{
		// TODO : 카운터 판정 콜라이더 끄기
	}
}
