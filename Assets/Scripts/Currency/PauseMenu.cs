using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // For resume functionality reference, go to "InputManager.cs" (around the top of the file) - `OnResumeBtnClicked()`

    public GameObject pauseMenuPanel;
    public GameObject saveText;

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        pauseMenuPanel.SetActive(false);
    }

    public void OnClickSaveGame()
    {
        AdvancedSavingSystem.instance.Save();
        saveText.SetActive(true);
        Invoke("SetFalse", 0.1f);
    }
}