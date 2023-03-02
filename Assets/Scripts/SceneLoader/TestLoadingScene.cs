using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLoadingScene : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            SceneLoader.instance.ReloadScene();
        }
    }
}
