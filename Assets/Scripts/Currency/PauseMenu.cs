using PixelCrushers.DialogueSystem;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // For resume functionality reference, go to "InputManager.cs" (around the top of the file) - `OnResumeBtnClicked()`

    public GameObject pauseMenuPanel;
    public GameObject saveText;
    public string save;

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        pauseMenuPanel.SetActive(false);
    }

    public void OnClickSaveGame()
    {
        //string save = PersistentDataManager.GetSaveData();
        //PlayerPrefs.SetString("SaveDialogue",save);
        AdvancedSavingSystem.instance.Save();
        saveText.SetActive(true);
        Invoke("SetFalse", 0.1f);
    }

 

}