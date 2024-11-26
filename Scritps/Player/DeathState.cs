using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathState : PlayerState
{
    public DeathState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        player.RB.linearVelocity = new Vector2(0, player.RB.linearVelocity.y);
        player.Animator.Play("Death");

        
        player.StartCoroutine(RestartSceneAfterDelay());
    }

    private System.Collections.IEnumerator RestartSceneAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}