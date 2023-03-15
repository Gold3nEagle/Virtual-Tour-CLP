using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float m_horizontalInput;
    private float m_verticalInput;
    private float m_steeringAngle;

    public WheelCollider frontDriverW, frontPassengerW, backDriverW, backPassengerW;
    public Transform rontDriverT, frontPassengerT, backDriverT, backPassengerT;
    public float maxSteerAngle = 30;
    public float motorForce = 50;

    
    public void GetInput()
    {

    }

    private void Steer()
    {

    }

    private void Accelerate()
    {

    }

    private void UpdateWheelPoses()
    {

    }

    private void UpdateWheelPose(WheelCollider collider, Transform transform)
    {

    }

    private void FixedUpdate()
    {
        
    }

}
