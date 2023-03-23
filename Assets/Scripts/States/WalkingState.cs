using UnityEngine;

public class WalkingState : IState
{
    private FSM manager;
    private LeadingRole leadingRole;
    public WalkingState(FSM manager)
    {
        this.manager = manager;
        this.leadingRole = manager.leadingRole;
    }
    void IState.OnEnter()
    {
        leadingRole.animator.Play("Run");
    }

    void IState.OnUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") == 0) manager.TransitionState(StateType.Idle);
        if (PlayerMovement.isJump) manager.TransitionState(StateType.Jump);
        if (Input.GetKeyDown(KeyCode.J)) manager.TransitionState(StateType.Attack);
    }

    void IState.OnExit()
    {

    }
    // Start is called before the first frame update
}
