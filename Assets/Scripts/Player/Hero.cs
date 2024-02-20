using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hero : MonoBehaviour
{
    [SerializeField] protected RuntimeAnimatorController[] animContorllers;
    protected Animator animator;
    protected CapsuleCollider myCollider;
    protected EnumType.HeroAnimType animType;
    protected HeroAnimEvent animEnvent;
    protected int attackComboCount;
    protected int currentAttackCombo;
    protected List<Skill> skills;
    public ComboAttack comboAttack;

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

}
