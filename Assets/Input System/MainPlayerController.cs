using UnityEngine;
using UnityEngine.InputSystem;

public class MainPlayerController : MonoBehaviour
{
    [SerializeField] private Animator playerAnim;
    [SerializeField] private float sprintSpeed = 12.0f;
    private float targetSpeed;
    private bool isIdle = true;
    private bool inAir = false;
    private bool isWalking = false;
    private float movementAnimSpeed = 0.0f;
    private MenuUI menuUI;
    // private PlayerControls playerControls;
    // public float jumpForce = 5f;

    [SerializeField] private float walkSpeed = 2.0f;
    [SerializeField] private float jogSpeed = 7.0f;
    [SerializeField] private float rotationSpeed = 15.0f;
    private InputManager inputManager;
    private Transform cameraTransform;
    private Vector3 movementDirection;
    private Rigidbody playerRigidbody;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cameraTransform = Camera.main.transform;
        playerRigidbody = GetComponent<Rigidbody>();
        inputManager = GetComponent<InputManager>();
        menuUI = new MenuUI();
    }

    // private void OnEnable()
    // {
    //     playerControls.Player.Enable();
    //
    //     // Subscribe events
    //     //playerControls.Player.Jump.started += Jump;
    //     playerControls.Player.Interact.performed += Interact;
    //     playerControls.Player.Walk.performed += WalkToggle;
    //     playerControls.Player.OpenInventory.performed += OpenInventory;
    //     playerControls.Player.EnterVehicle.performed += EnterVehicle;
    //     playerControls.Player.Pause.performed += OpenPause;
    //     playerControls.Player.OpenMap.performed += OpenMap;
    //     playerControls.Player.ResetCamera.performed += ResetCamera;
    // }

    // private void OnDisable()
    // {
    //     playerControls.Player.Disable();
    //
    //     // Unsubscribe events
    //     //playerControls.Player.Jump.started -= Jump;
    //     playerControls.Player.Interact.performed -= Interact;
    //     playerControls.Player.Walk.performed -= WalkToggle;
    //     playerControls.Player.OpenInventory.performed -= OpenInventory;
    //     playerControls.Player.EnterVehicle.performed -= EnterVehicle;
    //     playerControls.Player.Pause.performed -= OpenPause;
    //     playerControls.Player.OpenMap.performed -= OpenMap;
    //     playerControls.Player.ResetCamera.performed -= ResetCamera;
    // }

    // Start is called before the first frame update
    // void Start()
    // {
    //     Time.timeScale = 1.0f;
    //     targetSpeed = jogSpeed; // Default speed
    // }

    // Update is called once per frame
    // void FixedUpdate()
    // {
    //     Vector2 movementDirection = playerControls.Player.Move.ReadValue<Vector2>();
    //     // Debug.Log("Movement Input: " + movementDirection);

    //     isIdle = movementDirection == Vector2.zero;

    //     CheckIfInAir();

    //     // Move character only when ON GROUND
    //     if (!inAir)
    //     {
    //         playerRigidbody.AddForce(new Vector3(movementDirection.x, 0, movementDirection.y) * targetSpeed, ForceMode.Force);
    //     }

    //     UpdateMovementAnimSpeed();

    //     // Play animation smoothly
    //     playerAnim.SetFloat("moveSpeed", movementAnimSpeed, 0.1f, Time.deltaTime);

    //     // Smooth rotation
    //     if (!isIdle)
    //     {
    //         float targetRotation = Mathf.Atan2(movementDirection.x, movementDirection.y) * Mathf.Rad2Deg;
    //         float rotationVelocity = 10f;
    //         float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, 0.07f);

    //         transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
    //     }
    // }

    // === === === === ===
    // Player Controls Events
    // === === === === ===

    //private void Jump(InputAction.CallbackContext context)
    //{
    //    if (!inAir)
    //    {
    //        Debug.Log("Jumping...");
    //        playerAnim.SetTrigger("jump");
    //    }
    //
    //    // Will not use actual jumping because our game doesn't require the character to jump,
    //    // at least for now.
    //    // //playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    //}

    private void WalkToggle(InputAction.CallbackContext context)
    {
        isWalking = !isWalking;
        string tempStr = isWalking ? "walking" : "normal";
        Debug.Log("switched to " + tempStr + " mode...");
    }

    private void OpenInventory(InputAction.CallbackContext context)
    {
        menuUI.ToggleMenuVisibility(0);
    }

    private void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("interacting...");
    }

    private void EnterVehicle(InputAction.CallbackContext context)
    {
        menuUI.ToggleMenuVisibility(1);
    }

    private void OpenPause(InputAction.CallbackContext context)
    {
        menuUI.ToggleMenuVisibility(2);
    }

    public void ResumeGame()
    {
        Debug.Log("resuming game...");
    }

    private void OpenMap(InputAction.CallbackContext context)
    {
        Debug.Log("displaying map...");
    }

    private void ResetCamera(InputAction.CallbackContext context)
    {
        Debug.Log("resetting camera...");
    }

    // === === === === ===
    // NEW functions
    // === === === === ===

    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        movementDirection = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z) * inputManager.verticalInput;
        movementDirection += cameraTransform.right * inputManager.horizontalInput;
        movementDirection.Normalize();
        movementDirection.y = 0;

        movementDirection *= GetPlayerSpeed();


        Vector3 movementVelocity = movementDirection;
        playerRigidbody.velocity = movementVelocity;
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraTransform.forward * inputManager.verticalInput;
        targetDirection += cameraTransform.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero) targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    // === === === === ===
    // Helper functions
    // === === === === ===

    private float GetPlayerSpeed()
    {
        float speed = 0.0f;

        if (inputManager.verticalInput > 0.0f && inputManager.verticalInput < 0.35f)
        {
            speed = walkSpeed; // Walk
        }
        else if (inputManager.verticalInput > 0.35f)
        {
            speed = jogSpeed; // Jog
        }
        else if (inputManager.verticalInput < 0.0f && inputManager.verticalInput > -0.35f)
        {
            speed = walkSpeed; // Walk
        }
        else if (inputManager.verticalInput < -0.35f)
        {
            speed = jogSpeed; // Jog
        }

        Debug.Log($"Setting speed to: {speed}...");
        return speed;
    }

    /// <summary>
    /// To check if jump animation is currently playing.
    /// <para />
    /// If the jump animation is in progress, then it means that the player is IN AIR.
    /// Otherwise, if the jump animation is finished, then it means that the player is NOT IN AIR.
    /// </summary>
    private void CheckIfInAir()
    {
        AnimatorStateInfo animStateInfo = playerAnim.GetCurrentAnimatorStateInfo(0);
        if (animStateInfo.IsName("Jump"))
        {
            inAir = true;
        }
        else
        {
            inAir = false;
        }
    }

    /// <summary>
    /// Sets the "movement" animation speed to either of the following
    /// depending on the current state of the player such as walking or sprinting:
    /// <list type="number">
    ///     <item>
    ///         <term>Idle Animation</term>
    ///         <description>0.0f</description>
    ///     </item>
    ///     <item>
    ///         <term>Walking Animation</term>
    ///         <description>0.33f</description>
    ///     </item>
    ///     <item>
    ///         <term>Jogging Animation</term>
    ///         <description>0.66f</description>
    ///     </item>
    ///     <item>
    ///         <term>Sprinting Animation</term>
    ///         <description>1.0f</description>
    ///     </item>
    /// </list>
    /// </summary>
    private void UpdateMovementAnimSpeed()
    {
        // if (!isIdle)
        // {
        //     if (playerControls.Player.Sprint.IsInProgress())
        //     {
        //         targetSpeed = sprintSpeed;
        //         movementAnimSpeed = 1.0f;
        //         //Debug.Log("Sprinting... | Speed: " + targetSpeed);
        //     }
        //     else if (isWalking)
        //     {
        //         targetSpeed = walkSpeed;
        //         movementAnimSpeed = 0.33f;
        //         //Debug.Log("Walking... | Speed: " + targetSpeed);
        //     }
        //     else
        //     {
        //         targetSpeed = jogSpeed;
        //         movementAnimSpeed = 0.66f;
        //         //Debug.Log("Jogging... | Speed: " + targetSpeed);
        //     }
        // }
        // else
        // {
        //     // When player is idle
        //     movementAnimSpeed = 0.0f;
        //     //Debug.Log("Idling...");
        // }
    }
}
