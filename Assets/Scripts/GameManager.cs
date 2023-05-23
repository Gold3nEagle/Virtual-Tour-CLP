using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject cameraOBJ;
    public GameObject player;
    public GameObject characterOBJ;
    public GameObject car;
    public GameObject vehicleMainCamera;
    public GameObject vehicleVirtualCamera;
    public Vector3 playerExitVehicleOffset;
    public TextMeshProUGUI promptTXT;
    [HideInInspector] public bool isInVehicle;
    [HideInInspector] public bool isNearShop = false;
    [HideInInspector] public bool isPlayerWithinCarRange;
    [HideInInspector] public PlayerControls playerControls;

    private Rigidbody carBody;
    private float inCarMass;
    private float outsideCarMass = 1000;

    void Awake()
    {
        instance = this;
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        AudioManager.instance.Play("crowd");
        AudioManager.instance.Play("park");
    }

    private void OnDisable()
    {
        AudioManager.instance.Stop("crowd");
        AudioManager.instance.Stop("park");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            WaypointManager.instance.AddWaypoint("way1", transform.position);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            WaypointManager.instance.AddQuestWaypoint("qWay", new Vector3(100, 0, 100));
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            WaypointManager.instance.RemoveWaypoint("way1");
            WaypointManager.instance.RemoveWaypoint("qWay");
        }
    }

    void Start()
    {
        playerControls.Player.Enable();

        carBody = car.GetComponent<Rigidbody>();
        inCarMass = carBody.mass;
        carBody.mass = outsideCarMass;
    }

    public void SwitchToPlayerControls()
    {
        playerControls.Vehicle.Disable();
        playerControls.Player.Enable();

        isInVehicle = false;
        characterOBJ.transform.position = GetPlayerOffset();
        player.SetActive(true);
        vehicleMainCamera.SetActive(false);
        vehicleVirtualCamera.SetActive(false);
        carBody.mass = outsideCarMass;
        WaypointManager.instance.AddCarWaypoint();
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

        HidePrompt();
        WaypointManager.instance.RemoveWaypoint("Car");
    }

    public Vector3 GetPlayerOffset()
    {
        return car.transform.position + playerExitVehicleOffset;
    }

    public void ShowPrompt(string message)
    {
        promptTXT.text = message;
        promptTXT.gameObject.SetActive(true);
    }

    public void HidePrompt()
    {
        promptTXT.gameObject.SetActive(false);
    }

}
