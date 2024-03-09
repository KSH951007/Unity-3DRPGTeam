using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.VFX;

[RequireComponent(typeof(NavMeshAgent))]
public class BossMonsters : MonoBehaviour, IHitable
{
	public float maxHp;
	[SerializeField] public float currentHp;
	[SerializeField] protected float moveSpeed;
	[SerializeField] protected float exp;
	[SerializeField] public Transform target;
	[SerializeField] public float chasingTime;
	[SerializeField] protected Material takeHitMat;
	[SerializeField] public int patience;
	[SerializeField] public float timeForNextAttack;
	[SerializeField] public float timeForNextIdle;
    [SerializeField] public float timeForNextChange;
	[SerializeField] protected GameObject bossHpBarUI;
	private CapsuleCollider bossCollider;

    public float maxShieldAmount = 2500;
	public float curShieldAmount;

	protected GameObject[] dropItem;
	protected float dropCoin;

	[HideInInspector] public Animator animator;
    [HideInInspector] public NavMeshAgent nav;

	public SphereCollider detectColl;

	[Header("KhururuOrigin's Colliders")]
	[HideInInspector] public SphereCollider attack1Collider;
	[HideInInspector] public BoxCollider skill1Collider;
	[HideInInspector] public SphereCollider skill3Collider;
	[Space(10)]

	[Header("KhururuTrans's Colliders")]
	[HideInInspector] public SphereCollider t_attack1Collider;
	[HideInInspector] public SphereCollider t_attack2Collider;
	[HideInInspector] public SphereCollider t_skill1Collider;
	[HideInInspector] public SphereCollider t_skill2_1Collider;
	[HideInInspector] public MeshCollider t_skill2_2Collider;
	[HideInInspector] public SphereCollider t_skill3Collider;
	[HideInInspector] public MeshCollider t_skill4Collider;
	[Space(10)]

	[Header("Urbon's Colliders")]
	[HideInInspector] public SphereCollider u_attackCollider;
	[HideInInspector] public SphereCollider u_skill3Collider;
	[Space(10)]


	public LayerMask attackTargetLayer;
	protected SkinnedMeshRenderer skinnedMeshRenderer;
	protected SkinnedMeshRenderer currentMeshRenderer;

	[SerializeField] protected bool isStunned;
	[SerializeField] protected bool isDead;
	public bool hasAttacked;
	public bool shieldBroken;

	public UnityEvent onDead;

	[SerializeField] public GameObject hitParticle;

	protected void Awake()
	{
		animator = GetComponentInChildren<Animator>();
		nav = GetComponent<NavMeshAgent>();
		skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
		bossCollider = GetComponent<CapsuleCollider>();
	}

	protected virtual void OnEnable()
	{
		//animator.SetTrigger("Appear");
		nav.isStopped = true;
	}

	public float GetHp()
	{
		return currentHp / maxHp;
	}

	/// <summary>
	/// �����Ÿ� ���϶�� ������ �ð� ���ȸ� �÷��̾ �ѵ��� �ϱ� ���� �ð� ����
	/// </summary>
	/// <returns></returns>
	public void SetChasingTime()
	{
		chasingTime = Time.time + Random.Range(3f, 6f);
	}

	public void TakeHit(int damage, IHitable.HitType hitType, GameObject hitParticle = null)
	{
		bossHpBarUI.SetActive(true);

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
				if (curShieldAmount > 0)
				{
					curShieldAmount -= damage;
				}
				else
				{
					currentHp -= damage;
				}
				GameObject damageUI = PoolManager.Instance.Get("DamageFontUI");
                damageUI.GetComponent<DamageUI>().GetDamageFont(transform.position, damage);
                StartCoroutine(ChangeMat());
			}
			else if (currentHp - damage <= 0)
			{
				currentHp = 0;
				nav.enabled = false;
				bossCollider.enabled = false;
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
		nav.isStopped = true;
		animator.SetTrigger("Die");
		yield return new WaitForSeconds(2.5f);
		onDead.Invoke();
		gameObject.SetActive(false);
		//��� ������
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
