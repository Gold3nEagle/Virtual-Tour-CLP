using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    public float speed = 5f;
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
    void Update()
    {
        Vector2 movementDirection = playerControls.Land.Move.ReadValue<Vector2>();
        Debug.Log("Moving... -> Direction: " + movementDirection);
        playerRigidbody.AddForce(new Vector3(movementDirection.x, 0, movementDirection.y) * speed, ForceMode.Force);
    }

    // Player movements
    private void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jumping...");
        playerRigidbody.AddForce(Vector3.up * speed, ForceMode.Impulse);
    }
}
