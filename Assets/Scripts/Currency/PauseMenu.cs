using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public bool isPaused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0f : 1f; // Stop or resume game logic
            pauseMenuPanel.SetActive(isPaused); // Show or hide pause menu panel
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuPanel.SetActive(false);
    }
    //public void OnClickSaveGame()
    //{
    //    DataManager.instance.SaveGame();
    //}

    public void QuitGame()
    {
        Application.Quit();
    }
}