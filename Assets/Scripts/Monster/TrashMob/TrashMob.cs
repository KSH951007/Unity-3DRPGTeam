using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class TrashMob : MonoBehaviour, IHitable
{
    protected NavMeshAgent nav;
	private Transform target;
	private Animator animator;
	private float moveSpeed = 2f;
	private float comebackSpeed = 10f;
	[SerializeField] private SphereCollider detectCollider;
	[SerializeField] private SphereCollider attackCollider;
	[SerializeField] protected Transform spawnedPoint;        // Chase하다가 플레이어를 놓치면 맨 처음 위치로 되돌아가기
	[SerializeField] private GameObject hpBarUI;
	CapsuleCollider mobCollider;

	public LayerMask attackTargetLayer;
    private bool cancelWait;
	private bool invincible;

    [HideInInspector] public UnityEvent onDead;

	public float maxHp;
	public float currentHp;
	public int damage;
	public float lostDistance;		// lostDistance는 반드시 DetectRange보다 멀게 설정

	enum State
	{
		IDLE,
		CHASE,
		ATTACK,
		KILLED
	}

	State state;

	protected virtual void Start()
	{		
		mobCollider = GetComponent<CapsuleCollider>();
		animator = GetComponent<Animator>();
		nav = GetComponent<NavMeshAgent>();

		currentHp = maxHp;
		state = State.IDLE;
        StartCoroutine(StateMachine());
	}

    IEnumerator StateMachine()
	{
        while (state != State.KILLED)
        {
            yield return StartCoroutine(state.ToString());
        }

        yield return StartCoroutine(State.KILLED.ToString());
    }

    IEnumerator CancelableWait(float time)
    {
        cancelWait = false;
        while (time > 0 && cancelWait == false)
        {
            time -= Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator IDLE()
	{
        nav.isStopped = true;
        // 현재 animator 상태정보 얻기
        var curAnimStateInfo = animator.GetCurrentAnimatorStateInfo(0);

		// 애니메이션 이름이 IdleNormal 이 아니면 Play
		if (curAnimStateInfo.IsName("IdleNormal") == false)
			animator.Play("IdleNormal", 0, 0);

		// 몬스터가 Idle 상태일 때 두리번 거리게 하는 코드
		// 50% 확률로 좌/우로 돌아 보기
		int dir = Random.Range(0f, 1f) > 0.5f ? 1 : -1;

		// 회전 속도 설정
		float lookSpeed = Random.Range(25f, 40f);

		// IdleNormal 재생 시간 동안 돌아보기
		for (float i = 0; i < curAnimStateInfo.length; i += Time.deltaTime)
		{
			transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y + (dir) * Time.deltaTime * lookSpeed, 0f);

			yield return null;
		}
	}

	IEnumerator CHASE()
	{
		nav.isStopped = false;
		var curAnimStateInfo = animator.GetCurrentAnimatorStateInfo(0);

		if (curAnimStateInfo.IsName("WalkFWD") == false)
		{
			animator.Play("WalkFWD", -1, 0);
			// SetDestination 을 위해 한 frame을 넘기기위한 코드
			yield return null;
		}

		// 목표까지의 남은 거리가 멈추는 지점보다 작거나 같으면
		if (nav.remainingDistance <= nav.stoppingDistance)
		{
			// StateMachine 을 공격으로 변경
			ChangeState(State.ATTACK);
		}
		// 목표와의 거리가 멀어진 경우
		else if (nav.remainingDistance > lostDistance)
		{
			// 플레이어와 거리가 멀어지면 무적 상태로 돌입하고 원래 자리로 빠르게 되돌아감
			invincible = true;
			target = spawnedPoint;
			//yield return null;
			nav.stoppingDistance = 0;
			nav.speed = comebackSpeed;
			yield return new WaitUntil(() => nav.remainingDistance < 0.001f);

			target = null;
            nav.SetDestination(transform.position);
			nav.speed = moveSpeed;
            nav.stoppingDistance = 1.5f;
			invincible = false;
			hpBarUI.SetActive(false);
			// StateMachine 을 대기로 변경
			ChangeState(State.IDLE);
		}
		else
		{
			// WalkFWD 애니메이션의 한 사이클 동안 대기
			//yield return new WaitForSeconds(curAnimStateInfo.length);
            yield return StartCoroutine(CancelableWait(0.5f));
        }
	}

	IEnumerator ATTACK()
	{
		nav.isStopped = true;
		nav.velocity = Vector3.zero;
		var curAnimStateInfo = animator.GetCurrentAnimatorStateInfo(0);
		hpBarUI.SetActive(true);
        // 공격 애니메이션은 공격 후 Idle Battle 로 이동하기 때문에 
        // 코드가 이 지점에 오면 무조건 Attack 을 Play
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("TakeHit") == false);

        animator.Play("Attack", -1, 0);

		// 거리가 멀어지면
		if (nav.remainingDistance > nav.stoppingDistance)
		{
			// StateMachine을 추적으로 변경
			ChangeState(State.CHASE);
		}
		else
			// 공격 animation 의 두 배만큼 대기
			// 이 대기 시간을 이용해 공격 간격을 조절할 수 있음.
			//yield return new WaitForSeconds(curAnimStateInfo.length * 2f);
			yield return StartCoroutine(CancelableWait(curAnimStateInfo.length * 2f));
    }

	IEnumerator KILLED()
	{
        nav.isStopped = true;
        animator.Play("Die", -1, 0);
		onDead.Invoke();
        yield return new WaitForSeconds(5f);
		gameObject.SetActive(false);
	}

	void ChangeState(State newState)
	{
		state = newState;
	}

	// 감시 범위에 들어온 player를 target으로 설정
	private void DetectTarget()
	{
		if (state != State.KILLED)
		{
            Vector3 collCenter = detectCollider.transform.position + detectCollider.center;

            if (Physics.OverlapSphere(collCenter, detectCollider.radius, attackTargetLayer).Length != 0)
            {
                Collider[] detectedColl =
                    Physics.OverlapSphere(collCenter, detectCollider.radius, attackTargetLayer);

                target = detectedColl[0].transform;
            }
        }

		if (target != null)
		{
            nav.SetDestination(target.position);

            ChangeState(State.CHASE);
        }
	}

    private void FixedUpdate()
    {
        DetectTarget();
		
    }

    // Update is called once per frame
    private void Update()
    {
        if (target == null) return;

        if (state == State.ATTACK)
            transform.LookAt(target);
    }

    public void TakeHit(int damage, IHitable.HitType hitType, GameObject hitParticle = null)
    {
        if (!invincible)
        {
            GameObject damageUI = PoolManager.Instance.Get("DamageFontUI");
            damageUI.GetComponent<DamageUI>().GetDamageFont(transform.position, (int)damage);

            hpBarUI.SetActive(true);

            currentHp -= damage;

            if (currentHp <= 0)
            {
                target = null;
				nav.enabled = false;
				mobCollider.enabled = false;
				cancelWait = true;
                ChangeState(State.KILLED);
            }
            else
                animator.Play("TakeHit", -1, 0);
        }
		else
		{
            // TODO : 무적! 이라는 글씨 띄우기
            GameObject damageUI = PoolManager.Instance.Get("DamageFontUI");
            damageUI.GetComponent<DamageUI>().GetDamageFont(transform.position, 0);
        }
    }

    protected virtual void AnimHit()
    {
        if (target == null) return;
		else
		{
			Vector3 collCenter = attackCollider.transform.position + attackCollider.center;

			Collider[] detectedColl =
			Physics.OverlapSphere(collCenter, attackCollider.radius, attackTargetLayer);
			if (detectedColl.Length != 0)
			{
				if (detectedColl[0].transform.gameObject.TryGetComponent(out IHitable health))
				{
					health.TakeHit(damage);
				}
			}
		}
    }

	void StopWhileTakeHit()
	{
		nav.isStopped = true;
	}
}
