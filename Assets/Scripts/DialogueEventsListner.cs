using PixelCrushers.DialogueSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEventsListner : MonoBehaviour
{
    public GameObject Ali;
    public static int counter = 0;
    private void Start()
    {
        counter = PlayerPrefs.GetInt("shopQItems");
        Debug.Log(counter);
        Items.OnItemPurchase.AddListener(DidBuyItem);
    }

    private void DidBuyItem(string itemName)
    {
        
        if ("Halwa Showaiter" == itemName)
        {
            
            DialogueLua.SetVariable("Bab.halwa", 1);
            counter += 1;
            QuestLog.SetQuestEntryState("Bab AlBahrain", 3, "success");
            DialogueManager.SendUpdateTracker();
           
        }
        if ("Basbousa" == itemName)
        {

            DialogueLua.SetVariable("Bab.Basbousa", 1);
            counter += 1;
            QuestLog.SetQuestEntryState("Bab AlBahrain", 2, "success");
            DialogueManager.SendUpdateTracker();
        }
        if ("Harees" == itemName)
        {

            DialogueLua.SetVariable("Bab.Harees", 1);
            counter += 1;
            QuestLog.SetQuestEntryState("Bab AlBahrain", 4, "success");
            DialogueManager.SendUpdateTracker();
        }

        if(counter >= 3)
        {
            DialogueLua.SetVariable("Bab.Items", true);
            QuestLog.SetQuestEntryState("Bab AlBahrain", 1, "success");
            QuestLog.SetQuestEntryState("Bab AlBahrain", 5, "active");
            DialogueManager.SendUpdateTracker();
            WaypointManager.instance.RemoveWaypoint("ShopKeeper");
            SetQuestsWaypoints.activeWaypoints.Remove("ShopKeeper");
            WaypointManager.instance.AddWaypoint("Ali", Ali);
            SetQuestsWaypoints.AddToActiveWaypoints("Ali");
            
        }
    }
    public bool CheckBuyItem(string itemName)
    {
        foreach (Item item in ItemsManager.items.List)
        {
            if (item.Name == itemName && item.IsObtained)
            {
                return true;
            }

        }
        return false;
    }
}
