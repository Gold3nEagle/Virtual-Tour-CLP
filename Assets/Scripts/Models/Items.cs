using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to store a list of items of type `Item`
/// </summary>
public class Items
{
    private List<Item> items;
    private Item selectedItem;

    public List<Item> ToList { get => items; }
    public Item SelectedItem { get => selectedItem; }

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

    public void BuyItem(Item itemToBuy)
    {
        int itemToBuyIndex = items.FindIndex(item => item == itemToBuy);
        items.RemoveAt(itemToBuyIndex);
        items.Insert(itemToBuyIndex, itemToBuy);
    }

    public void SetSelectedItem(string itemName)
    {
        selectedItem = items.Find(item => item.Name == itemName);
    }
}