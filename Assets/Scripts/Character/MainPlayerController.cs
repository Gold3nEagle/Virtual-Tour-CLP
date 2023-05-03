using UnityEngine;

public class MainPlayerController : MonoBehaviour
{
    public bool isWalking;
    public bool isSprinting;

    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed = 2.0f;
    [SerializeField] private float jogSpeed = 6.0f;
    [SerializeField] private float sprintSpeed = 8.0f;
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
    }

    // === === === === ===
    // Player Input Handling
    // === === === === ===

    public void HandleAllMovement()
    {
        HandleMovement(true);
        HandleRotation(true);
    }

    public void DoNotHandleAllMovement()
    {
        HandleMovement(false);
        HandleRotation(false);
    }

    private void HandleMovement(bool isActive)
    {
        movementDirection = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z) * inputManager.verticalInput;
        movementDirection += cameraTransform.right * inputManager.horizontalInput;

        if (!isActive) movementDirection = new Vector3(0, 0, 0);

        movementDirection.Normalize();
        movementDirection.y = 0;

        movementDirection *= GetPlayerSpeed();

        Vector3 movementVelocity = movementDirection;
        movementVelocity.y = playerRigidbody.velocity.y;
        playerRigidbody.velocity = movementVelocity;
    }

    private void HandleRotation(bool isActive)
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraTransform.forward * inputManager.verticalInput;
        targetDirection += cameraTransform.right * inputManager.horizontalInput;

        if (!isActive) targetDirection = new Vector3(0, 0, 0);

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

    /// <summary>
    /// Will return "Jog" speed when the input value is greater than/ equal to 35%.
    /// <para />
    /// Will return "Walk" speed when the input value is less than 35%.
    /// </summary>
    /// <returns>The speed depending on input value</returns>
    private float GetPlayerSpeed()
    {
        float speed = 0.0f;

        if (isSprinting)
        {
            speed = sprintSpeed;
        }
        else if (isWalking)
        {
            speed = walkSpeed;
        }
        else
        {
            if (inputManager.moveAmount >= 0.35f)
            {
                speed = jogSpeed;
            }
            else
            {
                speed = walkSpeed;
            }
        }

        // Debug.Log($"Setting speed to: {speed}...");
        return speed;
    }
}
