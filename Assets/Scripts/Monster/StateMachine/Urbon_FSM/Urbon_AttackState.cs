using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Urbon_AttackState : BaseState
{
	public Urbon_AttackState(BossMonsters monster) : base(monster) { }

	//private float attackWeight = 0.25f;
	//private float skill1Weight = 0.25f;
	//private float skill2Weight = 0.25f;
	//private float skill3Weight = 0.25f;

	private float attackWeight = 0f;
	private float skill1Weight = 1f;
	private float skill2Weight = 0f;
	private float skill3Weight = 0f;

	private float totalWeight;

	bool attacked = false;
	bool combo = false;

	public override void OnStateEnter()
	{
		_monster.nav.isStopped = true;

		_monster.timeForNextChange = Time.time + 4f;

		totalWeight = attackWeight + skill1Weight + skill2Weight + skill3Weight;

		PlayRandomSkill();
	}

	public override void OnStateUpdate()
	{
		if (!attacked)
		{
			DetectSkillCollider();
		}
		else if (combo)
		{
			DetectSkillCollider();
		}
	}

	public override void OnStateExit()
	{
		_monster.hasAttacked = false;
		attacked = false;
		combo = false;
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
			_monster.timeForNextChange = Time.time + 4f;
			_monster.hasAttacked = true;
		}
	}

	private void DetectSkillCollider()
	{
		if (_monster.t_attack1Collider.enabled)
		{
			Vector3 collCenter = _monster.t_attack1Collider.transform.position + _monster.t_attack1Collider.center;

			Collider[] detectedColl =
			Physics.OverlapSphere(collCenter, _monster.t_attack1Collider.radius, _monster.attackTargetLayer);
			Debug.Log("Attack1 공격 실행");
			//Debug.Log("피격 : " + detectedColl[0].name);
		}
		else if (_monster.t_attack2Collider.enabled)
		{
			Vector3 collCenter = _monster.t_attack2Collider.transform.position + _monster.t_attack2Collider.center;

			Collider[] detectedColl =
			Physics.OverlapSphere(collCenter, _monster.t_attack2Collider.radius, _monster.attackTargetLayer);
			Debug.Log("Attack2 공격 실행");
			//Debug.Log("피격 : " + detectedColl[0].name);
		}
		else if (_monster.t_skill1Collider.enabled)
		{
			Vector3 collCenter = _monster.t_skill1Collider.transform.position + _monster.t_skill1Collider.center;

			Collider[] detectedColl =
			Physics.OverlapSphere(collCenter, _monster.t_skill1Collider.radius, _monster.attackTargetLayer);
			Debug.Log("Skill1 공격 실행");
			//Debug.Log("피격 : " + detectedColl[0].name);
		}
		else if (_monster.t_skill2_1Collider.enabled && !combo)
		{
			Vector3 collCenter = _monster.t_skill2_1Collider.transform.position + _monster.t_skill2_1Collider.center;

			Collider[] detectedColl =
			Physics.OverlapSphere(collCenter, _monster.t_skill2_1Collider.radius, _monster.attackTargetLayer);
			Debug.Log("Skill2_1 공격 실행");
			combo = true;
			//Debug.Log("피격 : " + detectedColl[0].name);
		}
		else if (_monster.t_skill2_2Collider.enabled)
		{
			Vector3 collCenter = _monster.t_skill2_2Collider.transform.position;

			Vector3 collHalfExtents = _monster.t_skill2_2Collider.bounds.extents * 2;

			Collider[] detectedColl =
			Physics.OverlapBox(collCenter, collHalfExtents, Quaternion.identity, _monster.attackTargetLayer);
			Debug.Log("Skill2_2 공격 실행");
			//Debug.Log("피격 : " + detectedColl[0].name);
		}
		else if (_monster.t_skill3Collider.enabled)
		{
			Debug.Log("Skill3 공격 실행");
			//Debug.Log("피격 : " + detectedColl[0].name);
		}
		else if (_monster.t_skill4Collider.enabled)
		{
			Vector3 collCenter = _monster.t_skill4Collider.transform.position + _monster.t_skill4Collider.bounds.center + new Vector3(0, 0, 2);

			Vector3 collHalfExtents = _monster.t_skill4Collider.bounds.extents * 1.6f;

			Collider[] detectedColl =
			Physics.OverlapBox(collCenter, collHalfExtents, Quaternion.identity, _monster.attackTargetLayer);
			Debug.Log("Skill4 공격 실행");
			Debug.Log("피격 : " + detectedColl[0].name);
			combo = true;
		}
		else
		{
			return;
		}

		attacked = true;
	}
}
