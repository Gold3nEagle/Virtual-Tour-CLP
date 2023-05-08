using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactions : MonoBehaviour
{
    private PlayerControls playerControls;
    public MenuUI menuUI;

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
        playerControls.Interactions.Interact.performed += CheckShopNearby;
        playerControls.Interactions.Pause.performed += _ => menuUI.ToggleMenuVisibility(2);
        playerControls.Interactions.EnterExitVehicle.performed += EnterExitVehicle;
    }

    private void OnDisable() { playerControls.Interactions.Disable(); }

    // === === === === ===
    // Interactions
    // === === === === ===

    public void OnResumeBtnClicked()
    {
        AudioManager.instance.Play("Click");
        menuUI.ResumeGame();
    }

    private void EnterExitVehicle(InputAction.CallbackContext context)
    {
        if (!GameManager.instance.isInVehicle)
        {
            if (GameManager.instance.isPlayerWithinCarRange)
            {
                AudioManager.instance.Play("StartEngine");
                AudioManager.instance.Play("CarDriving");
                GameManager.instance.SwitchToVehicleControls();
            }
        }
        else
        {
            AudioManager.instance.Stop("CarDriving");
            AudioManager.instance.Play("StopEngine");
            GameManager.instance.SwitchToPlayerControls();
        }
    }

    private void CheckShopNearby(InputAction.CallbackContext context)
    {
        if (GameManager.instance.isNearShop)
        {
            GameManager.instance.HidePrompt();
            menuUI.ToggleMenuVisibility(1);
        }
        else
        {
            return;
        }
    }
}
