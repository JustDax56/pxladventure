using UnityEngine;
public class PlayerStateMachine
{
    public PlayerState CurrentState { get; private set; }
    private readonly PlayerController player; 


    public PlayerStateMachine(PlayerController playerController)
    {
        player = playerController;
    }

    public void Initialize(PlayerState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(PlayerState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}