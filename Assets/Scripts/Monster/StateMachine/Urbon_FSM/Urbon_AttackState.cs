using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Urbon_AttackState : BaseState
{
	public Urbon_AttackState(BossMonsters monster) : base(monster) { }

	private int attackDamage;

	private float attackWeight = 0.3f;
	private float skill1Weight = 0.3f;
	private float skill2Weight = 0.3f;
	private float skill3Weight = 0.1f;

    private float totalWeight;

	private bool Onskill3;
	private bool combo;
	private bool attacked;
	private int attackCount = 0;

	private float comboTime;

	public override void OnStateEnter()
	{
		attackDamage = 40;

		_monster.nav.isStopped = true;

		_monster.timeForNextChange = Time.time + 4f;

		totalWeight = attackWeight + skill1Weight + skill2Weight + skill3Weight;

		PlayRandomSkill();
	}

	public override void OnStateUpdate()
	{
		SetTarget();

		if (!attacked)
		{
			DetectSkillCollider();
		}
		else if (combo && attackCount < 2 && comboTime < Time.time)
		{
			DetectSkillCollider();
		}

		if (_monster.nav.enabled)
		{
            _monster.nav.SetDestination(_monster.target.position);
        }

        if (Onskill3 && _monster.timeForNextChange < Time.time && !_monster.hasAttacked)
		{
			_monster.animator.SetTrigger("ExitSkill3");
			_monster.timeForNextChange += 2.50f;
			_monster.hasAttacked = true;
			Onskill3 = false;
		}
	}

	public override void OnStateExit()
	{
		attacked = false;
		attackCount = 0;
		combo = false;
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
			_monster.timeForNextChange += 2f;
		}
	}

	private void DetectSkillCollider()
	{
		if (_monster.u_attackCollider.enabled)
		{
			Vector3 collCenter = _monster.u_attackCollider.transform.position + _monster.u_attackCollider.center;

			Collider[] detectedColl =
			Physics.OverlapSphere(collCenter, _monster.u_attackCollider.radius, _monster.attackTargetLayer);
			if (detectedColl.Length != 0)
			{
				if (detectedColl[0].transform.gameObject.TryGetComponent(out IHitable health))
				{
					health.TakeHit(attackDamage);
				}
			}
		}
		else
		{
			return;
		}

		comboTime = Time.time + 0.3f;
        combo = true;
        attackCount++;
        attacked = true;
	}

	private void SetTarget()
	{
		Vector3 collCenter = _monster.detectColl.transform.position + _monster.detectColl.center;
		if (Physics.OverlapSphere(collCenter, _monster.detectColl.radius, _monster.attackTargetLayer).Length >= 1)
		{
			Collider[] detectedColl =
			Physics.OverlapSphere(collCenter, _monster.detectColl.radius, _monster.attackTargetLayer);
			_monster.target = detectedColl[0].transform;
			//Debug.Log(detectedColl[0].name);
		}
		else
		{
			return;
		}
	}
}
