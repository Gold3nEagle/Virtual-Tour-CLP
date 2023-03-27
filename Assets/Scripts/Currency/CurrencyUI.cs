using UnityEngine;
using TMPro;

public class CurrencyUI : MonoBehaviour
{
    public TextMeshProUGUI currencyText;
    private bool updated;
    void Awake()
    {
        CurrencySystem.instance.onMoneyChanged.AddListener(UpdateUI);
    }
    void Update()
    {
        //if(!updated)
        //{
        //    CurrencySystem.instance.AddMoney(100);
        //    updated = true;
        //}

    }
    //update money text in the player UI
    void UpdateUI()
    {
        currencyText.text = CurrencySystem.instance.CurrentMoney + " BHD";
    }
}
