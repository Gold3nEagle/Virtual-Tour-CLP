using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
   [SerializeField] private Button buyButton;

   public void BuyItem()
   {
        ItemsDisplayer.items.ObtainSelectedItem();
        buyButton.interactable = false;
   }
}
