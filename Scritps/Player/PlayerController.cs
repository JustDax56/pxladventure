using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Components
    [Header("Components")]
    [SerializeField] internal InputReader inputReader;
    [SerializeField] internal HealthSystem HealthSystem;
    public Rigidbody2D RB { get; private set; }
    public Animator Animator { get; private set; }
    public GroundCheck GroundCheck { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }
    #endregion

    #region State Machines
    public PlayerStateMachine StateMachine { get; private set; }
    public IdleState IdleState { get; private set; }
    public RunState RunState { get; private set; }
    public JumpState JumpState { get; private set; }
    public FallState FallState { get; private set; }
    public DoubleJumpState DoubleJumpState { get; private set; }
    public DeathState DeathState { get; private set; }
    public HitState HitState { get; private set; }
    #endregion

    #region Inspector Variables
    [Header("Movement Settings")]
    [SerializeField, Range(0f, 20f)] private float moveSpeed = 5f; public float MoveSpeed => moveSpeed;
    [SerializeField, Range(0f, 20f)] private float acceleration = 20f; public float Acceleration => acceleration;
    [SerializeField, Range(0f, 20f)] private float maxSpeed = 10f; public float MaxSpeed => maxSpeed;

    [Header("Jump Settings")]
    [SerializeField, Range(0f, 30f)] private float jumpForce = 10f;
    [SerializeField, Range(0f, 1f)] private float coyoteTimeDuration = 0.2f;
    [SerializeField, Range(0f, 1f)] private float jumpBufferDuration = 0.2f;
    [SerializeField, Range(0f, 1f)] private float jumpCutMultiplier = 0.5f;
    #endregion

    #region State Variables
    public float CoyoteTimeCounter { get; set; }
    public float JumpBufferCounter { get; set; }
    public bool IsJumping { get; set; }
    public float JumpCooldownCounter { get; private set; }
    #endregion

    #region Input Variables
    public Vector2 MovementInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool WasJumpPressed { get; private set; }
    public bool HasUsedDoubleJump { get; internal set; }
    #endregion

    #region Unity Callback Methods
    private void Awake()
    {
        InitializeComponents();
        InitializeStateMachine();
    }

    private void OnEnable()
    {
        inputReader.MoveEvent += OnMove;
        inputReader.JumpStartedEvent += OnJumpStarted;
        inputReader.JumpCanceledEvent += OnJumpCanceled;
        HealthSystem.OnDamageTaken.AddListener(TriggerHitState);
    }

    private void OnDisable()
    {
        inputReader.MoveEvent -= OnMove;
        inputReader.JumpStartedEvent -= OnJumpStarted;
        inputReader.JumpCanceledEvent -= OnJumpCanceled;
        HealthSystem.OnDamageTaken.RemoveListener(TriggerHitState);
    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        UpdateTimers();
        StateMachine.CurrentState.HandleInput();
        StateMachine.CurrentState.LogicUpdate();
        ResetJumpInput();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Initialization Methods
    private void InitializeComponents()
    {
        RB = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        GroundCheck = GetComponent<GroundCheck>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void InitializeStateMachine()
    {
        StateMachine = new PlayerStateMachine(this);
        IdleState = new IdleState(this, StateMachine);
        RunState = new RunState(this, StateMachine);
        JumpState = new JumpState(this, StateMachine);
        FallState = new FallState(this, StateMachine);
        DoubleJumpState = new DoubleJumpState(this, StateMachine);
        DeathState = new DeathState(this, StateMachine);
        HitState = new HitState(this, StateMachine);
    }
    #endregion

    #region Timer Management
    private void UpdateTimers()
    {
        UpdateCoyoteTime();
        UpdateJumpBuffer();
    }

    private void UpdateCoyoteTime()
    {
        if (GroundCheck.IsGrounded)
        {
            CoyoteTimeCounter = coyoteTimeDuration;
        }
        else
        {
            CoyoteTimeCounter -= Time.deltaTime;
        }
    }

    private void UpdateJumpBuffer()
    {
        if (WasJumpPressed)
        {
            JumpBufferCounter = jumpBufferDuration;
        }
        else
        {
            JumpBufferCounter = Mathf.Max(JumpBufferCounter - Time.deltaTime, 0f);
        }
    }

    private void ResetJumpInput()
    {
        WasJumpPressed = false;
    }
    #endregion

    #region Movement Methods
    public void PerformJump()
    {
        IsJumping = true;
        JumpCooldownCounter = Time.time + jumpBufferDuration;
        RB.linearVelocity = new Vector2(RB.linearVelocity.x, jumpForce);
    }

    public void CutJump()
    {
        if (RB.linearVelocity.y > 0f)
        {
            RB.linearVelocity = new Vector2(RB.linearVelocity.x, RB.linearVelocity.y * jumpCutMultiplier);
            CoyoteTimeCounter = 0f;
        }
    }
    #endregion

    #region Input Handling
    private void OnMove(Vector2 movement)
    {
        MovementInput = movement;
        UpdateSpriteDirection(movement);
    }

    private void UpdateSpriteDirection(Vector2 movement)
    {
        if (movement.x != 0)
        {
            SpriteRenderer.flipX = movement.x < 0;
        }
    }

    private void OnJumpStarted()
    {
        JumpInput = true;
        WasJumpPressed = true;
    }

    private void OnJumpCanceled()
    {
        JumpInput = false;
        CutJump();
    }
    #endregion

    public bool CanJump()
    {
        bool hasBufferedJump = CoyoteTimeCounter > 0f && JumpBufferCounter > 0f && !IsJumping;
        bool hasJustPressed = WasJumpPressed && GroundCheck.IsGrounded;
        HasUsedDoubleJump = false;
        return hasBufferedJump || hasJustPressed;
    }


    public bool ShouldFall()
    {
        return !GroundCheck.IsGrounded && RB.linearVelocity.y < 0;
    }

    public void ApplyHorizontalMovement()
    {

        float targetVelocityX = MovementInput.x * Acceleration;
        float clampedVelocityX = Mathf.Clamp(targetVelocityX * MoveSpeed, -maxSpeed, maxSpeed);
        RB.linearVelocity = new Vector2(
            clampedVelocityX,
            RB.linearVelocity.y
        );
    }
    public void CheckHealthAndTransition()
    {
        if (HealthSystem.CurrentHealth <= 0 && StateMachine.CurrentState != DeathState)
        {
            StateMachine.ChangeState(DeathState);
        }
    }
    public void TriggerHitState()
    {
        // Solo cambiar al HitState si no estamos ya en ese estado
        if (StateMachine.CurrentState != HitState)
        {
            StateMachine.ChangeState(HitState);
        }
    }
}
