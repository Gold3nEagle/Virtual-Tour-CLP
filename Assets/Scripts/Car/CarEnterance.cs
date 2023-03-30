using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarEnterance : MonoBehaviour
{
    private BoxCollider thisColl;
    private bool inRange;
    void Start()
    {
        thisColl = GetComponent<BoxCollider>();
    }
    private void OnEnable()
    {
        //playerControls.Player.EnterVehicle.performed += EnterVehicle;
        GameManager.instance.playerControls.Player.EnterVehicle.performed += EnterVehicle;
        GameManager.instance.playerControls.Vehicle.Exit.performed += ExitVehicle;
    }

    private void OnDisable()
    {
        //playerControls.Player.EnterVehicle.performed -= EnterVehicle;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void EnterVehicle(InputAction.CallbackContext context)
    {
        if (!inRange) return;
        Debug.Log("Entering vehicle...");

        GameManager.instance.SwitchToVehicleControls();
    }

    private void ExitVehicle(InputAction.CallbackContext context)
    {
        if(GameManager.instance.isInVehicle)
        {
            GameManager.instance.SwitchToPlayerControls();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
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
