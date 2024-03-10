using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KhururuTrans_AttackState : BaseState
{
    public KhururuTrans_AttackState(BossMonsters monster) : base(monster) { }

	private int attack1Damage = 10;
	private int attack2Damage = 15;
	private int skill1Damage = 100;
	private int skill2Damage = 50;
	private int skill4Damage = 100;

	private float attack1Weight = 0.2f;
	private float attack2Weight = 0.2f;
	private float skill1Weight = 0.15f;
	private float skill2Weight = 0.15f;
	private float skill3Weight = 0.15f;
	private float skill4Weight = 0.15f;

    private float totalWeight;

	bool attacked = false;
	bool combo = false;
	private int attackCount = 0;
	private float skill4Range = 4.9f;
	private int sectorAngle = 70;
	private float comboTime;
	private bool triple;

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
		else if (combo && attackCount < 2 && comboTime < Time.time)
		{
			DetectSkillCollider();
		}
		else if (triple && comboTime < Time.time)
		{
            DetectSkillCollider();
        }

        SetTarget();
    }

	public override void OnStateExit()
	{
		_monster.hasAttacked = false;
		attacked = false;
		combo = false;
		attackCount = 0;
		triple = false;
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
			SoundManager.instance.PlaySound("TransAttack2");
			_monster.hasAttacked = true;
		}
		else if (randomValue < attack1Weight + attack2Weight + skill1Weight)
		{
			_monster.animator.SetTrigger("Skill1");
			SoundManager.instance.PlaySound("TransSkill1");
			_monster.hasAttacked = true;
		}
		else if (randomValue < attack1Weight + attack2Weight + skill1Weight + skill2Weight)
		{
			_monster.animator.SetTrigger("Skill2");
			SoundManager.instance.PlaySound("TransSkill2");
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
            SoundManager.instance.PlaySound("TransSkill4");
            _monster.hasAttacked = true;
		}
	}

	private void DetectSkillCollider()
	{
		if (_monster.t_attack1Collider.enabled)
		{
			SoundManager.instance.PlaySound("TransAttack1");
			Vector3 collCenter = _monster.t_attack1Collider.transform.position + _monster.t_attack1Collider.center;

			Collider[] detectedColl =
			Physics.OverlapSphere(collCenter, _monster.t_attack1Collider.radius, _monster.attackTargetLayer);
			if (detectedColl.Length != 0)
			{
				if (detectedColl[0].transform.gameObject.TryGetComponent(out IHitable health))
				{
					health.TakeHit(attack1Damage);
				}
			}
		}
		else if (_monster.t_attack2Collider.enabled)
		{
			Vector3 collCenter = _monster.t_attack2Collider.transform.position + _monster.t_attack2Collider.center;

			Collider[] detectedColl =
			Physics.OverlapSphere(collCenter, _monster.t_attack2Collider.radius, _monster.attackTargetLayer);
			if (detectedColl.Length != 0)
			{
				if (detectedColl[0].transform.gameObject.TryGetComponent(out IHitable health))
				{
					health.TakeHit(attack2Damage);
				}
			}
			comboTime = Time.time + 0.3f;
			attackCount++;
			combo = true;
		}
		else if (_monster.t_skill1Collider.enabled)
		{
			_monster.nav.velocity = Vector3.zero;
			Vector3 collCenter = _monster.t_skill1Collider.transform.position + _monster.t_skill1Collider.center;

			Collider[] detectedColl =
			Physics.OverlapSphere(collCenter, _monster.t_skill1Collider.radius, _monster.attackTargetLayer);
			if (detectedColl.Length != 0)
			{
				if (detectedColl[0].transform.gameObject.TryGetComponent(out IHitable health))
				{
					health.TakeHit(skill1Damage);
				}
			}
		}
		else if (_monster.t_skill2_1Collider.enabled && !combo)
		{
			Vector3 collCenter = _monster.t_skill2_1Collider.transform.position + _monster.t_skill2_1Collider.center;

			Collider[] detectedColl =
			Physics.OverlapSphere(collCenter, _monster.t_skill2_1Collider.radius, _monster.attackTargetLayer);
			if (detectedColl.Length != 0)
			{
				if (detectedColl[0].transform.gameObject.TryGetComponent(out IHitable health))
				{
					health.TakeHit(skill2Damage);
				}
			}
		}
		else if (_monster.t_skill2_2Collider.enabled)
		{
			Vector3 collCenter = _monster.t_skill2_2Collider.transform.position;

			Vector3 collHalfExtents = _monster.t_skill2_2Collider.bounds.extents * 2;

			Collider[] detectedColl =
			Physics.OverlapBox(collCenter, collHalfExtents, Quaternion.identity, _monster.attackTargetLayer);
			if (detectedColl.Length != 0)
			{
				if (detectedColl[0].transform.gameObject.TryGetComponent(out IHitable health))
				{
					health.TakeHit(skill2Damage);
				}
			}
			attackCount++;

		}
		else if (_monster.t_skill3Collider.enabled)
		{
			return;
		}
		else if (_monster.t_skill4Collider.enabled)
		{
			Vector3 dir = _monster.target.position - _monster.transform.position;
			float angle = Vector3.Angle(dir, _monster.transform.forward);
			if (dir.magnitude <= skill4Range && Mathf.Abs(angle) <= sectorAngle)
			{
				if(_monster.target.gameObject.TryGetComponent(out IHitable health))
				{
					health.TakeHit(skill4Damage);
				}
			}
            comboTime = Time.time + 0.1f;
            combo = true;
			attackCount++;
			if (attackCount == 2)
			{
				triple = true;
			}
		}
		else
		{
			return;
		}

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
