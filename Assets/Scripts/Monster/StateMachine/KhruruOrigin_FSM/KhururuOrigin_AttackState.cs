using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class KhururuOrigin_AttackState : BaseState
{
	public KhururuOrigin_AttackState(BossMonsters monster) : base(monster) { }

    //private float attackWeight = 0.5f;
    //private float skill1Weight = 0.35f;
    //private float skill2Weight = 0.05f;
    //private float skill3Weight = 0.1f;

	private float attackWeight = 0f;
	private float skill1Weight = 0f;
	private float skill2Weight = 0f;
	private float skill3Weight = 1f;

	private float totalWeight;

	bool shieldOn;
	bool attacked = false;

	public override void OnStateEnter()
	{
        _monster.nav.isStopped = true;

        _monster.timeForNextIdle = Time.time + 2f;

        _monster.timeForNextAttack = Time.time + 3f;

		if (_monster.GetHp() < 0.9f && _monster.GetHp() > 0.4f)
		{
			attackWeight = 0.4f;
			skill1Weight = 0.4f;
			skill2Weight = 0.1f;
			skill3Weight = 0.1f;
		}
		else if (_monster.GetHp() < 0.4f)
		{
			attackWeight = 0.2f;
			skill1Weight = 0.3f;
			skill2Weight = 0.4f;
			skill3Weight = 0.1f;
		}

		totalWeight = attackWeight + skill1Weight + skill2Weight + skill3Weight;

		PlayRandomSkill();
	}

	public override void OnStateUpdate()
	{
		ShieldBroken();
		if (!attacked)
		{
            DetectSkillCollider();
        }
    }

	public override void OnStateExit()
	{
        _monster.hasAttacked = false;
        _monster.shieldBroken = false;
        _monster.animator.SetBool("ShieldBroken", false);

		attacked = false;
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
			_monster.curShieldAmount = _monster.maxShieldAmount;
			_monster.currentHp += _monster.maxShieldAmount;
			shieldOn = true;
		}
		else
		{
			_monster.animator.SetTrigger("Skill3");
			_monster.hasAttacked = true;
		}
	}

	public void ShieldBroken()
	{
		if (shieldOn && _monster.curShieldAmount / _monster.maxShieldAmount <= 0f)
		{
			_monster.shieldBroken = true;
			_monster.animator.SetBool("ShieldBroken", true);
			shieldOn = false;
		}
	}

    private void DetectSkillCollider()
    {
        if (_monster.attack1Collider.enabled)
        {
            Vector3 collCenter = _monster.attack1Collider.transform.position + _monster.attack1Collider.center;

            Collider[] detectedColl =
            Physics.OverlapSphere(collCenter, _monster.attack1Collider.radius, _monster.attackTargetLayer);
            Debug.Log("Attack1 공격 실행");
            //Debug.Log("피격 : " + detectedColl[0].name);
        }
        else if (_monster.skill1Collider.enabled)
        {
            Vector3 collCenter = _monster.skill1Collider.transform.position + _monster.skill1Collider.center;

            Collider[] detectedColl =
            Physics.OverlapBox(collCenter, _monster.skill1Collider.center, Quaternion.identity, _monster.attackTargetLayer);
            Debug.Log("Skill1 공격 실행");
            //Debug.Log("피격 : " + detectedColl[0].name);
        }
        else if (_monster.skill3Collider.enabled)
        {
            Vector3 collCenter = _monster.skill3Collider.transform.position + _monster.skill3Collider.center;

            Collider[] detectedColl =
            Physics.OverlapSphere(collCenter, _monster.skill3Collider.radius, _monster.attackTargetLayer);
            Debug.Log("Skill3 공격 실행");
            //Debug.Log("피격 : " + detectedColl[0].name);
        }
        else
        {
            return;
        }

		attacked = true;
    }
}
