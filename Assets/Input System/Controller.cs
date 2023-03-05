using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    public Animator playerAnim;
    public float walkSpeed = 10f;
    public float jumpSpeed = 5f;
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
        playerControls.Land.Jump.performed += Jump;
    }

    private void OnDisable()
    {
        playerControls.Land.Disable();

        // Unsubscribe events
        playerControls.Land.Jump.performed -= Jump;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movementDirection = playerControls.Land.Move.ReadValue<Vector2>();
        Debug.Log("Moving... -> Direction: " + movementDirection);
        playerRigidbody.AddForce(new Vector3(movementDirection.x, 0, movementDirection.y) * walkSpeed, ForceMode.Force);

        if (movementDirection == Vector2.zero)
        {
            playerAnim.SetTrigger("idle");
            playerAnim.ResetTrigger("walk");
        }
        else
        {
            playerAnim.SetTrigger("walk");
            playerAnim.ResetTrigger("idle");

            float _targetRotation = Mathf.Atan2(movementDirection.x, movementDirection.y) * Mathf.Rad2Deg;
            float _rotationVelocity = 20f;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, 0.07f);

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        if (movementDirection == Vector2.left)
        {
            Debug.Log("Turning left...");
        }
        else if (movementDirection == Vector2.right)
        {
            Debug.Log("Turning right...");
        }
    }

    // Player movements
    private void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jumping...");
        playerRigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
    }
}
