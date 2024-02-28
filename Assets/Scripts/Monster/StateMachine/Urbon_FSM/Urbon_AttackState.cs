using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Urbon_AttackState : BaseState
{
	public Urbon_AttackState(BossMonsters monster) : base(monster) { }

	//private float attackWeight = 0.25f;
	//private float skill1Weight = 0.25f;
	//private float skill2Weight = 0.25f;
	//private float skill3Weight = 0.25f;

	private float attackWeight = 0f;
	private float skill1Weight = 0f;
	private float skill2Weight = 0f;
	private float skill3Weight = 1f;
	private float totalWeight;

	private bool Onskill3;

	public override void OnStateEnter()
	{
		_monster.nav.isStopped = true;

		_monster.timeForNextChange = Time.time + 4f;

		totalWeight = attackWeight + skill1Weight + skill2Weight + skill3Weight;

		PlayRandomSkill();
	}

	public override void OnStateUpdate()
	{
		DetectSkillCollider();

		_monster.nav.SetDestination(_monster.target.position);

		if (Onskill3 && _monster.timeForNextChange - 3f < Time.time)
		{
			_monster.animator.SetTrigger("ExitSkill3");
			_monster.hasAttacked = true;
			Onskill3 = false;
		}
	}

	public override void OnStateExit()
	{
		_monster.hasAttacked = false;
	}

	private void PlayRandomSkill()
	{
		float randomValue = Random.Range(0f, totalWeight);

		if (randomValue < attackWeight)
		{
			_monster.animator.SetTrigger("Attack");
			_monster.hasAttacked = true;
		}
		else if (randomValue < attackWeight + skill1Weight)
		{
			_monster.animator.SetTrigger("Skill1");
			_monster.hasAttacked = true;
		}
		else if (randomValue < attackWeight + skill1Weight + skill2Weight)
		{
			_monster.animator.SetTrigger("Skill2");
			_monster.hasAttacked = true;
		}
		else
		{ 
			_monster.animator.SetTrigger("Skill3");
			Onskill3 = true;
			_monster.timeForNextChange += 6f;
		}
	}

	private void DetectSkillCollider()
	{
		if (_monster.u_attackCollider.enabled)
		{
			Vector3 collCenter = _monster.u_attackCollider.transform.position + _monster.u_attackCollider.center;

			Collider[] detectedColl =
			Physics.OverlapSphere(collCenter, _monster.u_attackCollider.radius, _monster.attackTargetLayer);
			Debug.Log("Attack 공격 실행");
			Debug.Log("피격 : " + detectedColl[0].name);
		}
		else if (_monster.u_skill3Collider.enabled)
		{
			Vector3 collCenter = _monster.u_skill3Collider.transform.position + _monster.u_skill3Collider.center;

			Collider[] detectedColl =
			Physics.OverlapSphere(collCenter, _monster.u_skill3Collider.radius, _monster.attackTargetLayer);
			Debug.Log("skill3 공격 실행");
			if (detectedColl.Length > 0)
			{
				Debug.Log("피격 : " + detectedColl[0].name);
			}
		}
		else
		{
			return;
		}
	}
}
