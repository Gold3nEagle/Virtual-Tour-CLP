
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItemsAndRewards : MonoBehaviour
{
    
    public void giveMoney()
    {
        CurrencySystem.instance.AddMoney(10);
    }

    public void reward(int rewardValue)
    {
        CurrencySystem.instance.AddMoney(rewardValue);
    }

    public void BuyItem(int moneyValue)
    {
        CurrencySystem.instance.DeductMoney(moneyValue);
    }

    public void giveItem(string itemName)
    {
       
    }

    
}
