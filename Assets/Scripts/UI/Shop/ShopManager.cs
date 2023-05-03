
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private Button buyButton;

    public void BuyItem()
    {
        ItemsManager.items.ObtainSelectedItem();
        buyButton.interactable = false;
    }

    public bool CheckBuyItem(string itemName)
    {
        foreach (Item item in ItemsManager.items.List)
        {
            if (item.Name == itemName && item.IsObtained)
            {
               
                return true;
            }

        }
        return false;
    }

    //private void OnEnable()
    //{
    //    Lua.RegisterFunction("CheckBuyItem", this, SymbolExtensions.GetMethodInfo(() => CheckBuyItem(string.Empty)));
    //}

    //private void OnDisable()
    //{
    //    Lua.UnregisterFunction("CheckBuyItem");
    //}
}
