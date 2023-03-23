using UnityEngine;


public class IdleState : IState
{
    private FSM manager;
    private LeadingRole leadingRole;
    public IdleState(FSM manager)
    {
        this.manager = manager;
        this.leadingRole = manager.leadingRole;
    }
    public void OnEnter()
    {
        leadingRole.animator.Play("Idle");
    }

    public void OnUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") != 0) manager.TransitionState(StateType.Walking);
        if (PlayerMovement.isJump) manager.TransitionState(StateType.Jump);
        if (Input.GetKeyDown(KeyCode.J)) manager.TransitionState(StateType.Attack);

    }

    public void OnExit()
    {

    }
}

