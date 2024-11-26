using UnityEngine;

public class HitState : PlayerState
{
    private float hitStunDuration = 0.5f; // Adjust as needed
    private float hitStunTimer;

    public HitState(PlayerController player, PlayerStateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Enter()
    {
       
        player.RB.linearVelocity = new Vector2(0, player.RB.linearVelocity.y);
        player.Animator.Play("Hit");
        hitStunTimer = hitStunDuration;
    }

    public override void LogicUpdate()
    {
        player.CheckHealthAndTransition();

        hitStunTimer -= Time.deltaTime;


        if (hitStunTimer <= 0)
        {

            if (player.GroundCheck.IsGrounded)
            {
                if (Mathf.Abs(player.MovementInput.x) > 0.1f)
                {
                    stateMachine.ChangeState(player.RunState);
                }
                else
                {
                    stateMachine.ChangeState(player.IdleState);
                }
            }

            else
            {
                stateMachine.ChangeState(player.FallState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        player.RB.linearVelocity = new Vector2(0, player.RB.linearVelocity.y);
    }
}
