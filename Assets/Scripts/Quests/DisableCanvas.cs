using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DisableCanvas : MonoBehaviour
{
    public Canvas dialogCanvas;    
    void Start()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode arg1)
    {
        if(scene.name != "Game")
        {
            dialogCanvas.renderMode = RenderMode.WorldSpace;
        }
        else
        {
            dialogCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
