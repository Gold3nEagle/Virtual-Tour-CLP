using UnityEngine;

public class CarController : MonoBehaviour
{
    private float m_horizontalInput;
    private float m_verticalInput;
    private float m_steeringAngle;

    public WheelCollider frontDriverW, frontPassengerW, backDriverW, backPassengerW;
    public Transform frontDriverT, frontPassengerT, backDriverT, backPassengerT;
    public float maxSteerAngle = 30;
    public float motorForce = 50;
    public float brakeForce = 500;


    public void GetInput()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");
    }

    private void Steer()
    {
        m_steeringAngle = maxSteerAngle * m_horizontalInput;
        frontDriverW.steerAngle = m_steeringAngle;
        frontPassengerW.steerAngle = m_steeringAngle;
    }

    private void Accelerate()
    {
        frontDriverW.motorTorque = m_verticalInput * motorForce;
        frontPassengerW.motorTorque = m_verticalInput * motorForce;
    }

    private void Brake()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            frontDriverW.brakeTorque = brakeForce;
            frontPassengerW.brakeTorque = brakeForce;
            backDriverW.brakeTorque = brakeForce;
            backPassengerW.brakeTorque = brakeForce;
        }
        else
        {
            frontDriverW.brakeTorque = 0;
            frontPassengerW.brakeTorque = 0;
            backDriverW.brakeTorque = 0;
            backPassengerW.brakeTorque = 0;
        }
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontDriverW, frontDriverT);
        UpdateWheelPose(frontPassengerW, frontPassengerT);
        UpdateWheelPose(backDriverW, backDriverT);
        UpdateWheelPose(backPassengerW, backPassengerT);
    }

    private void UpdateWheelPose(WheelCollider collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;
        collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        Brake();
        UpdateWheelPoses();
    }
}
