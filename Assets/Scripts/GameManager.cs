using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //public GameObject playerCameraObject;
    [HideInInspector] public bool isInVehicle;
    public GameObject player;
    public GameObject car;
    public GameObject vehicleMainCamera;
    public GameObject vehicleVirtualCamera;
    public Vector3 playerExitVehicleOffset;
    [HideInInspector] public PlayerControls playerControls;

    private GameObject characterOBJ;
    private Rigidbody carBody;
    private float inCarMass;
    private float outsideCarMass = 1000;

    void Awake()
    {
        instance = this;
        playerControls = new PlayerControls();
        Debug.Log("GameManager Awake");
    }

    void Start()
    {
        playerControls.Player.Enable();
        carBody = car.GetComponent<Rigidbody>();
        inCarMass = carBody.mass;
        carBody.mass = outsideCarMass;
        characterOBJ = player.transform.GetChild(1).transform.GetChild(0).gameObject;
    }

    public void SwitchToPlayerControls()
    {
        playerControls.Vehicle.Disable();
        playerControls.Player.Enable();
        isInVehicle = false;
        //player.transform.position = car.transform.position + playerExitVehicleOffset;
        //characterOBJ.transform.localPosition = (car.transform.position + playerExitVehicleOffset) - player.transform.position;
        characterOBJ.transform.position = car.transform.position + playerExitVehicleOffset;
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

}
