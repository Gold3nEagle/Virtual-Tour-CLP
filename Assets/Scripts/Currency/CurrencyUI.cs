using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CurrencyUI : MonoBehaviour
{
    public TextMeshProUGUI currencyText;

    void Awake()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            Debug.Log("Player's money won't be restored, please run from 'Main Menu' scene...");
            this.enabled = false;
            return;
        }

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
