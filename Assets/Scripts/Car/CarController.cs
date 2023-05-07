using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour, ISaveable
{
    public float acceleration = 22f;       // Acceleration force of the vehicle
    public float steering = 10f;           // Steering speed of the vehicle
    public float maxSpeed = 35f;           // Maximum speed of the vehicle
    public float brakePower = 10f;         // Brake power of the vehicle
    public float getSpeed;
    public float powerSteerBellowSpeed = 20;
    public CinemachineFreeLook freeLookCam;
    public float camShakeIntensity = 1f;
    public Transform[] wheels;

    [SerializeField] private bool isOnGround;
    private Rigidbody rb;                  // Reference to the rigidbody of the vehicle
    private CinemachineBasicMultiChannelPerlin[] noise;
    private float maxZRotation = 15f;        // max rotation for x and z axis to prevent the car from flipping
    private float maxXRotation = 60f;
    private float raycastDistance = 1.2f;   // Distance of the raycast from the car's center
    private bool isBraking;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();    // Get the rigidbody component of the vehicle
    }

    private void Start()
    {
        // Get the noise components from the three rigs of the Cinemachine FreeLook Camera
        noise = new CinemachineBasicMultiChannelPerlin[3];
        for (int i = 0; i < 3; i++)
        {
            var vcam = freeLookCam.GetRig(i);
            noise[i] = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            if (noise[i] == null)
            {
                // Add the CinemachineBasicMultiChannelPerlin component if it doesn't exist
                noise[i] = vcam.AddCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            }
        }
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
        HandleEngineSound();
        ShakeCamera();
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
        Vector2 moveDirection = GameManager.instance.playerControls.Vehicle.Drive.ReadValue<Vector2>();

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
        if (GameManager.instance.playerControls.Vehicle.Brake.inProgress && rb.velocity.magnitude > 0 && isOnGround)
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
        Vector2 moveDirection = GameManager.instance.playerControls.Vehicle.Steer.ReadValue<Vector2>();
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

            if (isMovingBackwards())
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

    private void HandleEngineSound()
    {
        if (AudioManager.instance.IsPlaying("CarDriving"))
        {
            float engineSoundLevel = getSpeed / maxSpeed;
            AudioManager.instance.SetSoundVolumeTo("CarDriving", engineSoundLevel);
        }
    }

    private void ShakeCamera()
    {
        if (getSpeed >= maxSpeed)
        {
            for (int i = 0; i < 3; i++)
            {
                noise[i].m_AmplitudeGain = camShakeIntensity;
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                noise[i].m_AmplitudeGain = 0f;
            }
        }
    }

    public object CaptureState()
    {
        //pos = transform.position;
        return new SaveData
        {
            //playerPostion = pos
            carPostionX = transform.position.x,
            carPostionY = transform.position.y,
            carPostionZ = transform.position.z,
            carRotationX = transform.rotation.x,
            carRotationY = transform.rotation.y,
            carRotationZ = transform.rotation.z,
            carRotationW = transform.rotation.w
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;
        Vector3 savedPos = new Vector3(saveData.carPostionX, saveData.carPostionY, saveData.carPostionZ);
        transform.position = savedPos;
        Quaternion carRotation = new Quaternion(saveData.carRotationX, saveData.carRotationY, saveData.carRotationZ, saveData.carRotationW);
        transform.rotation = carRotation;

    }

    [Serializable]
    public struct SaveData
    {
        public float carPostionX;
        public float carPostionY;
        public float carPostionZ;
        public float carRotationX;
        public float carRotationY;
        public float carRotationZ;
        public float carRotationW;
    }

}