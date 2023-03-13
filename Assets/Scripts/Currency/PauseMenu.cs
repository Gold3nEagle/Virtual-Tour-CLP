using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public bool isPaused;

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
           
        }
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuPanel.SetActive(false);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuPanel.SetActive(false);
    }
    public void PauseGame()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f; // Stop or resume game logic
        pauseMenuPanel.SetActive(true); // Show or hide pause menu panel
    }
    public void OnClickSaveGame()
    {
        //AdvancedSavingSystem saveGame = new AdvancedSavingSystem();
        //saveGame.Save();
        AdvancedSavingSystem.instance.Save();
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}