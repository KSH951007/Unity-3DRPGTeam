using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using static UnityEngine.UI.GridLayoutGroup;

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
    protected PlayerMoveAction moveAction;
    protected PlayerAttackAction attackAction;
    protected NavMeshAgent agent;
    protected ActionScheduler scheduler;

    public NavMeshAgent Agent { get { return agent; } }
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

    public void MoveAction(Vector3 targetPosition)
    {
        moveAction.SetMovePoint(targetPosition);
        scheduler.AddAction(moveAction);
    }
    public void AttackAction(Vector3 newDirection)
    {
        if (!attackAction.IsLastAttack())
        {
            attackAction.SetTargetTo(newDirection);
            scheduler.AddAction(attackAction);
        }

    }
    public IEnumerator TargetToLoock(Vector3 targetPos, float smoothTime)
    {
        Vector3 velocity = Vector3.zero;
        while (Vector3.Dot(transform.forward, targetPos) <= 0.99f)
        {
            transform.forward = Vector3.SmoothDamp(transform.forward, targetPos, ref velocity, smoothTime);

            yield return null;
        }
    }
}
