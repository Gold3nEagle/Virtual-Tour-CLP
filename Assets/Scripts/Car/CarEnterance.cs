using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarEnterance : MonoBehaviour
{
    private BoxCollider thisColl;
    private PlayerControls playerControls;
    private bool inRange;
    void Awake()
    {
        playerControls = new PlayerControls();
    }
    void Start()
    {
        thisColl = GetComponent<BoxCollider>();
    }
    private void OnEnable()
    {
        playerControls.Land.EnterVehicle.performed += EnterVehicle;
    }

    private void OnDisable()
    {
        playerControls.Land.EnterVehicle.performed -= EnterVehicle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void EnterVehicle(InputAction.CallbackContext context)
    {
        if (!inRange) return;
        Debug.Log("Entering vehicle...");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            inRange = true;
            Debug.Log("You can enter the car now, press E to enter");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
        }
    }
}
