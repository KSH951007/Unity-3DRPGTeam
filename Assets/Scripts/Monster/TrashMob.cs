using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class TrashMob : MonoBehaviour
{
    private NavMeshAgent nav;
	private Transform target;
	private Animator animator;
	[SerializeField] private SphereCollider detectCollider;

	public bool isChase;
	public LayerMask attackTargetLayer;
	private bool isDead;

	public UnityEvent onDead;

	//private void Awake()
	//{
	//	nav = GetComponent<NavMeshAgent>();
	//	animator = GetComponent<Animator>();
	//}

	//void Update()
 //   {
	//	if (isDead)
	//	{
	//		nav.enabled = false;
	//	}

 //       if (nav.enabled)
	//	{
	//		nav.SetDestination(target.position);
	//		nav.isStopped = !isChase;
	//	}
 //   }

	//protected IEnumerator Die()
	//{
	//	animator.SetTrigger("Die");
	//	yield return new WaitForSeconds(2.5f);
	//	yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);
	//	gameObject.SetActive(false);
	//	onDead.Invoke();
	//	// TODO : ��� ������
	//}

	float HP;
	public float lostDistance;

	enum State
	{
		IDLE,
		CHASE,
		ATTACK,
		KILLED
	}

	State state;

	void Start()
	{
		animator = GetComponentInChildren<Animator>();
		nav = GetComponent<NavMeshAgent>();

		HP = 10;
		state = State.IDLE;
		StartCoroutine(StateMachine());
	}

	IEnumerator StateMachine()
	{
		while (HP > 0)
		{
			yield return StartCoroutine(state.ToString());
		}
	}

	IEnumerator IDLE()
	{
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
		var curAnimStateInfo = animator.GetCurrentAnimatorStateInfo(0);

		if (curAnimStateInfo.IsName("WalkFWD") == false)
		{
			animator.Play("WalkFWD", 0, 0);
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
			target = null;
			nav.SetDestination(transform.position);
			yield return null;
			// StateMachine �� ���� ����
			ChangeState(State.IDLE);
		}
		else
		{
			// WalkFWD �ִϸ��̼��� �� ����Ŭ ���� ���
			yield return new WaitForSeconds(curAnimStateInfo.length);
		}
	}

	IEnumerator ATTACK()
	{
		var curAnimStateInfo = animator.GetCurrentAnimatorStateInfo(0);

		// ���� �ִϸ��̼��� ���� �� Idle Battle �� �̵��ϱ� ������ 
		// �ڵ尡 �� ������ ���� ������ Attack �� Play
		animator.Play("Attack", 0, 0);

		// �Ÿ��� �־�����
		if (nav.remainingDistance > nav.stoppingDistance)
		{
			// StateMachine�� �������� ����
			ChangeState(State.CHASE);
		}
		else
			// ���� animation �� �� �踸ŭ ���
			// �� ��� �ð��� �̿��� ���� ������ ������ �� ����.
			yield return new WaitForSeconds(curAnimStateInfo.length * 2f);
	}

	IEnumerator KILLED()
	{
		yield return null;
	}

	void ChangeState(State newState)
	{
		state = newState;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.name != "Player") return;
		// Sphere Collider �� Player �� �����ϸ�      
		target = other.transform;
		// NavMeshAgent�� ��ǥ�� Player �� ����
		nav.SetDestination(target.position);
		// StateMachine�� �������� ����
		ChangeState(State.CHASE);
	}

	// TODO : ontrigger��� �̰� ���
	private void DetectTarget()
	{
		Vector3 collCenter = detectCollider.transform.position + detectCollider.center;

		if (Physics.OverlapSphere(collCenter, detectCollider.radius, attackTargetLayer).Length != 0)
		{
			Collider[] detectedColl =
				Physics.OverlapSphere(collCenter, detectCollider.radius, attackTargetLayer);

			target = detectedColl[0].transform;
		}

		nav.SetDestination(target.position);

		ChangeState(State.CHASE);
	}

	// Update is called once per frame
	void Update()
	{
		if (target == null) return;
		// target �� null �� �ƴϸ� target �� ��� ����
		nav.SetDestination(target.position);
	}

}
