using UnityEngine;

// Base State that all other states will inherit from
public abstract class PlayerState
{
    protected PlayerController player;
    protected PlayerStateMachine stateMachine;

    protected PlayerState(PlayerController player, PlayerStateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void HandleInput() { }
    public virtual void LogicUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void Exit() { }


}