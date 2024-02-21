using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class BossMonsters : MonoBehaviour, IHitable
{
	[SerializeField] protected float maxHp;
	[SerializeField] protected float currentHp;
	public float basicDamage;
	[SerializeField] protected float moveSpeed;
	[SerializeField] protected float exp;
	[SerializeField] protected Transform target;
	[SerializeField] protected float chasingTime;
	[SerializeField] protected Material takeHitMat;
	[SerializeField] protected int patience = 0;

	protected GameObject[] dropItem;
	protected float dropCoin;

	protected Animator animator;
	protected NavMeshAgent nav;
	protected SkinnedMeshRenderer skinnedMeshRenderer;
	protected SkinnedMeshRenderer currentMeshRenderer;
	protected Coroutine currentSkillCoroutine;
	//protected Coroutine usingSkill;

	[SerializeField] protected bool isMelee;
	[SerializeField] protected bool isStunned;
	[SerializeField] protected bool playerFound;
	[SerializeField] protected bool isChase;
	[SerializeField] protected bool isAttack;
	[SerializeField] protected bool isDead;

	[SerializeField] public GameObject hitParticle;
	

	protected enum State
	{
		Idle,
		Chase,
		Attack
	}

	protected State state;

	protected void Awake()
	{
		animator = GetComponentInChildren<Animator>();
		nav = GetComponent<NavMeshAgent>();
		skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
		state = State.Idle;
	}

	protected virtual void OnEnable()
	{
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
		chasingTime = Time.time + Random.Range(0f, 3f);
	}

	public void TakeHit(float damage, IHitable.HitType hitType, GameObject hitParticle = null)
	{
        if (patience != 0)
        {
			patience--;
			return;
        }

        if (patience == 0)
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
			Instantiate(items, transform.position, Quaternion.identity);
		}

		return dropCoin;
	}
}
