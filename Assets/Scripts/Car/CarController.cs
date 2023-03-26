using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    public float acceleration = 22f;       // Acceleration force of the vehicle
    public float steering = 10f;           // Steering speed of the vehicle
    public float maxSpeed = 35f;           // Maximum speed of the vehicle
    public float brakePower = 10f;         // Brake power of the vehicle
    public float getSpeed;
    public float powerSteerBellowSpeed = 20;
    public Transform[] wheels;

    private Rigidbody rb;                  // Reference to the rigidbody of the vehicle
    [SerializeField]private bool isOnGround;
    private float maxZRotation = 15f;        // max rotation for x and z axis to prevent the car from flipping
    private float maxXRotation = 60f;
    private float raycastDistance = 1.2f;   // Distance of the raycast from the car's center
    private bool isBraking;
    private PlayerControls playerControls;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();    // Get the rigidbody component of the vehicle
        playerControls = new PlayerControls();
    }
    private void OnEnable()
    {
        playerControls.Vehicle.Enable();
    }

    private void OnDisable()
    {
        playerControls.Vehicle.Disable();
    }

    private void FixedUpdate()
    {
        CheckGround();
        Brake();
        Accelerate();
        Steer();
        LockRotation();
        LimitSpeed();
        RotateWheels();
        getSpeed = rb.velocity.magnitude;
        if (isBraking) isBraking = false;
    }

    // Check if the vehicle is on the ground
    private void CheckGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (-transform.up * 0.05f), out hit, raycastDistance))
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }
    }

    // Accelerate the vehicle
    private void Accelerate()
    {
        if (isBraking || !isOnGround) return;
        float currentAcc = acceleration;
        Vector2 moveDirection = playerControls.Vehicle.Drive.ReadValue<Vector2>();

        if (moveDirection.y >= 1)
        {
            if (rb.velocity.magnitude < 5)
            {
                currentAcc *= 2;
            }
            rb.AddForce(transform.forward * currentAcc, ForceMode.Acceleration);
        }
        else if (moveDirection.y <= -1)
        {
            if (rb.velocity.magnitude < 5)
            {
                currentAcc *= 2;
            }
            rb.AddForce(-transform.forward * currentAcc, ForceMode.Acceleration);
        }
    }

    // Brake the vehicle
    private void Brake()
    {
        if(playerControls.Vehicle.Brake.inProgress && rb.velocity.magnitude > 0 && isOnGround)
        {
            isBraking = true;
            if (isMovingBackwards())
            {
                rb.AddForce(transform.forward * brakePower, ForceMode.Acceleration);
            }
            else
            {
                rb.AddForce(-transform.forward * brakePower, ForceMode.Acceleration);
            }
        }
    }
    private void Steer()
    {
        float currentCarSpeed = rb.velocity.magnitude;
        Vector2 moveDirection = playerControls.Vehicle.Steer.ReadValue<Vector2>();
        if (isOnGround && currentCarSpeed > 0.1f && (Vector3.Dot(rb.velocity.normalized, transform.forward) > 0 || isMovingBackwards()))
        {
            // Calculate the steering angle based on the current speed
            float speedFactor;
            if (currentCarSpeed < powerSteerBellowSpeed)
            {
                speedFactor = 2;
            }
            else
            {
                speedFactor = currentCarSpeed / maxSpeed;
            }
            float steeringAngle = moveDirection.x * 30f * speedFactor;

            // Apply the steering angle to the vehicle's transform
            transform.Rotate(Vector3.up * steeringAngle * Time.fixedDeltaTime);

            if(isMovingBackwards())
            {
                steeringAngle *= 0.6f;
                steeringAngle = -steeringAngle;
            }
            // Steer the front wheels
            for (int i = 0; i < wheels.Length - 2; i++)
            {
                Vector3 frontWheelEulerAngles = wheels[i].localEulerAngles;
                wheels[i].localEulerAngles = new Vector3(frontWheelEulerAngles.x, steeringAngle, frontWheelEulerAngles.z);
            }
        }
    }


    private void LockRotation()
    {
        // Clamp the x and z rotations of the car to prevent flipping
        Vector3 rotation = transform.rotation.eulerAngles;
        float newXRotation = ClampXRotation(rotation.x);
        float newZRotation = ClampZRotation(rotation.z);
        rotation.x = newXRotation;
        rotation.z = newZRotation;
        transform.rotation = Quaternion.Euler(rotation);
    }
    // Clamp the rotation angle to prevent flipping
    private float ClampZRotation(float rotation)
    {
        if (rotation > 180f)
        {
            rotation -= 360f; // convert to -180 to 180 range
        }
        return Mathf.Clamp(rotation, -maxZRotation, maxZRotation);
    }

    private float ClampXRotation(float rotation)
    {
        if (rotation > 180f)
        {
            rotation -= 360f; // convert to -180 to 180 range
        }
        return Mathf.Clamp(rotation, -maxXRotation, maxXRotation);
    }

    // Limit the vehicle's speed
    private void LimitSpeed()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    // Rotate the wheels according to the vehicle's speed
    private void RotateWheels()
    {
        float rotationAngle = rb.velocity.magnitude / (2 * Mathf.PI * wheels[0].localScale.y) * 360f;
        if (isMovingBackwards()) rotationAngle = -rotationAngle;
        foreach (Transform wheel in wheels)
        {
            wheel.Rotate(Vector3.right, rotationAngle * Time.fixedDeltaTime);
        }
    }

    private bool isMovingBackwards()
    {
        return Vector3.Dot(rb.velocity.normalized, -transform.forward) > 0;
    }
     
}
