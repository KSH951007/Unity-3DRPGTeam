using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private ClickMoveArrowEffect moveArrowEffect;
    private PlayerControls inputs;
    private HeroManager heroManager;
    public PlayerControls Inputs { get { return inputs; } }
    private bool canControl;

    private Hero mainHero;
    private PlayerInteract interact;
    private EquipmentManager equipmentManager;
    private void Awake()
    {
        heroManager = GetComponent<HeroManager>();
        interact = GetComponent<PlayerInteract>();
        equipmentManager = GetComponent<EquipmentManager>();
        canControl = true;


    }
    private void Start()
    {
        mainHero = heroManager.GetMainHero();

        heroManager.onChangeCharacter += () =>
            {
                mainHero = heroManager.GetMainHero();

            };

        inputs.Player.Move.performed += _ => PointerClickMove();
        inputs.Player.Attack.performed += _ => PointerClickAttack();
        inputs.Player.Skill1.performed += _ => PressSkill(0);
        inputs.Player.Skill2.performed += _ => PressSkill(1);
        inputs.Player.Skill3.performed += _ => PressSkill(2);
        inputs.Player.Interact.performed += _ =>
        {
            interact.MainHeroTranform = heroManager.GetMainHero().transform;
            interact.Interact();
        };

        inputs.Player.ChangeCharacter.performed += _ =>
        {
            if (mainHero.Scheduler.GetCurrentAction() == null || mainHero.Scheduler.GetCurrentAction() is HeroMoveAction)
                heroManager.ChangeCharacter();
        };

        inputs.Player.ItemUse.performed += (value) => { equipmentManager.UsePortion(Convert.ToInt32(value.control.name) - 1); };
    }


    private void OnEnable()
    {
        if (inputs == null)
        {
            inputs = new PlayerControls();
        }
        inputs.Enable();

    }
    private void OnDisable()
    {
        if (inputs != null)
        {
            inputs.Disable();
        }
    }
    private void Update()
    {

        mainHero?.Scheduler.ProcessAction();
    }
    public void PointerClickMove()
    {
        if (!canControl)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.value);

        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, LayerMask.GetMask("Ground")))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {

                moveArrowEffect.Play(hit.point);
                mainHero.MoveAction(hit.point);
            }
        }
    }
    public void PointerClickAttack()
    {
        if (!canControl)
            return;
        if (EventSystem.current.IsPointerOverGameObject() == false)
        {
            mainHero.AttackAction(PointerToTarget());
        }


    }
    public void PressSkill(int skillIndex)
    {
        if (GetComponent<SkillManager>().CanUseSkill(mainHero, skillIndex))
        {
            mainHero.SkillAction(skillIndex, PointerToTarget());
        }
    }
    private Vector3 PointerToTarget()
    {
        //TODO: 나중에 마우스위치 수정
        Vector3 screenPos = Camera.main.WorldToScreenPoint(mainHero.transform.position);
        screenPos.z = 0f;
        Vector3 mousePos = Mouse.current.position.value;
        Vector3 direction = (mousePos - screenPos).normalized;

        return new Vector3(direction.x, 0f, direction.y);
    }
}
