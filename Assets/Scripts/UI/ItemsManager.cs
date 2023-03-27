using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour, ISaveable
{
    public static Items items;

    [SerializeField] private List<ItemScriptableObj> itemsScriptableObjects;

    private void Awake()
    {
        items = new Items();
        PopulateItems();
    }

    /// <summary>
    /// Populates the items list with items
    /// </summary>
    private void PopulateItems()
    {
        items.List.Clear();

        for (int i = 0; i < itemsScriptableObjects.Count; i++)
        {
            Item item = new Item(itemsScriptableObjects[i]);
            items.AddItem(item);
        }
    }

    [ContextMenu("View list in JSON")]
    public void ViewListInJSON() { Debug.Log(JsonUtility.ToJson(items)); }

    // === === === === ===
    // Saving/ Loading related stuff
    // === === === === ===

    public object CaptureState() => new SaveData { itemList = items };

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;
        items = saveData.itemList;

        Debug.Log("Length is: " + items.List.Count);

        foreach (Item item in items.List)
        {
            item.PopulateItemInfo();

            if (item.IsObtained)
            {
                Debug.Log($"{item.IsObtained} (BHD {item.Price}.0) - obtained: {item.IsObtained}");
            }
        }
    }

    [Serializable]
    public struct SaveData
    {
        public Items itemList;
    }
}
