using UnityEngine;

public class DoubleJumpState : PlayerState
{
    public DoubleJumpState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        player.Animator.Play("DoubleJump");
        player.PerformJump();
        player.IsJumping = true;
        player.JumpBufferCounter = 0;
    }
    public override void PhysicsUpdate()
    {
        player.ApplyHorizontalMovement();
        if (Time.deltaTime>player.JumpCooldownCounter)
        {
            player.IsJumping = false;
        }
    }

    public override void LogicUpdate()
    {

        if (player.HealthSystem.CurrentHealth == 0)
        {
            stateMachine.ChangeState(player.DeathState);
        }

        if (player.ShouldFall())
        {
            stateMachine.ChangeState(player.FallState);
        }

    }
}

