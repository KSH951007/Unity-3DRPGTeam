using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class KhururuOrigin_AttackState : BaseState
{
	public KhururuOrigin_AttackState(BossMonsters monster) : base(monster) { }

	private float attackWeight = 0.5f;
	private float skill1Weight = 0.3f;
	private float skill2Weight = 0.1f;
	private float skill3Weight = 0.1f;

	private float totalWeight;

	bool shieldOn;

	public override void OnStateEnter()
	{
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
	}

	public override void OnStateExit()
	{
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
}
