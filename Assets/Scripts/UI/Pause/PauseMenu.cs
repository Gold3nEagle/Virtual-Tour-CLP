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
    [SerializeField] private GameObject interactionsGO;
    private Interactions interactions;

    private void Start()
    {
        interactions = interactionsGO.GetComponent<Interactions>();
    }

    public void ReturnToMenu()
    {
        AudioManager.instance.Stop("CarDriving");
        AudioManager.instance.Play("Click");
        interactions.menuUI.ToggleMenuVisibility(2);
        SceneManager.LoadScene("MainMenu");
        pauseMenuPanel.SetActive(false);
    }

    public void OnClickSaveGame()
    {
        AudioManager.instance.Play("Click");
        string save = PersistentDataManager.GetSaveData();
        PlayerPrefs.SetString("SaveDialogue", save);
        AdvancedSavingSystem.instance.Save();
        saveText.SetActive(true);
        Invoke("SetFalse", 0.1f);
    }
}