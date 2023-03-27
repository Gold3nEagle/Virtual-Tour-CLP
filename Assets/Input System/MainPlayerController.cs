using UnityEngine;
using UnityEngine.InputSystem;

public class MainPlayerController : MonoBehaviour
{
    public Animator playerAnim;
    private float targetSpeed;
    public float walkSpeed = 15f;
    public float jogSpeed = 30f;
    public float sprintSpeed = 50f;
    private bool isIdle = true;
    private bool inAir = false;
    private bool isWalking = false;
    private float movementAnimSpeed = 0.0f;
    private MenuUI invMenu;
    private MenuUI shopMenu;
    private MenuUI pauseMenu;
    private Rigidbody playerRigidbody;
    private PlayerControls playerControls;
    // public float jumpForce = 5f;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerControls = new PlayerControls();
        invMenu = new MenuUI("inv");
        shopMenu = new MenuUI("shop");
        pauseMenu = new MenuUI("pause");
    }

    private void OnEnable()
    {
        playerControls.Land.Enable();

        // Subscribe events
        playerControls.Land.Jump.started += Jump;
        playerControls.Land.Interact.performed += Interact;
        playerControls.Land.Walk.performed += WalkToggle;
        playerControls.Land.OpenInventory.performed += OpenInventory;
        playerControls.Land.EnterVehicle.performed += EnterVehicle;
        playerControls.Land.Pause.performed += OpenPause;
        playerControls.Land.OpenMap.performed += OpenMap;
        playerControls.Land.ResetCamera.performed += ResetCamera;
    }

    private void OnDisable()
    {
        playerControls.Land.Disable();

        // Unsubscribe events
        playerControls.Land.Jump.started -= Jump;
        playerControls.Land.Interact.performed -= Interact;
        playerControls.Land.Walk.performed -= WalkToggle;
        playerControls.Land.OpenInventory.performed -= OpenInventory;
        playerControls.Land.EnterVehicle.performed -= EnterVehicle;
        playerControls.Land.Pause.performed -= OpenPause;
        playerControls.Land.OpenMap.performed -= OpenMap;
        playerControls.Land.ResetCamera.performed -= ResetCamera;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        targetSpeed = jogSpeed; // Default speed
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movementDirection = playerControls.Land.Move.ReadValue<Vector2>();
        // Debug.Log("Movement Input: " + movementDirection);

        isIdle = movementDirection == Vector2.zero;

        CheckIfInAir();

        // Move character only when ON GROUND
        if (!inAir)
        {
            playerRigidbody.AddForce(new Vector3(movementDirection.x, 0, movementDirection.y) * targetSpeed, ForceMode.Force);
        }

        UpdateMovementAnimSpeed();

        // Play animation smoothly
        playerAnim.SetFloat("moveSpeed", movementAnimSpeed, 0.1f, Time.deltaTime);

        // Smooth rotation
        if (!isIdle)
        {
            float targetRotation = Mathf.Atan2(movementDirection.x, movementDirection.y) * Mathf.Rad2Deg;
            float rotationVelocity = 10f;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, 0.07f);

            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }
    }

    // === === === === ===
    // Player Controls Events
    // === === === === ===

    private void Jump(InputAction.CallbackContext context)
    {
        if (!inAir)
        {
            Debug.Log("Jumping...");
            playerAnim.SetTrigger("jump");
        }

        // Will not use actual jumping because our game doesn't require the character to jump,
        // at least for now.
        // //playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void WalkToggle(InputAction.CallbackContext context)
    {
        isWalking = !isWalking;
        string tempStr = isWalking ? "walking" : "normal";
        Debug.Log("switched to " + tempStr + " mode...");
    }

    private void OpenInventory(InputAction.CallbackContext context)
    {
        Debug.Log("displaying inventory menu...");

        pauseMenu.CloseMenu();
        shopMenu.CloseMenu();
        invMenu.OpenMenu();
    }

    private void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("interacting...");
    }

    private void EnterVehicle(InputAction.CallbackContext context)
    {
        Debug.Log("Entering vehicle...");

        pauseMenu.CloseMenu();
        invMenu.CloseMenu();
        shopMenu.OpenMenu();
    }

    private void OpenPause(InputAction.CallbackContext context)
    {
        Debug.Log("displaying pause menu...");

        shopMenu.CloseMenu();
        invMenu.CloseMenu();
        pauseMenu.OpenMenu();
    }

    public void ResumeGame()
    {
        Debug.Log("resuming game...");

        shopMenu.CloseMenu();
        invMenu.CloseMenu();
        pauseMenu.OpenMenu();
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
    // Helper functions
    // === === === === ===

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
        if (!isIdle)
        {
            if (playerControls.Land.Sprint.IsInProgress())
            {
                targetSpeed = sprintSpeed;
                movementAnimSpeed = 1.0f;
                //Debug.Log("Sprinting... | Speed: " + targetSpeed);
            }
            else if (isWalking)
            {
                targetSpeed = walkSpeed;
                movementAnimSpeed = 0.33f;
                //Debug.Log("Walking... | Speed: " + targetSpeed);
            }
            else
            {
                targetSpeed = jogSpeed;
                movementAnimSpeed = 0.66f;
                //Debug.Log("Jogging... | Speed: " + targetSpeed);
            }
        }
        else
        {
            // When player is idle
            movementAnimSpeed = 0.0f;
            //Debug.Log("Idling...");
        }
    }
}
