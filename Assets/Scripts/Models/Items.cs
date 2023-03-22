using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Items
{
    private List<Item> items;
    public List<Item> List { get => items; }

    public Items()
    {
        items = new List<Item>();
    }

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    // public void DisplayInventoryItems(GameObject itemsGrid, GameObject itemPrefab)
    // {
    //     List<GameObject> itemsGOs = new List<GameObject>();

    //     foreach (Item item in itemsList)
    //     {
    //         if (item.IsObtained)
    //         {
    //             Sprite itemIcon = item.Icon;
    //             string itemLabel = item.Name;
    //             string itemDesc = item.Desc;
    //             string itemPrice = item.Price + ".0";

    //             GameObject itemGO = Instantiate(itemPrefab);
    //             itemGO.name = itemLabel;

    //             itemGO.transform.GetChild(0).GetComponent<Image>().sprite = itemIcon;
    //             itemGO.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemLabel;
    //             itemGO.transform.SetParent(itemsGrid.transform);

    //             // To display the selected item
    //             itemGO.transform.localScale = new Vector3(1f, 1f, 1f);
    //         }
    //     }
    // }
}