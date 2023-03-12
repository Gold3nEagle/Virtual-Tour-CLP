using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
public class DataManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    private GameData gameData;
    private List<IDataPresistence> dataPresistenceObjects;
    private FileDataHandler dataHandler;
   public static DataManager instance { get; private set; }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       
        this.dataPresistenceObjects = FindAllDataPresistenceObjects();
        LoadGame();
    }
    
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found more than one data manager in the scene. Destroying the new instance.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }
   

    public void NewGame()
    {
        
        this.gameData = new GameData();
        CurrencySystem.instance.ResetValues(0);
    }
    public void LoadGame()
    {
        //load any saved data using the file handler
        this.gameData = dataHandler.Load();

        if(this.gameData == null)
        {
            Debug.Log("No data was found. A new game should be started before data can be loaded.");
            return;
        }
        foreach (IDataPresistence dataPresistenceObj in dataPresistenceObjects)
        {
            dataPresistenceObj.LoadData(gameData);
        }
        Debug.Log("Loaded Money = " + gameData.money);
    }
    //private void OnApplicationQuit()
    //{
    //    SaveGame();
    //}

    public void SaveGame()
    {
        if(this.gameData == null)
        {
            Debug.Log("No data was found. A new game should be started before data can be loaded.");
            return;
        }
        foreach (IDataPresistence dataPresistenceObj in dataPresistenceObjects)
        {
            dataPresistenceObj.SaveData(gameData);
        }
        Debug.Log("Saved Money = " + gameData.money);
        //save data to file using data handler
        dataHandler.Save(gameData);
    }

    private List<IDataPresistence> FindAllDataPresistenceObjects()
    {
        IEnumerable<IDataPresistence> dataPresistencesObjects = FindObjectsOfType<MonoBehaviour>(true)
            .OfType<IDataPresistence>();
        return new List<IDataPresistence>(dataPresistencesObjects);
    }

    public bool gameDataExist()
    {
        return gameData != null;
    }
}
