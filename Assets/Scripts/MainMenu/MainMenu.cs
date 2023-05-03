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
        SceneManager.LoadSceneAsync(1);
    }
    public void OnContinueClicked()
    {
        DisableMenuButtons();
        //string save = PlayerPrefs.GetString("SaveDialogue");
        //PersistentDataManager.ApplySaveData(save);
        AdvancedSavingSystem.instance.Load();
        SceneManager.LoadSceneAsync(1);
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
