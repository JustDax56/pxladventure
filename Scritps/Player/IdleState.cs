using UnityEngine;

public class IdleState : PlayerState
{
    public IdleState(PlayerController player, PlayerStateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Enter()
    {
        player.RB.linearVelocity = new Vector2(0, player.RB.linearVelocity.y);
        player.Animator.Play("Idle");
        player.HasUsedDoubleJump = false;
    }

    public override void LogicUpdate()
    {

        if (Mathf.Abs(player.MovementInput.x) > 0.1f)
        {
            stateMachine.ChangeState(player.RunState);
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