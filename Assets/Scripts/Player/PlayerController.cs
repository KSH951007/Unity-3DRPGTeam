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
    private PlayerMover mover;
    private StateMachine<EnumType.PlayerState, PlayerController> stateMachine;
    public PlayerControls Inputs { get { return inputs; } }
    public PlayerMover Mover { get { return mover; } }
    private bool canControl;
    private ActionScheduler scheduler;
    private PlayerMoveAction moveAction;
    private PlayerAttackAction attackAction;
    private NavMeshAgent agent;

    private void Awake()
    {
        heroManager = GetComponent<HeroManager>();
        agent = GetComponent<NavMeshAgent>();
        mover = GetComponent<PlayerMover>();
        canControl = true;
        scheduler = new ActionScheduler();


    }
    private void Start()
    {
        moveAction = new PlayerMoveAction(heroManager.GetMainHero().HeroAnimator, this, agent, 3.5f);
        attackAction = new PlayerAttackAction(heroManager.GetMainHero().HeroAnimator, this, heroManager.GetMainHero(), 3);
        inputs.Player.Move.performed += _ => PointerClickMove();
        inputs.Player.Attack.performed += _ => PointerClickAttack();
        inputs.Player.ChangeCharacter.performed += _ => heroManager.ChangeCharacter(heroManager.nextCharacter());
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

        //Debug.Log(screenPos);

       

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.value);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                hit.point = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                attackAction.SetTargetTo(hit.point);
                scheduler.AddAction(attackAction);
            }
        }

    }
    public IEnumerator TargetToLoock(Vector3 targetPos, float smoothTime)
    {
        Vector3 direction = (targetPos - this.transform.position).normalized;
        Vector3 velocity = Vector3.zero;
        while (Vector3.Dot(transform.forward, direction) <= 0.99f)
        {
            transform.forward = Vector3.SmoothDamp(transform.forward, direction, ref velocity, smoothTime);

            yield return null;
        }
    }
    public void AttackTo()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.value);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            hit.point = new Vector3(hit.point.x, transform.position.y, hit.point.z);

            StartCoroutine(TargetToLoock(hit.point, 0.1f));
        }
        heroManager.GetMainHero().HeroAnimator.SetTrigger("Attack");
    }
}
