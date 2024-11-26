using UnityEngine;

public class RunState : PlayerState
{
    public RunState(PlayerController player, PlayerStateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Enter()
    {
        player.Animator.Play("Run");
        player.HasUsedDoubleJump = false;
    }

    public override void PhysicsUpdate()
    {
        player.ApplyHorizontalMovement();
    }

    public override void LogicUpdate()
    {

        if (Mathf.Abs(player.MovementInput.x) < 0.5f)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if (player.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if (player.ShouldFall())
        {
            stateMachine.ChangeState(player.FallState);
        }

    }
}
