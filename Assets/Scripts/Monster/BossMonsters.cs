using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class BossMonsters : MonoBehaviour
{
	[SerializeField] protected float maxHp;
	[SerializeField] protected float currentHp;
	[SerializeField] protected float basicDamage;
	[SerializeField] protected float moveSpeed;
	[SerializeField] protected float exp;
	[SerializeField] protected Transform target;
	[SerializeField] protected float chasingTime;
	[SerializeField] protected Material takeHitMat;

	protected GameObject[] dropItem;
	protected float dropCoin;

	protected Animator animator;
	protected Rigidbody rb;
	protected NavMeshAgent nav;
	protected SkinnedMeshRenderer skinnedMeshRenderer;
	protected SkinnedMeshRenderer currentMeshRenderer;
	//protected Coroutine usingSkill;

	[SerializeField] protected bool isMelee;
	[SerializeField] protected bool isStunned;
	[SerializeField] protected bool playerFound;
	[SerializeField] protected bool isChase;
	[SerializeField] protected bool isAttack;
	[SerializeField] protected bool isDead;

	public GameObject[] hitParticles;
	

	protected enum State
	{
		Idle,
		Chase,
		Attack
	}

	protected enum HitType
	{
		None,
		Stagger,
		Trip
	}

	protected State state;
	protected HitType hitType;

	protected void Awake()
	{
		animator = GetComponentInChildren<Animator>();
		rb = GetComponent<Rigidbody>();
		nav = GetComponent<NavMeshAgent>();
		skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
		state = State.Idle;
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

	/// <summary>
	/// �����Ÿ� ���϶�� ������ �ð� ���ȸ� �÷��̾ �ѵ��� �ϱ� ���� �ð� ����
	/// </summary>
	/// <returns></returns>
	protected void SetChasingTime()
	{
		chasingTime = Time.time + Random.Range(1f, 3f);
	}

	protected void TakeHit(int damage, GameObject hitParticle = null)
	{
		if (hitParticle != null)
		{
			// TODO : bool property �̸� ����
			hitParticle.GetComponent<VisualEffect>().SetBool("Hit", true);
		}

		if (currentHp - damage > 0)
		{
			currentHp -= damage;
			StartCoroutine(ChangeMat());
		}
		else if (currentHp - damage <= 0)
		{
			currentHp = 0;
			isDead = true;
			Die();
		}
	}

	protected IEnumerator ChangeMat()
	{
		Material originalMat = skinnedMeshRenderer.material;
		skinnedMeshRenderer.material = takeHitMat;
		yield return new WaitForSeconds(0.1f);
		skinnedMeshRenderer.material = originalMat;
	}

	protected void Die()
	{
		animator.SetTrigger("Die");
	}

	/// <summary>
	/// �������͸� ������ �����۰� ������ ����Ʈ���� �Լ�
	/// </summary>
	/// <param name="dropCoin">�� ���������� ��������</param>
	/// <returns></returns>
	protected float DropItem(float dropCoin)
	{
		foreach(GameObject items in dropItem)
		{
			Instantiate(items);
		}

		return dropCoin;
	}
}
