using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // For resume functionality reference, go to "InputManager.cs" (around the top of the file) - `OnResumeBtnClicked()`

    public GameObject pauseMenuPanel;
    public GameObject saveText;

    public void ReturnToMenu()
    {
        AudioManager.instance.Play("Click");
        SceneManager.LoadScene("MainMenu");
        pauseMenuPanel.SetActive(false);
    }

    public void OnClickSaveGame()
    {
        AudioManager.instance.Play("Click");
        AdvancedSavingSystem.instance.Save();
        saveText.SetActive(true);
        Invoke("SetFalse", 0.1f);
    }
}