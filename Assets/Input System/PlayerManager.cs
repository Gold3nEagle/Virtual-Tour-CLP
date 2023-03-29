using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    MainPlayerController mainPlayerController;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        mainPlayerController = GetComponent<MainPlayerController>();
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        mainPlayerController.HandleAllMovement(); 
    }
}
