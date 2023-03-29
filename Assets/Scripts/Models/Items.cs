using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to store a list of items of type `Item`
/// </summary>
[Serializable]
public class Items
{
    [SerializeField] private List<Item> items;
    private Item selectedItem;

    public List<Item> List { get => items; }
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

    /// <summary>
    /// Before using this function make sure to set an item as selected using the function
    /// <c>`SetSelectedItem()`</c>.
    /// </summary>
    public void ObtainSelectedItem()
    {
        if (selectedItem == null)
        {
            Debug.LogError("No item was selected...! Use `SetSelectedItem(<item_name>)` before calling this function.");
            return;
        }

        if (CurrencySystem.instance.currentMoney >= selectedItem.Price)
        {
            int itemToBuyIndex = items.FindIndex(item => item == selectedItem);
            items.RemoveAt(itemToBuyIndex);
            items.Insert(itemToBuyIndex, selectedItem);
            selectedItem.ToggleIsObtained();
            CurrencySystem.instance.DeductMoney(selectedItem.Price);
        }

        Debug.Log($"Purchased {selectedItem.Name} for {selectedItem.Price}.0 | Current balance: BHD {CurrencySystem.instance.currentMoney}.0");
    }

    /// <summary>
    /// Sets the item in the list that has the same name of the passed name as the currently selected item.
    /// </summary>
    /// <param name="itemName">The exact name of the item</param>
    public void SetSelectedItem(string itemName)
    {
        selectedItem = items.Find(item => item.Name == itemName);
    }
}