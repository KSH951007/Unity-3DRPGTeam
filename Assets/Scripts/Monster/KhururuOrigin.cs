using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhururuOrigin : BossMonsters
{
	private bool isAwake = false;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A)) 
		{
			animator.SetTrigger("Skill3");
		}

		if (Input.GetKeyDown(KeyCode.S)/*ī���Ͱ� �����ߴٸ�*/)
		{
			animator.SetTrigger("Counter");
		}

		if(Input.GetMouseButtonDown(0))
		{
			TakeHit(1);
		}
	}

	private void StateControl()
	{
		while (!isDead)
		{
			switch (state)
			{
				case State.Idle:
					Idle();
					break;

				case State.Chase:
					Chase();
					break;

				case State.Attack:
					break;

			}
		}
	}

	private void Idle()
	{
		if (isAwake)
		{
			state = State.Chase;
		}
	}

	private void Chase()
	{
		if (nav.remainingDistance > 3f)
		{
			nav.SetDestination(target.position);
			rb.velocity = nav.destination * moveSpeed;
			animator.SetBool("Move", true);
		}
		else if (nav.remainingDistance > 1f && nav.remainingDistance <= 3f)
		{
			SetChasingTime();
			if (Time.time < chasingTime)
			{
				nav.SetDestination(target.position);
				rb.velocity = nav.destination * moveSpeed;
				animator.SetBool("Move", true);
			}
		}
		else
		{
			state = State.Attack;
		}
	}

	private void Attack()
	{

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

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("PlayerSkill") && !isAwake)
		{
			isAwake = true;
		}

		// KhururuOrigin�� ���� ���ĺ��� �������� ����
		if (other.CompareTag("PlayerSkill") && isAwake)
		{
			//TakeHit();
		}
	}
}
