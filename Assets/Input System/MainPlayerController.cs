using UnityEngine;
using UnityEngine.InputSystem;

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

    // private void OnEnable()
    // {
    //     playerControls.Player.EnterVehicle.performed += EnterVehicle;
    //     playerControls.Player.OpenMap.performed += OpenMap;
    //     playerControls.Player.ResetCamera.performed += ResetCamera;
    // }

    private void EnterVehicle(InputAction.CallbackContext context)
    {
        // menuUI.ToggleMenuVisibility(1);
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
    // Player Input Handling
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
