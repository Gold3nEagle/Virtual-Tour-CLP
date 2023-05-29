using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    public float transitionSpeed = 0.8f;
    public enum Scenes {MainMenu, Game}; //the first scene in this enum should be the first scene in the game
    [SerializeField] private Image loadingImage;
    private float copyTransSpeed;
    private Scenes currentScene; //it will take by default the first scene in the enum
    private enum Action { none, show, hide };
    private Action currentAction;
    void Awake()
    {
        if (instance == null)     //checking if instance is null or not becuase we only need one instance of this class
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);  //this will make the gameobject to move to all scenes not only main menu
        copyTransSpeed = transitionSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAction == Action.show)   // if the action is "show", black screen will start appearing
        {
            Show();
        }
        else if (currentAction == Action.hide)  // if the action is "hide", black screen will start disappearing
        {
            Hide();
        }
    }

    private void Show()
    {
        Color tempClr = loadingImage.color;
        tempClr.a += Time.deltaTime * transitionSpeed;  // increase alpha value of the color over time
        loadingImage.color = tempClr;  // update the color of the black screen
        if (loadingImage.color.a >= 1f)   // check if black screen has fully appeared
        {
            SceneManager.LoadScene(currentScene.ToString());  
            currentAction = Action.hide;
        }
    }

    private void Hide()
    {
        Color tempClr = loadingImage.color;
        tempClr.a -= Time.deltaTime * transitionSpeed;  // decrease alpha value of the color over time
        loadingImage.color = tempClr;  // update the color of the black screen
        if (loadingImage.color.a <= 0f)  // check if black screen has fully disappeared
        {
            currentAction = Action.none;
            loadingImage.gameObject.SetActive(false);  
            transitionSpeed = copyTransSpeed;  // reset the transition speed to the original value
        }
    }

    /// <summary>
    /// Loads the specified scene by its name
    /// </summary>
    /// <param name="sceneName">The scene to be loaded</param>
    public void LoadScene(Scenes sceneName)   //this function will be called from any script to move to any scene
    {                                               //usage: SceneLoader.instance.LoadScene(SceneLoader.WhichScene.Level0);
        loadingImage.gameObject.SetActive(true);
        currentAction = Action.show;
        this.currentScene = sceneName;
    }

    /// <summary>
    /// Loads the specified scene with the specified transition speed
    /// </summary>
    /// <param name="sceneName">The scene to be loaded</param>
    /// <param name="transitioningSpeed">The speed of the transition</param>
    public void LoadScene(Scenes sceneName, float transitioningSpeed) //same as the previous function but with this function you can specifiy the speed of transitioning
    {
        transitionSpeed = transitioningSpeed;
        LoadScene(sceneName);
    }

    /// <summary>
    /// Reloads the current scene
    /// </summary>
    public void ReloadScene()   //reload the current scene
    {
        LoadScene(currentScene);
        //loadingImage.gameObject.SetActive(true);
        //currentAction = Action.show;
        
    }

    /// <summary>
    /// Reloads the current scene with the specified transition speed
    /// </summary>
    /// <param name="transitioningSpeed">The speed of the transition</param>
    public void ReloadScene(float transitioningSpeed)
    {
        transitionSpeed = transitioningSpeed;
        ReloadScene();
    }

    /// <summary>
    /// Checks whether the scene is transitioning or not
    /// </summary>
    /// <returns>True if the scene is transitioning, false otherwise</returns>
    public bool IsTransitioning()   //this function will be called to check wither the scene is transitioning or not
    {
        return currentAction != Action.none;
    }
}
