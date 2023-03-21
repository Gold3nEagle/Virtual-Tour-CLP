using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsLimit : MonoBehaviour
{
    public int FPS = 30;

    // Set the target frame rate to 60 FPS
    void Start()
    {
        Application.targetFrameRate = FPS;
    }

    // Update is called once per frame
    void Update()
    {
        // Your code here
    }
}
