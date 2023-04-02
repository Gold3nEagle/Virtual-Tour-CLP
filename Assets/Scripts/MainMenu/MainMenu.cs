using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;
    private string Savepath;

    private void Start()
    {
        Savepath = $"{Application.persistentDataPath}/saveData.text";
        if (!File.Exists(Savepath))
        {
            continueGameButton.interactable = false;
        }
    }
    public void OnNewGameClicked()
    {
        CurrencySystem.instance.currentMoney = 0;
        DisableMenuButtons();
        SceneManager.LoadSceneAsync("White-Boxing");
    }
    public void OnContinueClicked()
    {
        DisableMenuButtons();

        AdvancedSavingSystem.instance.Load();
        SceneManager.LoadSceneAsync("White-Boxing");
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
