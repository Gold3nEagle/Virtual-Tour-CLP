using System;
using System.Collections.Generic;
using UnityEngine;

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
}