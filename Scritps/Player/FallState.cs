using UnityEngine;

public class FallState : PlayerState
{
    public FallState(PlayerController player, PlayerStateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Enter()
    {
        player.Animator.Play("Fall");
    }
    public override void PhysicsUpdate()
    {
        player.ApplyHorizontalMovement();
    }
    public override void LogicUpdate()
    {

        bool isStableOnGround = player.GroundCheck.IsGrounded && player.RB.linearVelocity.y <= 0.1f;

        if (isStableOnGround)
        {
            player.HasUsedDoubleJump = false;

            if (Mathf.Abs(player.MovementInput.x) > 0.1f)
            {
                stateMachine.ChangeState(player.RunState);
            }
            else
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
        else if (!player.HasUsedDoubleJump && player.JumpInput && player.WasJumpPressed)
        {
            stateMachine.ChangeState(player.DoubleJumpState);
            player.HasUsedDoubleJump = true;
        }

    }

}
