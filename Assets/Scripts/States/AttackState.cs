using UnityEngine;

public class AttackState : IState
{
    private FSM manager;
    private LeadingRole leadingRole;
    private float time = 0;
    private float animationTime = 0.46f;
    private float _moveSpeed;
    public AttackState(FSM manager)
    {
        this.manager = manager;
        this.leadingRole = manager.leadingRole;
    }
    void IState.OnEnter()
    {
        leadingRole.animator.Play("Attack");
        _moveSpeed = PlayerMovement.moveSpeed;
        if (PlayerMovement.isGround)  PlayerMovement.moveSpeed = 0;
          
    }

    void IState.OnUpdate()
    {
        time+= Time.deltaTime;
        if (time > animationTime) manager.TransitionState(StateType.Idle);
    }
    void IState.OnExit()
    {
        PlayerMovement.moveSpeed = _moveSpeed;
        time = 0;
    }

    // Start is called before the first frame update
}
