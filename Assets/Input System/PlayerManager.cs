using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private InputManager inputManager;
    private MainPlayerController mainPlayerController;
    private CameraManager cameraManager;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        mainPlayerController = GetComponent<MainPlayerController>();
        cameraManager = FindObjectOfType<CameraManager>();
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        mainPlayerController.HandleAllMovement();
    }

    private void LateUpdate()
    {
        cameraManager.HandleAllCameraMovement();
    }
}
