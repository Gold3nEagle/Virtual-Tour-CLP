using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetQuestsWaypoints : MonoBehaviour
{
    // Start is called before the first frame update
    public string[] questsNames = {"Tree Of Life", "F1 Ticket", "Bab AlBahrain", "The Ancient Mosque", "The Fan Tower", };
    public GameObject[] NPCs;
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
        //if((QuestLog.GetQuestState("The Fan Tower"))
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuestStarted(string waypointID)
    {
        WaypointManager.instance.RemoveWaypoint(waypointID);
    }
}
