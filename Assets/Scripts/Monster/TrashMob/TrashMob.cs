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
	[SerializeField] protected Transform spawnedPoint;        // Chase�ϴٰ� �÷��̾ ��ġ�� �� ó�� ��ġ�� �ǵ��ư���
	[SerializeField] private GameObject hpBarUI;
	CapsuleCollider mobCollider;

	public LayerMask attackTargetLayer;
    private bool cancelWait;
	private bool invincible;

    [HideInInspector] public UnityEvent onDead;

	public float maxHp;
	public float currentHp;
	public int damage;
	public float lostDistance;		// lostDistance�� �ݵ�� DetectRange���� �ְ� ����

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
        // ���� animator �������� ���
        var curAnimStateInfo = animator.GetCurrentAnimatorStateInfo(0);

		// �ִϸ��̼� �̸��� IdleNormal �� �ƴϸ� Play
		if (curAnimStateInfo.IsName("IdleNormal") == false)
			animator.Play("IdleNormal", 0, 0);

		// ���Ͱ� Idle ������ �� �θ��� �Ÿ��� �ϴ� �ڵ�
		// 50% Ȯ���� ��/��� ���� ����
		int dir = Random.Range(0f, 1f) > 0.5f ? 1 : -1;

		// ȸ�� �ӵ� ����
		float lookSpeed = Random.Range(25f, 40f);

		// IdleNormal ��� �ð� ���� ���ƺ���
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
			// SetDestination �� ���� �� frame�� �ѱ������ �ڵ�
			yield return null;
		}

		// ��ǥ������ ���� �Ÿ��� ���ߴ� �������� �۰ų� ������
		if (nav.remainingDistance <= nav.stoppingDistance)
		{
			// StateMachine �� �������� ����
			ChangeState(State.ATTACK);
		}
		// ��ǥ���� �Ÿ��� �־��� ���
		else if (nav.remainingDistance > lostDistance)
		{
			// �÷��̾�� �Ÿ��� �־����� ���� ���·� �����ϰ� ���� �ڸ��� ������ �ǵ��ư�
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
			// StateMachine �� ���� ����
			ChangeState(State.IDLE);
		}
		else
		{
			// WalkFWD �ִϸ��̼��� �� ����Ŭ ���� ���
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
        // ���� �ִϸ��̼��� ���� �� Idle Battle �� �̵��ϱ� ������ 
        // �ڵ尡 �� ������ ���� ������ Attack �� Play
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("TakeHit") == false);

        animator.Play("Attack", -1, 0);

		// �Ÿ��� �־�����
		if (nav.remainingDistance > nav.stoppingDistance)
		{
			// StateMachine�� �������� ����
			ChangeState(State.CHASE);
		}
		else
			// ���� animation �� �� �踸ŭ ���
			// �� ��� �ð��� �̿��� ���� ������ ������ �� ����.
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

	// ���� ������ ���� player�� target���� ����
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
            // TODO : ����! �̶�� �۾� ����
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
