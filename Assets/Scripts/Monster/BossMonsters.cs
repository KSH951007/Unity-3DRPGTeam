using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

[RequireComponent(typeof(NavMeshAgent))]
public class BossMonsters : MonoBehaviour, IHitable
{
	[SerializeField] protected float maxHp;
	[SerializeField] public float currentHp;
	public float basicDamage;
	[SerializeField] protected float moveSpeed;
	[SerializeField] protected float exp;
	[SerializeField] public Transform target;
	[SerializeField] public float chasingTime;
	[SerializeField] protected Material takeHitMat;
	[SerializeField] public int patience;
	[SerializeField] public float timeForNextAttack;
	[SerializeField] public float timeForNextChase;

	public float maxShieldAmount = 15;
	public float curShieldAmount;

	protected GameObject[] dropItem;
	protected float dropCoin;

	[HideInInspector] public Animator animator;
    [HideInInspector] public NavMeshAgent nav;
	public SphereCollider detectColl;
	public LayerMask attackTargetLayer;
	protected SkinnedMeshRenderer skinnedMeshRenderer;
	protected SkinnedMeshRenderer currentMeshRenderer;
	//protected Coroutine currentSkillCoroutine;
	//protected Coroutine usingSkill;

	[SerializeField] protected bool isMelee;
	[SerializeField] protected bool isStunned;
	[SerializeField] protected bool playerFound;
	[SerializeField] protected bool isChase;
	[SerializeField] protected bool isAttack;
	[SerializeField] protected bool isDead;
	public bool hasAttacked;
	public bool shieldBroken;

	[SerializeField] public GameObject hitParticle;

	protected void Awake()
	{
		animator = GetComponentInChildren<Animator>();
		nav = GetComponent<NavMeshAgent>();
		detectColl = transform.Find("DetectRange").GetComponent<SphereCollider>();
		skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
	}

	protected virtual void OnEnable()
	{
		animator.SetTrigger("Appear");
	}

	public float GetHp()
	{
		return currentHp / maxHp;
	}

	/// <summary>
	/// 일정거리 이하라면 랜덤한 시간 동안만 플레이어를 쫓도록 하기 위한 시간 설정
	/// </summary>
	/// <returns></returns>
	public void SetChasingTime()
	{
		chasingTime = Time.time + Random.Range(3f, 6f);
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
				// TODO : bool property 이름 변경
				hitParticle.GetComponent<VisualEffect>().SetBool("Hit", true);
			}

			if (currentHp - damage > 0)
			{
				if (curShieldAmount > 0)
				{
					curShieldAmount--;
				}
				currentHp -= damage;
				StartCoroutine(ChangeMat());
			}
			else if (currentHp - damage <= 0)
			{
				currentHp = 0;
				isDead = true;
				StartCoroutine(Die());
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

	protected IEnumerator Die()
	{
		animator.SetTrigger("Die");
		yield return new WaitForSeconds(2.5f);
		gameObject.SetActive(false);
		//드랍 아이템
	}

	/// <summary>
	/// 보스몬스터를 잡으면 아이템과 코인을 떨어트리는 함수
	/// </summary>
	/// <param name="dropCoin">각 보스몬스터의 보상코인</param>
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
