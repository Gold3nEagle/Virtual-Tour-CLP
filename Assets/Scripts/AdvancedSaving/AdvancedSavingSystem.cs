using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class AdvancedSavingSystem : MonoBehaviour
{
    public static AdvancedSavingSystem instance;
    private string Savepath;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Savepath = $"{Application.persistentDataPath}/saveData.text";
    }


    public void Save()
    {
        var state = LoadFile();
        CaptureState(state);
        SaveFile(state);

    }

   
    public void Load()
    {
        var state = LoadFile();
        RestoreState(state);

    }

    private void SaveFile(object state)
    {
        using (var stream = File.Open(Savepath, FileMode.Create))
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, state);
        }
    }

    private Dictionary<string, object> LoadFile()
    {
        if (!File.Exists(Savepath))
        {
            return new Dictionary<string, object>();
        }
        using (FileStream stream = File.Open(Savepath, FileMode.Open))
        {
            var formatter = new BinaryFormatter();
            return (Dictionary<string, object>)formatter.Deserialize(stream);
        }
    }

    private void CaptureState(Dictionary<string, object> state)
    {
        foreach (var saveable in FindObjectsOfType<SaveableEntity>())
        {
            state[saveable.Id] = saveable.CaptureState();
        }
    }
    private void RestoreState(Dictionary<string, object> state)
    {
        foreach(var saveable in FindObjectsOfType<SaveableEntity>())
        {
            if(state.TryGetValue(saveable.Id, out object value))
            {
                saveable.RestoreState(value);
            }
        }
    }
}
