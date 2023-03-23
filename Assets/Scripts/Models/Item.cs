using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Item
{
    // Might be useful later in future.
    public enum ItemName
    {
        Apple,
        Avocado,
        BahrainiKebab,
        Banana,
        Basbousa,
        Bread,
        ChaiKarak,
        Dates,
        DigitalCamera,
        EvianWater,
        F1Ticket,
        HalwaShowaiter,
        Harees,
        Luqaitmat,
        OrangeJuice,
        Samboosa,
        VimtoDrink
    }

    private const string itemScriptableObjPath = "Assets/Scriptable Objects/Inventory Items/";

    private string name;
    public string Name { get => name; }
    private Sprite icon;
    public Sprite Icon { get => icon; }
    private string desc;
    public string Desc { get => desc; }
    private int price;
    public int Price { get => price; }
    private bool isObtained;
    public bool IsObtained { get => isObtained; }

    public Item(string gameObjectName)
    {
        string path = itemScriptableObjPath + gameObjectName + ".asset";
        ItemScriptableObj tempItemSO = AssetDatabase.LoadAssetAtPath<ItemScriptableObj>(path);

        this.name = tempItemSO.itemName;
        this.desc = tempItemSO.description;
        this.icon = tempItemSO.icon;
        this.price = tempItemSO.price;
        this.isObtained = false;
    }

    public Item(ItemScriptableObj itemScriptableObj)
    {
        this.name = itemScriptableObj.itemName;
        this.icon = itemScriptableObj.icon;
        this.desc = itemScriptableObj.description;
        this.price = itemScriptableObj.price;
        this.isObtained = false;
    }
}
