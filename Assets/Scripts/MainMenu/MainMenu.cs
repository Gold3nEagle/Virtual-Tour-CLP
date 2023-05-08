using PixelCrushers.DialogueSystem;
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

    [Header("Menus")]
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject mainMenu;

    private void Start()
    {
        int quality = PlayerPrefs.GetInt("currentQ");
        QualitySettings.SetQualityLevel(quality);

        Savepath = $"{Application.persistentDataPath}/saveData.text";
        if (!File.Exists(Savepath))
        {
            continueGameButton.interactable = false;
        }
    }
    public void OnNewGameClicked()
    {
        AudioManager.instance.Play("Click");
        CurrencySystem.instance.currentMoney = 0;
        string saveFilePath = Path.Combine(Application.persistentDataPath, "saveData.text");
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            DialogueManager.ResetDatabase(DatabaseResetOptions.KeepAllLoaded);
            DialogueManager.SendUpdateTracker();
            Debug.Log("Save file deleted successfully.");
        }
        else
        {
            Debug.Log("Save file not found.");
        }
        DisableMenuButtons();
        SceneManager.LoadScene(1);
    }
    public void OnContinueClicked()
    {
        AudioManager.instance.Play("Click");
        DisableMenuButtons();
        string save = PlayerPrefs.GetString("SaveDialogue");
        PersistentDataManager.ApplySaveData(save);
        AdvancedSavingSystem.instance.Load();
        SceneManager.LoadScene(1);
    }
    public void OnOptionClicked()
    {
        AudioManager.instance.Play("Click");
        optionMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void OnCreditsClicked()
    {
        AudioManager.instance.Play("Click");
        creditsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void OnBackFromOption()
    {
        AudioManager.instance.Play("Click");
        optionMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OnBackFromCreditsMenu()
    {
        AudioManager.instance.Play("Click");
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OnQuit()
    {
        AudioManager.instance.Play("Click");
        Application.Quit();
    }
    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }
}
