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
    private string SavePath;

    [Header("Menus")]
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject controlsMenu;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject mainMenu;

    private void OnDisable()
    {
        AudioManager.instance.Stop("MainMenuBackground");
    }

    private void Start()
    {
        AudioManager.instance.Stop("CarDriving");
        AudioManager.instance.Play("MainMenuBackground");
        int quality = PlayerPrefs.GetInt("currentQ");
        QualitySettings.SetQualityLevel(quality);

        SavePath = $"{Application.persistentDataPath}/saveData.text";
        if (!File.Exists(SavePath))
        {
            continueGameButton.interactable = false;
        }
    }

    public void OnNewGameClicked()
    {
        AudioManager.instance.Play("Click");
        ResetWaypoints();
        PlayerPrefs.SetInt("inMasjidQuest", 0);
        CurrencySystem.instance.currentMoney = 0;
        string saveFilePath = Path.Combine(Application.persistentDataPath, "saveData.text");

        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Save file deleted successfully.");
        }
        else
        {
            Debug.Log("Save file not found.");
        }
        DialogueManager.ResetDatabase(DatabaseResetOptions.KeepAllLoaded);
        DialogueManager.SendUpdateTracker();
        DisableMenuButtons();
        SceneManager.LoadScene(1);
    }

    private void ResetWaypoints()
    {
        string[] waypointsIDs = { "TreeOfLife", "Ahmed", "TicketSeller", "OldMan", "ShopKeeper", "Ali", "Masjid Al Khamis",
            "PicArea", "PicArea (1)", "Bu Yaqoob", "Metal Box", "board for fan tower", "Mohamed" };
        for (int i = 0; i < waypointsIDs.Length; i++)
        {
            PlayerPrefs.SetInt(waypointsIDs[i], 0);
        }
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
    public void OnControlsClicked()
    {
        AudioManager.instance.Play("Click");
        controlsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void OnCreditsClicked()
    {
        AudioManager.instance.Play("Click");
        creditsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void OnBackBtnPressed()
    {
        AudioManager.instance.Play("Click");
        optionMenu.SetActive(false);
        creditsMenu.SetActive(false);
        controlsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OnQuitBtnPressed()
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
