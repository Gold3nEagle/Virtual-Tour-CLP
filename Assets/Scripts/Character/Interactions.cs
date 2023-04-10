using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactions : MonoBehaviour
{
    private PlayerControls playerControls;
    private MenuUI menuUI;

    private void Awake()
    {
        menuUI = new MenuUI();
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Interactions.Enable();

        // Subscribe events
        playerControls.Interactions.OpenInventory.performed += _ => menuUI.ToggleMenuVisibility(0);
        playerControls.Interactions.Interact.performed += _ => menuUI.ToggleMenuVisibility(1);
        playerControls.Interactions.Pause.performed += _ => menuUI.ToggleMenuVisibility(2);
        playerControls.Interactions.EnterExitVehicle.performed += EnterExitVehicle;
    }

    private void OnDisable() { playerControls.Interactions.Disable(); }

    // === === === === ===
    // Interactions
    // === === === === ===

    public void OnResumeBtnClicked() { menuUI.ResumeGame(); }

    private void EnterExitVehicle(InputAction.CallbackContext context)
    {
        if (!GameManager.instance.isInVehicle)
        {
            if (GameManager.instance.isPlayerWithinCarRange)
            {
                GameManager.instance.SwitchToVehicleControls();
            }
        }
        else
        {
            GameManager.instance.SwitchToPlayerControls();
        }
    }

    public void EnableCursor()
    {
        SetCursorVisibility(true);
    }
    public void DisableCursor()
    {
        SetCursorVisibility(false);
    }

    private void SetCursorVisibility(bool visible)
    {
        if (visible)
        {
            // Unhide and unlock the cursor
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            // Hide and lock the cursor
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
