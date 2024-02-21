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
    private StateMachine<EnumType.PlayerState, PlayerController> stateMachine;
    public PlayerControls Inputs { get { return inputs; } }
    private bool canControl;
    private ActionScheduler scheduler;
    private PlayerMoveAction moveAction;
    private PlayerAttackAction attackAction;
    private NavMeshAgent agent;

    private void Awake()
    {
        heroManager = GetComponent<HeroManager>();
        agent = GetComponent<NavMeshAgent>();
        canControl = true;
        scheduler = new ActionScheduler();


    }
    private void Start()
    {
        moveAction = new PlayerMoveAction(heroManager.GetMainHero().HeroAnimator, this, agent, 3.5f);
        attackAction = new PlayerAttackAction(heroManager.GetMainHero().HeroAnimator, this, heroManager.GetMainHero(), 3);
        inputs.Player.Move.performed += _ => PointerClickMove();
        inputs.Player.Attack.performed += _ => PointerClickAttack();

        inputs.Player.ChangeCharacter.performed += _ =>
        {            
            heroManager.ChangeCharacter();
            scheduler.ResetActions();
            moveAction.ChangeAnimator(heroManager.GetMainHero().HeroAnimator);
            attackAction.ChangeAnimator(heroManager.GetMainHero().HeroAnimator);
        };
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
        scheduler.ProcessAction();
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
                moveAction.SetMovePoint(hit.point);
                scheduler.AddAction(moveAction);
            }
        }
    }
    public void PointerClickAttack()
    {
        if (!canControl)
            return;

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
