using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IBossMonsterState
{
    private float attackWeight = 0.6f;
    private float skill1Weight = 0.2f;
    private float skill2Weight = 0.1f;
    private float skill3Weight = 0.1f;

    private float totalWeight;

    AnimationClip[] animationClips;

    public IBossMonsterState DoState(KhururuOrigin monster)
    {
        monster.nav.isStopped = true;

        animationClips = monster.animator.runtimeAnimatorController.animationClips;

        SetCondition(monster);

        PlayRandomSkill(monster);

        return monster.chaseState;
    }

    private void SetCondition(KhururuOrigin monster)
    {
        if (monster.GetHp() < 0.9f && monster.GetHp() > 0.4f)
        {
            attackWeight = 0.4f;
            skill1Weight = 0.4f;
            skill2Weight = 0.1f;
            skill3Weight = 0.1f;
        }
        else if (monster.GetHp() < 0.4f)
        {
            attackWeight = 0.2f;
            skill1Weight = 0.3f;
            skill2Weight = 0.4f;
            skill3Weight = 0.1f;
        }

        totalWeight = attackWeight + skill1Weight + skill2Weight + skill3Weight;
    }

    private void PlayRandomSkill(KhururuOrigin monster)
    {
        float randomValue = Random.Range(0f, totalWeight);

        if (randomValue < attackWeight)
        {
            monster.animator.SetTrigger("Attack");
        }
        else if (randomValue < attackWeight + skill1Weight)
        {
            monster.animator.SetTrigger("Skill1");
        }
        else if (randomValue < attackWeight + skill1Weight + skill2Weight)
        {
            monster.animator.SetTrigger("Skill2");
        }
        else
        {
            monster.animator.SetTrigger("Skill3");
        }
    }

}
