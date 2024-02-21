using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhururuOrigin : BossMonsters
{
	protected override void OnEnable()
	{
		patience = 1;
		AppearAnimation();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A)) 
		{
			animator.SetTrigger("Attack");
		}

		if (Input.GetKeyDown(KeyCode.S)/*ī���Ͱ� �����ߴٸ�*/)
		{
			animator.SetTrigger("Counter");
		}

		if(Input.GetMouseButtonDown(0))
		{
			TakeHit(1, 0);
		}
	}

	private IEnumerator StateControl()
	{
		while (!isDead)
		{
			switch (state)
			{
				case State.Idle:
					Idle();
					break;

				case State.Chase:
					yield return new WaitForSeconds(0.5f);
					Chase();
					break;

				case State.Attack:
					yield return new WaitForSeconds(0.5f);
					Attack();
					break;
			}
		}
	}

	private void Idle()
	{
		if (patience == 0)
		{
			state = State.Chase;
		}
	}

	private void Chase()
	{
		nav.SetDestination(target.position);

		if (nav.remainingDistance > 3f)
		{
			transform.position = nav.destination * moveSpeed * Time.deltaTime;
			animator.SetBool("Move", true);
		}
		else if (nav.remainingDistance > 1f && nav.remainingDistance <= 3f)
		{
			SetChasingTime();
			if (Time.time < chasingTime)
			{
				transform.position = nav.destination * moveSpeed * Time.deltaTime;
				animator.SetBool("Move", true);
			}
			else
			{
				state = State.Attack;
			}
		}
		else
		{
			state = State.Attack;
		}
	}

	public void Attack()
	{
		if (currentHp / maxHp > 0.8f && nav.remainingDistance < 0.1f)
		{
			animator.SetTrigger("Attack");
			// TODO : �÷��̾��� TakeHit �Լ� ȣ��, basicDamage, HitType.None ����.
		}
		else if (currentHp / maxHp > 0.3f && currentHp / maxHp < 0.8f && nav.remainingDistance < 1f)
		{
			animator.SetTrigger("Skill1");
		}
		else if (currentHp / maxHp < 0.3f)
		{
			animator.SetTrigger("Skill2");
		}
		else if (nav.remainingDistance > 2f)
		{
			animator.SetTrigger("Skill3");
		}

		state = State.Chase;
	}

	private IEnumerator Skill1()
	{
		yield return null;
	}
	private IEnumerator Skill2()
	{
		yield return null;
	}
	private IEnumerator Skill3()
	{
		yield return null;
	}
	private IEnumerator Skill4()
	{
		yield return null;
	}

	// ���Ͱ� Ư�� ��ų�� ����� �� �÷��̾ Ÿ�̹��� ���� (����)���ݿ� �����ϸ� ���� ���� ���¿� ����
	protected void CounterStart()
	{
		// TODO : ���� ������ ���� ��½�̸鼭 ī���� Ÿ�̹����� �˸��� �ð�ȿ��
		// TODO : ���� �� �տ� ī���Ͱ� �����ߴ��� ������ �� �ݶ��̴� �ѱ�
	}

	protected void CounterSuccess()
	{
		// TODO : isStunned = true;
		animator.SetTrigger("Counter");
		isStunned = true;
	}

	protected void CounterEnd()
	{
		// TODO : ī���� ���� �ݶ��̴� ����
	}

	//private void OnTriggerEnter(Collider other)
	//{
	//	if (other.CompareTag("PlayerSkill") && !isAwake)
	//	{
	//		isAwake = true;
	//	}

	//	// KhururuOrigin�� ���� ���ĺ��� �������� ����
	//	if (other.CompareTag("PlayerSkill") && isAwake)
	//	{
	//		// TODO : �÷��̾��� ���ݿ� ���� �������� �Ķ���ͷ� �޾ƿͼ� TakeHit�Լ� ����
	//		// var (damage, hitType) = Player.Attack();
	//		//TakeHit(damage, hitType, hitParticle);
	//	}
	//}
}
