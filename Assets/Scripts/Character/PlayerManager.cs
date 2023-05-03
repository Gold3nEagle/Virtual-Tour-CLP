using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private InputManager inputManager;
    private MainPlayerController mainPlayerController;
    private CameraManager cameraManager;
    private bool handleMovement = true;
    public static bool handleMovementStatic = true;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        mainPlayerController = GetComponent<MainPlayerController>();
        cameraManager = FindObjectOfType<CameraManager>();
    }

    private void Update()
    {
        if (handleMovement && handleMovementStatic) { inputManager.HandleAllInputs(); }
        else { inputManager.DoNotHandleAllInputs(); }
    }

    private void FixedUpdate()
    {
        if (handleMovement && handleMovementStatic) { mainPlayerController.HandleAllMovement(); }
        else { mainPlayerController.DoNotHandleAllMovement(); }
    }

    private void LateUpdate()
    {
        if (Time.timeScale != 0.0f) cameraManager.HandleAllCameraMovement();
    }

    public void StopPlayerMovement()
    {
        handleMovement = false;
    }

    public void ContinuePlayerMovement()
    {
        handleMovement = true;
    }
    public static void StopPlayer()
    {
        handleMovementStatic = false;
    }

    public static void ContinuePlayer()
    {
        handleMovementStatic = true;
    }
}
