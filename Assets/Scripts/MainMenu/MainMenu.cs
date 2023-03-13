using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;

   
    private void Start()
    {
        
    }
    public void OnNewGameClicked()
    {
        CurrencySystem.instance.currentMoney = 0;
        DisableMenuButtons();
        SceneManager.LoadSceneAsync("SayedTesting");
    }
    public void OnContinueClicked()
    {
        DisableMenuButtons();
        
        AdvancedSavingSystem.instance.Load();
        SceneManager.LoadSceneAsync("SayedTesting");
    }
    public void OnQuit()
    {
        Application.Quit();
    }
    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }
}
