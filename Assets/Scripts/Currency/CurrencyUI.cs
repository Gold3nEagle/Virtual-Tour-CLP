using UnityEngine;
using TMPro;

public class CurrencyUI : MonoBehaviour
{
    public TextMeshProUGUI currencyText;
   
    void Awake()
    {
        CurrencySystem.instance.onMoneyChanged.AddListener(UpdateUI);
        if (CurrencySystem.instance.currentMoney <= 0)
        {
            currencyText.text = $"Money: 0.0";
        }
        else
        {
            currencyText.text = $"Money: {CurrencySystem.instance.currentMoney}.0";
        }
    }
    
    //update money text in the player UI
    void UpdateUI()
    {
        currencyText.text = $"Money: {CurrencySystem.instance.currentMoney}.0";
    }
}
