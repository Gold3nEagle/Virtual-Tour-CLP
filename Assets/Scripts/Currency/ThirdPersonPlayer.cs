using UnityEngine;

public class ThirdPersonPlayer : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    private new Rigidbody rigidbody;
    private Vector3 moveDirection;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Calculate the move direction based on the input
        moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Rotate the character to face the move direction
        if (moveDirection.magnitude > 0f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            rigidbody.rotation = Quaternion.Lerp(rigidbody.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        // Move the character
        rigidbody.MovePosition(rigidbody.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }
}