using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class TestSound : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            AudioManager.instance.Play("TestingSound");
        }
    }
}
