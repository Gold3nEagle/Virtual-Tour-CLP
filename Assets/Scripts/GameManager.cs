using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject playerCameraObject;
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToCarCamera()
    {
        playerCameraObject.SetActive(false);
    }

    public void SwitchToPlayerCamera()
    {
        playerCameraObject.SetActive(true);
    }
}
