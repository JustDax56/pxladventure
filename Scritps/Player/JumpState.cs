using UnityEngine;

public class JumpState : PlayerState
{
    public JumpState(PlayerController player, PlayerStateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Enter()
    {
        player.Animator.Play("Jump");
        player.PerformJump();
        player.JumpBufferCounter = 0f;
    }
    public override void PhysicsUpdate()
    {
        player.ApplyHorizontalMovement();
    }

    public override void LogicUpdate()
    {

        if (player.ShouldFall())
        {
            stateMachine.ChangeState(player.FallState);
        }

        if (!player.HasUsedDoubleJump && player.JumpInput && player.WasJumpPressed)
        {
            stateMachine.ChangeState(player.DoubleJumpState);
            player.HasUsedDoubleJump = true;
        }

        if (Time.deltaTime > player.JumpCooldownCounter)
        {
            player.IsJumping = false;
        }
    }
}
