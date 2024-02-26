using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSkillAction : HeroAction
{

    private Vector3 target;
    private Skill skill;
    private HeroAnimEvent animEvent;
    public HeroSkillAction(Skill skill, Animator animator, Hero owner) : base(animator, owner)
    {
        this.skill = skill;
        animEvent = animator.gameObject.GetComponent<HeroAnimEvent>();

    }

    public override bool IsCanle(HeroAction action)
    {
        return false;
    }

    public void SetTarget(Vector3 target)
    {
        this.target = target;
    }
    public override void StartAction()
    {
        isEndAction = false;
        animator.SetTrigger("Skill1");
        owner.StartCoroutine(owner.TargetToLoock(target, 0.05f));
        animEvent.onProgressAttack += skill.UseSkill;
    }

    public override void StopAction()
    {
        animEvent.onProgressAttack -= skill.UseSkill;
    }

    public override void UpdateAction()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName($"skill1") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            isEndAction = true;
            return;
        }
    }

}
