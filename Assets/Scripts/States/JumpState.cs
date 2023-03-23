using UnityEngine;

public class JumpState : IState
{
    private FSM manager;
    private LeadingRole leadingRole;
    private float time=0;
    public JumpState(FSM manager)
    {
        this.manager = manager;
        this.leadingRole = manager.leadingRole;
    }
    void IState.OnEnter()
    {
        leadingRole.animator.Play("Jump");
        PlayerMovement.isJump= true;
    }

    void IState.OnUpdate()
    {
        time += Time.deltaTime;
        if (PlayerMovement.isGround && time>0.3f)
        {
            manager.TransitionState(StateType.Idle);
        }
        if (Input.GetKeyDown(KeyCode.J)) manager.TransitionState(StateType.Attack);


    }
    void IState.OnExit()
    {
        time = 0;
        PlayerMovement.isJump = false;
    }

    // Start is called before the first frame updat
}
