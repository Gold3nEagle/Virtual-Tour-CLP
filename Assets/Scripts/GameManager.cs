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
    [HideInInspector] public PlayerControls playerControls;

    void Awake()
    {
        instance = this;
        playerControls = new PlayerControls();
        Debug.Log("GameManager Awake");
    }

    void Start()
    {
        playerControls.Player.Enable();
        Debug.Log("Player: " + playerControls.Player.enabled);
    }

    public void SwitchToPlayerControls()
    {
        playerControls.Vehicle.Disable();
        playerControls.Player.Enable();
        isInVehicle = false;
        //TODO add new location to player
        player.SetActive(true);
    }

    public void SwitchToVehicleControls()
    {
        playerControls.Player.Disable();
        playerControls.Vehicle.Enable();
        isInVehicle = true;
        player.SetActive(false);
    }

}
