using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    public Animator playerAnim;
    private float targetSpeed;
    public float walkSpeed = 10f;
    public float jogSpeed = 15f;
    public float sprintSpeed = 20f;
    public float jumpForce = 5f;
    private bool inAir = false;
    private bool isWalking = false;
    private bool isSprinting = false;
    private float movementAnimSpeed = 0.0f;
    private Rigidbody playerRigidbody;
    private PlayerControls playerControls;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Land.Enable();

        // Subscribe events
        playerControls.Land.Jump.started += Jump;
        playerControls.Land.Walk.performed += WalkToggle;
    }

    private void OnDisable()
    {
        playerControls.Land.Disable();

        // Unsubscribe events
        playerControls.Land.Jump.started -= Jump;
        playerControls.Land.Walk.performed -= WalkToggle;
    }

    // Start is called before the first frame update
    void Start()
    {
        targetSpeed = jogSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movementDirection = playerControls.Land.Move.ReadValue<Vector2>();
        // Debug.Log("Movement Input: " + movementDirection);

        bool isIdle = movementDirection == Vector2.zero;

        // To check if jump animation is playing to disable double jump
        AnimatorStateInfo animStateInfo = playerAnim.GetCurrentAnimatorStateInfo(0);
        if (animStateInfo.IsName("Jump"))
        {
            inAir = true;
        }
        else
        {
            inAir = false;
        }

        // Move character
        if (!inAir)
        {
            playerRigidbody.AddForce(new Vector3(movementDirection.x, 0, movementDirection.y) * targetSpeed, ForceMode.Force);
        }

        if (!isIdle)
        {
            if (isWalking)
            {
                targetSpeed = walkSpeed;
                movementAnimSpeed = 0.33f;
                Debug.Log("Walking... | Speed: " + targetSpeed);
            }
            else
            {
                targetSpeed = jogSpeed;
                movementAnimSpeed = 0.66f;
                Debug.Log("Jogging... | Speed: " + targetSpeed);
            }
        }
        else
        {
            // When player is idle
            movementAnimSpeed = 0.0f;
            Debug.Log("Idling...");
        }

        // Play animation
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

    // Player movements
    private void Jump(InputAction.CallbackContext context)
    {
        if (!inAir)
        {
            Debug.Log("Jumping...");
            playerAnim.SetTrigger("jump");
        }
        // playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void WalkToggle(InputAction.CallbackContext context)
    {
        isWalking = !isWalking;
        string tempStr = isWalking ? "walking" : "normal";
        Debug.Log("switched to " + tempStr + " mode...");
    }
}
