using UnityEngine;
using TMPro;

public class CurrencySystem : MonoBehaviour
{

    public int startingCurrency = 0;
    public TextMeshProUGUI currencyText;

    private int currentCurrency;

    void Start()
    {
        currentCurrency = startingCurrency;
        UpdateCurrencyText();
    }

    public void AddCurrency(int amount)
    {
        currentCurrency += amount;
        UpdateCurrencyText();
    }

    public void SubtractCurrency(int amount)
    {
        currentCurrency -= amount;
        UpdateCurrencyText();
    }

    private void UpdateCurrencyText()
    {
        currencyText.text =  currentCurrency.ToString() + " BHD";
    }
}