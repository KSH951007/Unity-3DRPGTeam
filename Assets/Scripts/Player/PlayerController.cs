using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
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
    private void Awake()
    {
        heroManager = GetComponentInChildren<HeroManager>();
        mover = GetComponent<PlayerMover>();
        canControl = true;

       stateMachine = new StateMachine<EnumType.PlayerState, PlayerController>(heroManager.GetMainHero().HeroAnimator);

    }
    private void Start()
    {
        stateMachine.AddState(new PlayerIdleState(this, stateMachine));
        stateMachine.AddState(new PlayerRunState(this, stateMachine));
        stateMachine.ChangeState(EnumType.PlayerState.Idle);
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
        stateMachine?.Update(); 
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
                mover.MoveTo(hit.point);
                stateMachine.ChangeState(EnumType.PlayerState.Run);
                return;
            }
        }
    }
    public void PointerClickAttack()
    {
        if (!canControl)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.value);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            hit.point = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(hit.point);
            heroManager.GetMainHero().ChangeAnimatorController(EnumType.HeroAnimType.Battle);
            heroManager.GetMainHero().HeroAnimator.SetInteger("AttackCombo",1);
        }
    }
}
