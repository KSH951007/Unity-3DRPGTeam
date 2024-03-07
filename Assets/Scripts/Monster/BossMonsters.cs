using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.VFX;

[RequireComponent(typeof(NavMeshAgent))]
<<<<<<< HEAD
public class BossMonsters : MonoBehaviour, IHitable_Monster
{
	[SerializeField] protected float maxHp;
	[SerializeField] public float currentHp;
	public float basicDamage;
=======
public class BossMonsters : MonoBehaviour, IHitable
{
	public float maxHp;
	[SerializeField] public float currentHp;
>>>>>>> Sample
	[SerializeField] protected float moveSpeed;
	[SerializeField] protected float exp;
	[SerializeField] public Transform target;
	[SerializeField] public float chasingTime;
	[SerializeField] protected Material takeHitMat;
	[SerializeField] public int patience;
	[SerializeField] public float timeForNextAttack;
	[SerializeField] public float timeForNextIdle;
    [SerializeField] public float timeForNextChange;
<<<<<<< HEAD

	
    public float maxShieldAmount = 15;
=======
	[SerializeField] protected GameObject bossHpBarUI;
	private Collider bossCollider;

    public float maxShieldAmount = 2500;
>>>>>>> Sample
	public float curShieldAmount;

	protected GameObject[] dropItem;
	protected float dropCoin;

	[HideInInspector] public Animator animator;
    [HideInInspector] public NavMeshAgent nav;

	public SphereCollider detectColl;

	[Header("KhururuOrigin's Colliders")]
<<<<<<< HEAD
    public SphereCollider attack1Collider;
    public BoxCollider skill1Collider;
	public SphereCollider skill3Collider;
	[Space(10)]

	[Header("KhururuTrans's Colliders")]
	public SphereCollider t_attack1Collider;
	public SphereCollider t_attack2Collider;
	public SphereCollider t_skill1Collider;
	public SphereCollider t_skill2_1Collider;
	public MeshCollider t_skill2_2Collider;
	public SphereCollider t_skill3Collider;
	public MeshCollider t_skill4Collider;
	[Space(10)]

	[Header("Urbon's Colliders")]
	public SphereCollider u_attackCollider;
	public SphereCollider u_skill3Collider;
	[Space(10)]

	[Header("ForQuest")]
	public UnityEvent ondead;
=======
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

>>>>>>> Sample

	public LayerMask attackTargetLayer;
	protected SkinnedMeshRenderer skinnedMeshRenderer;
	protected SkinnedMeshRenderer currentMeshRenderer;

	[SerializeField] protected bool isStunned;
	[SerializeField] protected bool isDead;
	public bool hasAttacked;
	public bool shieldBroken;

<<<<<<< HEAD
=======
	public UnityEvent onDead;

>>>>>>> Sample
	[SerializeField] public GameObject hitParticle;

	protected void Awake()
	{
		animator = GetComponentInChildren<Animator>();
		nav = GetComponent<NavMeshAgent>();
		skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
<<<<<<< HEAD
=======
		bossCollider = GetComponent<Collider>();
>>>>>>> Sample
	}

	protected virtual void OnEnable()
	{
<<<<<<< HEAD
		animator.SetTrigger("Appear");
=======
		//animator.SetTrigger("Appear");
		nav.isStopped = true;
>>>>>>> Sample
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

<<<<<<< HEAD
	public void TakeHit(float damage, HitType hitType, GameObject hitParticle = null)
	{
=======
	public void TakeHit(int damage, IHitable.HitType hitType, GameObject hitParticle = null)
	{
		bossHpBarUI.SetActive(true);

>>>>>>> Sample
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
<<<<<<< HEAD
				StartCoroutine(ChangeMat());
=======
                GameObject damageUI = PoolManager.Instance.Get("DamageFontUI");
                damageUI.GetComponent<DamageUI>().GetDamageFont(transform.position, damage);
                StartCoroutine(ChangeMat());
>>>>>>> Sample
			}
			else if (currentHp - damage <= 0)
			{
				currentHp = 0;
<<<<<<< HEAD
				isDead = true;
				StartCoroutine(Die());
			
=======
				bossCollider.enabled = false;
				isDead = true;
				StartCoroutine(Die());
>>>>>>> Sample
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
<<<<<<< HEAD
        ondead.Invoke(); // 유니티 이벤트 ondead 발생

        //드랍 아이템
    }

    /// <summary>
    /// 보스몬스터를 잡으면 아이템과 코인을 떨어트리는 함수
    /// </summary>
    /// <param name="dropCoin">각 보스몬스터의 보상코인</param>
    /// <returns></returns>
    protected float DropItem(float dropCoin)
=======
		onDead.Invoke();
		//드랍 아이템
	}

	/// <summary>
	/// 보스몬스터를 잡으면 아이템과 코인을 떨어트리는 함수
	/// </summary>
	/// <param name="dropCoin">각 보스몬스터의 보상코인</param>
	/// <returns></returns>
	protected float DropItem(float dropCoin)
>>>>>>> Sample
	{
		foreach(GameObject items in dropItem)
		{
			Instantiate(items, transform.position, Quaternion.identity);
		}

		return dropCoin;
	}
<<<<<<< HEAD

	// KhururuTrans의 skill4 범위 확인
	//private void OnDrawGizmos()
	//{
	//	Gizmos.color = Color.red;
	//	if (t_skill4Collider != null)
	//	{
	//		Vector3 temp = t_skill4Collider.bounds.center + new Vector3(0, 0, 2);
	//		Gizmos.DrawWireCube(t_skill4Collider.transform.position + temp, t_skill4Collider.bounds.extents * 1.6f);
	//	}
	//}
=======
>>>>>>> Sample
}
