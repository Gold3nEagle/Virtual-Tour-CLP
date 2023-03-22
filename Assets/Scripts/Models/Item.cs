using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
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

    public Item(ItemScriptableObj itemScriptableObj)
    {
        name = itemScriptableObj.itemName;
        icon = itemScriptableObj.icon;
        desc = itemScriptableObj.description;
        price = itemScriptableObj.price;
        isObtained = false;
    }
}
