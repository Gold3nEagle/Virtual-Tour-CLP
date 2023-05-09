using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetQuestsWaypoints : MonoBehaviour
{
    // Start is called before the first frame update
    public string[] questsNames = {"Tree Of Life", "F1 Ticket", "Bab AlBahrain", "The Ancient Mosque", "The Fan Tower", };
    public GameObject[] NPCs;
    public GameObject[] objectsInQuests;
    public static List<string> activeWaypoints = new List<string>();
    public List<string> activeWaypointsCopy = new List<string>();
    void Start()
    {
        for (int i = 0; i < questsNames.Length; i++)
        {
            if (QuestLog.GetQuestState(questsNames[i]).ToString() == "Unassigned")
            {
                string waypointID = "Q" + (i+1);
                WaypointManager.instance.AddQuestWaypoint(waypointID, NPCs[i]);
            }
        }
        activeWaypoints = new List<string>();
        for (int i = 0; i < objectsInQuests.Length; i++)
        {
            if(PlayerPrefs.GetInt(objectsInQuests[i].name) == 1)
            {
                WaypointManager.instance.AddWaypoint(objectsInQuests[i].name, objectsInQuests[i]);
                activeWaypoints.Add(objectsInQuests[i].name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        activeWaypointsCopy = activeWaypoints;
    }

    public void SwitchWaypoint(string ID)
    {
        string[] ids = ID.Split("-");
        WaypointManager.instance.RemoveWaypoint(ids[0]);
        WaypointManager.instance.AddWaypoint(ids[1], GetGameObject(ids[1]));
        activeWaypoints.Remove(ids[0]);
        activeWaypoints.Add(ids[1]);
    }

    public void QuestFinished(string waypointId)
    {
        WaypointManager.instance.RemoveWaypoint(waypointId);
        activeWaypoints.Remove(waypointId);
    }

    private GameObject GetGameObject(string name)
    {
        foreach(GameObject gObject in objectsInQuests)
        {
            if (gObject.name == name) return gObject;
        }
        Debug.LogWarning("Quest game object not found!");
        return null;
    }
}
