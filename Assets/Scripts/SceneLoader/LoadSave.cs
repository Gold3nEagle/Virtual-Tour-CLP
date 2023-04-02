using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSave : MonoBehaviour
{
    private void Awake()
    {
        if (AdvancedSavingSystem.instance == null)
        {
            Debug.Log("Player's save data won't be loaded, please run from 'Main Menu' scene...");
            this.enabled = false;
            return;
        }
    }

    void Start() { AdvancedSavingSystem.instance.Load(); }
}
