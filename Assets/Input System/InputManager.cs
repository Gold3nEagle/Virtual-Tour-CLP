// REFERENCE(S):
// 1. Player Locomotion Playlist: https://youtube.com/playlist?list=PLD_vBJjpCwJsqpD8QRPNPMfVUpPFLVGg4

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerControls playerControls;
    private AnimatorManager animatorManager;
    private float moveAmount;

    public Vector2 movementInput;
    public Vector2 cameraInput;
    public float verticalInput;
    public float horizontalInput;
    public float cameraInputX;
    public float cameraInputY;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            // Subscribe events
            playerControls.Player.Move.performed += inputValue => movementInput = inputValue.ReadValue<Vector2>();
            playerControls.Player.Look.performed += inputValue => cameraInput = inputValue.ReadValue<Vector2>();
        }

        playerControls.Enable();
    }

    private void OnDisable() { playerControls.Disable(); }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        // HandleSprintInput();
        // HandleWalkInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputY = cameraInput.y;
        cameraInputX = cameraInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount);
    }
}
