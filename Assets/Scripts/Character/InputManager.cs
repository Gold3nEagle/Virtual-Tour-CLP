// REFERENCE(S):
// 1. Player Locomotion Playlist: https://youtube.com/playlist?list=PLD_vBJjpCwJsqpD8QRPNPMfVUpPFLVGg4

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private MainPlayerController mainPlayerController;
    private AnimatorManager animatorManager;

    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    public float cameraInputX;
    public float cameraInputY;

    public bool sprintBtnInput;
    public bool isInVehicle;
    public bool walkBtnInput;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        mainPlayerController = GetComponent<MainPlayerController>();
    }

    // Replaced `OnEnable()` with `Start()` because for literally no reason the player's controls wouldn't work (happened after adding the trees to terrain i guess)
    private void Start()
    {
        // Subscribe events

        // Movement - Player
        GameManager.instance.playerControls.Player.Move.performed += inputValue => movementInput = inputValue.ReadValue<Vector2>();
        GameManager.instance.playerControls.Player.Look.performed += inputValue => cameraInput = inputValue.ReadValue<Vector2>();

        GameManager.instance.playerControls.Player.Sprint.performed += _ => sprintBtnInput = true;
        GameManager.instance.playerControls.Player.Sprint.canceled += _ => sprintBtnInput = false;

        GameManager.instance.playerControls.Player.Walk.performed += _ => walkBtnInput = !walkBtnInput;
    }

    // === === === === ===
    // Movement - Player
    // === === === === ===

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintingInput();
        HandleWalkingInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputY = cameraInput.y;
        cameraInputX = cameraInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount, mainPlayerController.isSprinting, mainPlayerController.isWalking);
    }

    private void HandleSprintingInput()
    {
        if (sprintBtnInput && moveAmount > 0.35f)
        {
            mainPlayerController.isSprinting = true;
        }
        else
        {
            mainPlayerController.isSprinting = false;
        }
    }

    private void HandleWalkingInput()
    {
        if (walkBtnInput && moveAmount > 0.0f)
        {
            mainPlayerController.isWalking = true;
        }
        else
        {
            mainPlayerController.isWalking = false;
        }
    }
}
