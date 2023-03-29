// REFERENCE(S):
// 1. Player Locomotion Playlist: https://youtube.com/playlist?list=PLD_vBJjpCwJsqpD8QRPNPMfVUpPFLVGg4

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerControls playerControls;
    private MainPlayerController mainPlayerController;
    private AnimatorManager animatorManager;
    private MenuUI menuUI;

    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    public float cameraInputX;
    public float cameraInputY;

    public bool sprintBtnInput;
    public bool walkBtnInput;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        mainPlayerController = GetComponent<MainPlayerController>();
        menuUI = new MenuUI();
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            // Subscribe events

            // Movement - Player
            playerControls.Player.Move.performed += inputValue => movementInput = inputValue.ReadValue<Vector2>();
            playerControls.Player.Look.performed += inputValue => cameraInput = inputValue.ReadValue<Vector2>();

            playerControls.Player.Sprint.performed += _ => sprintBtnInput = true;
            playerControls.Player.Sprint.canceled += _ => sprintBtnInput = false;

            playerControls.Player.Walk.performed += _ => walkBtnInput = !walkBtnInput;

            // Interactions
            playerControls.Player.OpenInventory.performed += _ => menuUI.ToggleMenuVisibility(0);
            playerControls.Player.Interact.performed += _ => menuUI.ToggleMenuVisibility(1);
            playerControls.Player.Pause.performed += _ => menuUI.ToggleMenuVisibility(2);
        }

        playerControls.Enable();
    }

    private void OnDisable() { playerControls.Disable(); }

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
        if (walkBtnInput)
        {
            mainPlayerController.isWalking = true;
        }
        else
        {
            mainPlayerController.isWalking = false;
        }
    }

    // === === === === ===
    // Interactions
    // === === === === ===

    // None yet...
}
