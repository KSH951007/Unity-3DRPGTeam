using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private PlayerControls inputs;
    private HeroManager heroManager;
    public PlayerControls Inputs { get { return inputs; } }
    private bool canControl;

    private Hero mainHero;

    private void Awake()
    {
        heroManager = GetComponent<HeroManager>();
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

        inputs.Player.ChangeCharacter.performed += _ => heroManager.ChangeCharacter();

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

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                mainHero.MoveAction(hit.point);
            }
        }
    }
    public void PointerClickAttack()
    {
        if (!canControl)
            return;

        //if (SceneLoader.Instance.GetSceneType() == EnumType.SceneType.Village)
        //    return;

        mainHero.AttackAction(PointerToTarget());

    }
    public void PressSkill1()
    {

    }
    private Vector3 PointerToTarget()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(mainHero.transform.position);

        screenPos.z = 0f;
        Vector3 mousePos = Mouse.current.position.value;

        Vector3 direction = (mousePos - screenPos).normalized;

        return new Vector3(direction.x, 0f, direction.y);
    }
}
