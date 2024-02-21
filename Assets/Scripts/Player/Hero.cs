using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public abstract class Hero : MonoBehaviour
{
    [SerializeField] protected RuntimeAnimatorController[] animContorllers;
    protected Animator animator;
    protected CapsuleCollider myCollider;
    protected EnumType.HeroAnimType animType;
    protected HeroAnimEvent animEnvent;

    [SerializeField] protected HeroSO heroData;
    protected int attackComboCount;
    protected int currentAttackCombo;
    protected List<Skill> skills;
    protected ActionScheduler scheduler;
    protected PlayerMoveAction moveAction;
    protected PlayerAttackAction attackAction;
    protected NavMeshAgent agent;

    public EnumType.HeroAnimType GetAnimType() { return animType; }
    public Animator HeroAnimator { get => animator; }
    public int AttackComboCount { get => attackComboCount; }
    public int CurrentAttackCombo { get => currentAttackCombo; set => currentAttackCombo = value; }
    public HeroAnimEvent AnimEvent { get => animEnvent; }


    public ActionScheduler Scheduler { get => scheduler; }
    public PlayerMoveAction GetMoveAction() { return moveAction; }
    public PlayerAttackAction GetAttackAction() { return attackAction; }
    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        myCollider = GetComponent<CapsuleCollider>();
        animType = EnumType.HeroAnimType.Base;
        ChangeAnimatorController(EnumType.HeroAnimType.Base);
        animEnvent = GetComponentInChildren<HeroAnimEvent>();
        skills = new List<Skill>(3);

        GetComponent<Health>().SetHealth(heroData.GetMaxHealth());

        scheduler = new ActionScheduler();

    }
    public void ChangeAnimatorController(EnumType.HeroAnimType newAnimType)
    {
        animType = newAnimType;
        animator.runtimeAnimatorController = animContorllers[(int)animType];
    }
    public abstract void Atacck();

    public abstract void Skill1();
    public abstract void Skill2();
    public abstract void Skill3();

    public void MoveAction(Vector3 targetPosition)
    {
        moveAction.SetMovePoint(targetPosition);
        scheduler.AddAction(moveAction);
    }
    public void AttackAction(Vector3 newDirection)
    {
        attackAction.SetTargetTo(newDirection);
        scheduler.AddAction(attackAction);
    }
}
