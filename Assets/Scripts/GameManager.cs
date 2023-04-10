using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public GameObject characterOBJ;
    public GameObject car;
    public GameObject vehicleMainCamera;
    public GameObject vehicleVirtualCamera;
    public Vector3 playerExitVehicleOffset;
    /*[HideInInspector]*/ public bool isInVehicle;
    [HideInInspector] public bool isPlayerWithinCarRange;
    [HideInInspector] public PlayerControls playerControls;

    private Rigidbody carBody;
    private float inCarMass;
    private float outsideCarMass = 1000;
    //private Camera vehicleCamComponent;

    void Awake()
    {
        instance = this;
        playerControls = new PlayerControls();
    }

    void Start()
    {
        playerControls.Player.Enable();

        carBody = car.GetComponent<Rigidbody>();
        inCarMass = carBody.mass;
        carBody.mass = outsideCarMass;
        //vehicleCamComponent = vehicleMainCamera.GetComponent<Camera>();
    }

    public void SwitchToPlayerControls()
    {
        playerControls.Vehicle.Disable();
        playerControls.Player.Enable();

        isInVehicle = false;
        //player.transform.position = car.transform.position + playerExitVehicleOffset;
        //characterOBJ.transform.localPosition = (car.transform.position + playerExitVehicleOffset) - player.transform.position;
        characterOBJ.transform.position = GetPlayerOffset();
        player.SetActive(true);
        vehicleMainCamera.SetActive(false);
        vehicleVirtualCamera.SetActive(false);
        carBody.mass = outsideCarMass;
    }

    public void SwitchToVehicleControls()
    {
        playerControls.Player.Disable();
        playerControls.Vehicle.Enable();

        isInVehicle = true;
        player.SetActive(false);
        vehicleMainCamera.SetActive(true);
        vehicleVirtualCamera.SetActive(true);
        carBody.mass = inCarMass;
    }

    public Vector3 GetPlayerOffset()
    {
        return car.transform.position + playerExitVehicleOffset;
    }

    //public Camera GetActiveCamera()
    //{
    //    if (player.activeInHierarchy)
    //    {
    //        Debug.Log("Returning main cam..");
    //        return Camera.main;
    //    }
    //    Debug.Log("Returning vehicle cam..");
    //    return vehicleCamComponent;
    //}
}
