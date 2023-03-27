using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AdvancedSavingSystem.instance.Load();
    }

}
