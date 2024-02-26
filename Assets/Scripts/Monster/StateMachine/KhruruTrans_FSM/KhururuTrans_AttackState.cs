using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhururuTrans_AttackState : BaseState
{
    public KhururuTrans_AttackState(BossMonsters monster) : base(monster) { }

	private float attack1Weight = 0.2f;
	private float attack2Weight = 0.2f;
	private float skill1Weight = 0.15f;
	private float skill2Weight = 0.15f;
	private float skill3Weight = 0.15f;
	private float skill4Weight = 0.15f;

    private float totalWeight;

	bool attacked = false;

	public override void OnStateEnter()
    {
        _monster.nav.isStopped = true;

        _monster.timeForNextChange = Time.time + 4f;

		totalWeight = attack1Weight + attack2Weight + skill1Weight + skill2Weight + skill3Weight + skill4Weight;

		PlayRandomSkill();
	}

	public override void OnStateUpdate()
    {
		if (!attacked)
		{
			DetectSkillCollider();
		}
	}

	public override void OnStateExit()
	{
		_monster.hasAttacked = false;
		attacked = false;
	}

	private void PlayRandomSkill()
	{
		float randomValue = Random.Range(0f, totalWeight);

		if (randomValue < attack1Weight)
		{
			_monster.animator.SetTrigger("Attack1");
			_monster.hasAttacked = true;
		}
		else if (randomValue < attack1Weight + attack2Weight)
		{
			_monster.animator.SetTrigger("Attack2");
			_monster.hasAttacked = true;
		}
		else if (randomValue < attack1Weight + attack2Weight + skill1Weight)
		{
			_monster.animator.SetTrigger("Skill1");
			_monster.hasAttacked = true;
		}
		else if (randomValue < attack1Weight + attack2Weight + skill1Weight + skill2Weight)
		{
			_monster.animator.SetTrigger("Skill2");
			_monster.hasAttacked = true;
		}
		else if (randomValue < attack1Weight + attack2Weight + skill1Weight + skill2Weight + skill3Weight)
		{
			_monster.animator.SetTrigger("Skill3");
			_monster.hasAttacked = true;
		}
		else
		{
			_monster.animator.SetTrigger("Skill4");
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
		else if (_monster.t_skill2_1Collider.enabled)
		{
			Vector3 collCenter = _monster.t_skill2_1Collider.transform.position + _monster.t_skill2_1Collider.center;

			Collider[] detectedColl =
			Physics.OverlapSphere(collCenter, _monster.t_skill2_1Collider.radius, _monster.attackTargetLayer);
			Debug.Log("Skill2_1 공격 실행");
			//Debug.Log("피격 : " + detectedColl[0].name);
		}
		else if (_monster.t_skill2_2Collider.enabled)
		{
			Vector3 collCenter = _monster.t_skill2_2Collider.transform.position;

			Vector3 collHalfExtents = _monster.t_skill2_2Collider.bounds.extents;

			Collider[] detectedColl =
			Physics.OverlapBox(collCenter, collHalfExtents, Quaternion.identity, _monster.attackTargetLayer);
			Debug.Log("Skill2_2 공격 실행");
			//Debug.Log("피격 : " + detectedColl[0].name);
		}
		else if (_monster.t_skill3Collider.enabled)
		{
			Vector3 collCenter = _monster.t_skill3Collider.transform.position + _monster.t_skill3Collider.center;

			Collider[] detectedColl =
			Physics.OverlapSphere(collCenter, _monster.t_skill3Collider.radius, _monster.attackTargetLayer);
			Debug.Log("Skill3 공격 실행");
			//Debug.Log("피격 : " + detectedColl[0].name);
		}
		else if (_monster.t_skill4Collider.enabled)
		{
			Vector3 collCenter = _monster.t_skill4Collider.transform.position;

			Vector3 collHalfExtents = _monster.t_skill4Collider.bounds.extents;

			Collider[] detectedColl =
			Physics.OverlapBox(collCenter, collHalfExtents, Quaternion.identity, _monster.attackTargetLayer);
			Debug.Log("Skill4 공격 실행");
			//Debug.Log("피격 : " + detectedColl[0].name);
		}
		else
		{
			return;
		}

		attacked = true;
	}
}
