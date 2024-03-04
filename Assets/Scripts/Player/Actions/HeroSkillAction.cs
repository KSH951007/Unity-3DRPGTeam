using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSkillAction : HeroAction
{

    private Vector3 target;
    private SkillManager skillmanager;
    private HeroAnimEvent animEvent;
    private int skillIndex;

    public HeroSkillAction(ActionScheduler scheduler, SkillManager skillmanager, int skillIndex, Animator animator, Hero owner) : base(scheduler, animator, owner)
    {
        this.skillmanager = skillmanager;
        this.skillIndex = skillIndex;
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
            
        animator.SetTrigger($"Skill{skillIndex + 1}");
        animEvent.onProgressAttack += UseSkill;
        animEvent.onEndAttack += EndAction;
        owner.StartCoroutine(owner.TargetToLoock(target, 0.01f));

    }

    public override void StopAction()
    {
        animEvent.onProgressAttack -= UseSkill;
        animEvent.onEndAttack -= EndAction;
        isEndAction = false;
    }

    public override void UpdateAction()
    {
        if(isEndAction)
        {
            scheduler.ChangeAction();
        }
    }
    public void EndAction()
    {
        isEndAction = true;
    }
    public void UseSkill()
    {
        skillmanager.UseSkill(owner, skillIndex);
    }
}
