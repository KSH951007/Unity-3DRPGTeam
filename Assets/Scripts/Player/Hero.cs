using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;



public class HeroData
{
    public int heroID;
    public int level;
    public int health;
    public int mana;
    public int damage;
    public float regenerationHealth;
    public float regenerationMana;
    public float defensivePercent;

    public HeroData()
    {

    }
    public HeroData(int heroID,int level, int health, int mana, int damage, float regenerationHealth, float regenerationMana, float defensivePercent)
    {
        this.heroID = heroID;
        this.level = level;
        this.health = health;
        this.mana = mana;
        this.damage = damage;
        this.regenerationHealth = regenerationHealth;
        this.regenerationMana = regenerationMana;
        this.defensivePercent = defensivePercent;
    }
}

public abstract class Hero : MonoBehaviour,ISavable
{

    public HeroData data;



    [SerializeField] protected RuntimeAnimatorController[] animContorllers;
    [SerializeField] protected Transform attackPoint;
    protected Animator animator;
    protected CapsuleCollider myCollider;
    protected EnumType.HeroAnimType animType;
    protected HeroAnimEvent animEnvent;
    [SerializeField] protected HeroSO heroData;
    protected int currentAttackCombo;
    protected HeroMoveAction moveAction;
    protected HeroAttackAction attackAction;
    protected HeroSkillAction[] skillAction;
    protected NavMeshAgent agent;
    protected ActionScheduler scheduler;
    protected ManaSystem manaSystem;


    public ManaSystem GetManaSystem() { return manaSystem; }
    public NavMeshAgent Agent { get { return agent; } }
    public EnumType.HeroAnimType GetAnimType() { return animType; }
    public Animator HeroAnimator { get => animator; }
    public int CurrentAttackCombo { get => currentAttackCombo; set => currentAttackCombo = value; }
    public HeroAnimEvent AnimEvent { get => animEnvent; }

    public ActionScheduler Scheduler { get => scheduler; }

    public Transform AttackPoint { get => attackPoint; }
    public HeroSO GetHeroData() { return heroData; }


    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = transform.Find("Renderer").GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider>();
        animType = EnumType.HeroAnimType.Base;
        ChangeAnimatorController(EnumType.HeroAnimType.Base);
        animEnvent = GetComponentInChildren<HeroAnimEvent>();
        manaSystem = GetComponent<ManaSystem>();

        manaSystem.Init(heroData.GetMaxMana(), heroData.GetRegenerationMana());
        GetComponent<Health>().SetHealth(heroData.GetMaxHealth(), heroData.GetRegenerationHealth(), heroData.GetDefensive());
        scheduler = new ActionScheduler();



        if (DataManager.Instance.LoadData<HeroData>(heroData.GetName(), out HeroData heroInfo))
        {
            data = heroInfo;
        }
        else
        {
            data = new HeroData();
            data.heroID = heroData.GetHeroID();
            data.level = heroData.GetLevel();
            data.health = heroData.GetMaxHealth();
            data.mana = heroData.GetMaxMana();
            data.regenerationHealth = heroData.GetRegenerationHealth();
            data.regenerationMana = heroData.GetRegenerationMana();
            data.damage = heroData.GetDamage();
            data.defensivePercent = heroData.GetDefensive();
        }

    
        Transform skillsTr = transform.Find("Skills");
        Skill[] skills = new Skill[3];
        for (int i = 0; i < skills.Length; i++)
        {
            skills[i] = skillsTr.GetChild(i).GetComponent<Skill>();
        }
        SkillManager skillManager = GetComponentInParent<SkillManager>();
        skillManager.AddSkill(this, skills);
        skillAction = new HeroSkillAction[skillsTr.childCount];
        skillAction[0] = new HeroSkillAction(scheduler, skillManager, 0, animator, this);
        skillAction[1] = new HeroSkillAction(scheduler, skillManager, 1, animator, this);
        skillAction[2] = new HeroSkillAction(scheduler, skillManager, 2, animator, this);

        DataManager.Instance.AddSaveHandler(this);
    }
    public void NoneActiveHero()
    {
        manaSystem.Regenerat();
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
        if (animType == EnumType.HeroAnimType.Base)
            return;

        if (!attackAction.IsLastAttack())
        {

            attackAction.SetTargetTo(newDirection);
            scheduler.AddAction(attackAction);
        }
    }
    public void SkillAction(int skillIndex, Vector3 newDirection)
    {
        if (animType == EnumType.HeroAnimType.Base)
            return;

        if (scheduler.GetNextAction() == skillAction[skillIndex] || scheduler.GetCurrentAction() == skillAction[skillIndex])
            return;

        skillAction[skillIndex].SetTarget(newDirection);
        scheduler.AddAction(skillAction[skillIndex]);

    }
    public IEnumerator TargetToLoock(Vector3 targetPos, float smoothTime)
    {
        Vector3 velocity = Vector3.zero;

        while (Vector3.SignedAngle(transform.forward, targetPos, Vector3.up) > 0.01f)
        {
            transform.forward = Vector3.SmoothDamp(transform.forward, targetPos, ref velocity, smoothTime);
            yield return null;
        }
        transform.forward = targetPos;
    }

    public void SaveData()
    {
        DataManager.Instance.SaveData(data, heroData.GetName());
    }
   
}
