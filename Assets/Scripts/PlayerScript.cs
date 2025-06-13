using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public bool canMove = true;

    [Header("Movement Speeds")]
    public float walkSpeed = 1f;
    public float runSpeed = 3f;

    [Header("Audio")]
    public AudioSource footstepAudioSource;
    public AudioClip walkClip;
    public AudioClip runClip;

    [Header("Mobile UI Input")]
    public bool isMobile = false;
    public FloatingJoystick joystick; // Assign in Inspector
    public bool runButtonPressed = false;
    public bool crouchButtonPressed = false;

    private PlayerControls controls;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveInput;
    private bool isRunning;
    private bool isCrouching;
    private bool isMoving;

    void Awake()
    {
        controls = new PlayerControls();

        // PC Input bindings (only if not mobile)
        controls.Player.Move.performed += ctx => { if (!isMobile) moveInput = ctx.ReadValue<Vector2>(); };
        controls.Player.Move.canceled += ctx => { if (!isMobile) moveInput = Vector2.zero; };

        controls.Player.Run.performed += _ => { if (!isMobile) isRunning = true; };
        controls.Player.Run.canceled += _ => { if (!isMobile) isRunning = false; };

        controls.Player.Crouch.performed += _ => { if (!isMobile) isCrouching = true; };
        controls.Player.Crouch.canceled += _ => { if (!isMobile) isCrouching = false; };
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Auto-detect mobile
        if (Application.isMobilePlatform)
            isMobile = true;
    }

    void Update()
    {
        // Read from joystick if mobile
        if (isMobile)
        {
            moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
            isRunning = runButtonPressed;
            isCrouching = crouchButtonPressed;
        }

        // Flip sprite based on movement direction
        if (moveInput.x != 0)
            transform.localScale = new Vector3(Mathf.Sign(moveInput.x), 1f, 1f);

        // Animator updates
        animator.SetFloat("Speed", moveInput.magnitude);
        animator.SetBool("IsRunning", isRunning);
        animator.SetBool("IsCrouching", isCrouching);

        // Footstep audio logic
        isMoving = moveInput.magnitude > 0.1f && !isCrouching;

        if (isMoving)
        {
            if (!footstepAudioSource.isPlaying)
            {
                footstepAudioSource.clip = isRunning ? runClip : walkClip;
                footstepAudioSource.loop = true;
                footstepAudioSource.Play();
            }
            else if (footstepAudioSource.clip != (isRunning ? runClip : walkClip))
            {
                footstepAudioSource.Stop();
                footstepAudioSource.clip = isRunning ? runClip : walkClip;
                footstepAudioSource.Play();
            }
        }
        else
        {
            footstepAudioSource.Stop();
        }
    }

    void FixedUpdate()
    {
        if (!canMove) return;

        float speed = isRunning ? runSpeed : walkSpeed;
        Vector2 velocity = moveInput * speed;
        rb.linearVelocity = velocity; // Use velocity (not linearVelocity) in Rigidbody2D
    }

    // Input System
    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    // UI Button Events (assign in Inspector for mobile)
    public void OnRunButtonDown() => runButtonPressed = true;
    public void OnRunButtonUp() => runButtonPressed = false;

    public void OnCrouchButtonDown() => crouchButtonPressed = true;
    public void OnCrouchButtonUp() => crouchButtonPressed = false;
}
