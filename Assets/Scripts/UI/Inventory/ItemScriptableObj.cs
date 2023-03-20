using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory or shop item", menuName = "Scriptable Objects/Inventory or shop item")]
public class ItemScriptableObj : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite icon;
    public int price;
}
