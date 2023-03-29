using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Class to store info related to a SINGLE item.
/// <br />
/// Such as:
/// <list type="number">
///     <item>
///         <term>Name</term>
///         <description>Item's name</description>
///     </item>
///     <item>
///         <term>Description</term>
///         <description>Item's description</description>
///     </item>
///     <item>
///         <term>Price</term>
///         <description>Item's price</description>
///     </item>
/// </list>
/// </summary>
[Serializable]
public class Item
{
    private const string itemScriptableObjPath = "Assets/Scriptable Objects/Inventory Items/";

    [SerializeField] private string name;
    [SerializeField] private bool isObtained;
    [NonSerialized] private Sprite icon;
    private string desc;
    private int price;
    private bool isQuestItem;

    public string Name { get => name; }
    public Sprite Icon { get => icon; }
    public string Desc { get => desc; }
    public int Price { get => price; }
    public bool IsObtained { get => isObtained; }
    public bool IsQuestItem { get => isQuestItem; }

    /// <summary>
    /// Creates a new item by passing the game object's name.
    /// </summary>
    /// <param name="itemName">The item's name, usually the same as the file name</param>
    public Item(string itemName)
    {
        string path = itemScriptableObjPath + itemName + ".asset";
        ItemScriptableObj tempItemSO = AssetDatabase.LoadAssetAtPath<ItemScriptableObj>(path);

        this.name = tempItemSO.itemName;
        this.desc = tempItemSO.description;
        this.icon = tempItemSO.icon;
        this.price = tempItemSO.price;
        this.isQuestItem = tempItemSO.isQuestItem;
        this.isObtained = false;
    }

    /// <summary>
    /// Creates a new item by passing the item scriptable object.
    /// </summary>
    /// <param name="itemScriptableObj">The item scriptable object</param>
    public Item(ItemScriptableObj itemScriptableObj)
    {
        this.name = itemScriptableObj.itemName;
        this.icon = itemScriptableObj.icon;
        this.desc = itemScriptableObj.description;
        this.price = itemScriptableObj.price;
        this.isQuestItem = itemScriptableObj.isQuestItem;
        this.isObtained = false;
    }

    /// <summary>
    /// Toggles the `IsObtained` property between true or false.
    /// </summary>
    public void ToggleIsObtained()
    {
        this.isObtained = !this.isObtained;
    }

    public void PopulateItemInfo()
    {
        string path = itemScriptableObjPath + name + ".asset";
        ItemScriptableObj tempItemSO = AssetDatabase.LoadAssetAtPath<ItemScriptableObj>(path);

        this.desc = tempItemSO.description;
        this.icon = tempItemSO.icon;
        this.price = tempItemSO.price;
        this.isQuestItem = tempItemSO.isQuestItem;
    }
}