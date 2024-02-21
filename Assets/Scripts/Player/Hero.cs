using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public EnumType.HeroAnimType GetAnimType() { return animType; }
    public Animator HeroAnimator { get => animator; }
    public int AttackComboCount { get => attackComboCount; }
    public int CurrentAttackCombo { get => currentAttackCombo; set => currentAttackCombo = value; }
    public HeroAnimEvent AnimEvent { get => animEnvent; }
    protected virtual void Awake()
    {
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
    public void PointerClickMove()
    {

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.value);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                moveAction.SetMovePoint(hit.point);
                scheduler.AddAction(moveAction);
            }
        }
    }
    public void PointerClickAttack()
    {

        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        screenPos.z = 0f;
        Vector3 mousePos = Mouse.current.position.value;

        Vector3 direction = (mousePos - screenPos).normalized;

        Vector3 newDirection = new Vector3(direction.x, 0f, direction.y);

        if (!attackAction.IsLastAttack())
        {
            attackAction.SetTargetTo(newDirection);
            scheduler.AddAction(attackAction);
        }
    }
}
