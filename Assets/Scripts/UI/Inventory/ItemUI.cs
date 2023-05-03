using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemDescTextField;
    [SerializeField] private TextMeshProUGUI itemPriceTextField;
    [SerializeField] private Button buyButton;

    private void Awake()
    {
        itemDescTextField.text = "Select an item to view the description";
        itemPriceTextField.text = "0.0";
    }

    public void OnHover() { gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f); }

    public void OnHoverExit() { gameObject.transform.localScale = new Vector3(1f, 1f, 1f); }

    public void OnSelected()
    {
        AudioManager.instance.Play("Click");
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Stock Item"))
        {
            go.GetComponent<Image>().enabled = false;
        }

        gameObject.GetComponent<Image>().enabled = true;

        RefreshItemInfo();
    }

    /// <summary>
    /// Refreshes the currently selected item information such as: Price and Description.
    /// The selected item will be stored to a `public static variable` as `Item` type
    /// that can be accessible from anywhere.
    /// </summary>
    private void RefreshItemInfo()
    {
        ItemsManager.items.SetSelectedItem(gameObject.name);

        // Debug.Log($"{ItemsManager.items.SelectedItem.IsObtained} (BHD {ItemsManager.items.SelectedItem.Price}.0) - obtained: {ItemsManager.items.SelectedItem.IsObtained} | Current balance: BHD {CurrencySystem.instance.currentMoney}.0");

        itemDescTextField.text = ItemsManager.items.SelectedItem.Desc;
        itemPriceTextField.text = ItemsManager.items.SelectedItem.Price + ".0";

        if (ItemsManager.items.SelectedItem.IsObtained || CurrencySystem.instance.currentMoney < ItemsManager.items.SelectedItem.Price)
        {
            buyButton.interactable = false;
        }
        else
        {
            buyButton.interactable = true;
        }
    }
}
