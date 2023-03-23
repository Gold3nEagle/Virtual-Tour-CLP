using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to store a list of items of type `Item`
/// </summary>
public class Items
{
    private List<Item> items;
    public List<Item> ToList { get => items; }

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
}