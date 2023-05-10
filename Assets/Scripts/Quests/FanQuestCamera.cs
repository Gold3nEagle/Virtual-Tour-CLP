using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanQuestCamera : MonoBehaviour
{
    public GameObject fanCamera;
    public GameObject uiPanel;
    public GameObject miniMap;
    public GameObject waypointsHolder;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void TakePicture()
    {
        Switch();
        GameManager.instance.cameraOBJ.SetActive(false);
        fanCamera.SetActive(true);
        
    }
    public void Switch()
    {
        // Disable panel1 and enable panel2
        uiPanel.SetActive(false);
        //miniMap.SetActive(false);
        waypointsHolder.SetActive(false);
        PlayerManager.StopPlayer();
        // Wait for 3 seconds before reversing the process
        StartCoroutine(WaitAndSwitch());
    }

    IEnumerator WaitAndSwitch()
    {
        yield return new WaitForSeconds(3);

        
        uiPanel.SetActive(true);
        //miniMap.SetActive(true);
        waypointsHolder.SetActive(true);
        fanCamera.SetActive(false);
        PlayerManager.ContinuePlayer();
        GameManager.instance.cameraOBJ.SetActive(true);
       
    }

   
}