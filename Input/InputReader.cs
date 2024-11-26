
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using DaxInput;

[CreateAssetMenu(fileName = "PlayerInputReader", menuName = "Input/Player Input Reader")]
public class InputReader : ScriptableObject, GameInput.IGameplayActions
{
    private GameInput gameInput;

    // Movement
    public Vector2 MovementValue { get; private set; }
    public event Action<Vector2> MoveEvent;

    // Jump
    public event Action JumpStartedEvent;
    public event Action JumpCanceledEvent;

    private void OnEnable()
    {
        if (gameInput == null)
        {
            gameInput = new GameInput();
            gameInput.Gameplay.SetCallbacks(this);
            EnableGameplayInput();
        }
    }

    private void OnDisable()
    {
        DisableAllInput();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
        MoveEvent?.Invoke(MovementValue);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                JumpStartedEvent?.Invoke();
                break;
            case InputActionPhase.Canceled:
                JumpCanceledEvent?.Invoke();
                break;
        }
    }

    public void EnableGameplayInput() => gameInput.Gameplay.Enable();
    public void DisableAllInput() => gameInput.Gameplay.Disable();

    public void OnAttack(InputAction.CallbackContext context)
    {
        // me falta implementar la wea 
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        //same 
    }
    ///despues de aca me falta implementar el INPUT Para el UI AGREGAR == GameInput.IUInputActions y agregar os eventos en caso de que necesite en l linea  10 para abajo antes del on enable 
}
