using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    public static Item selectedItem;

    [SerializeField] private TextMeshProUGUI itemDescTextField;
    [SerializeField] private TextMeshProUGUI itemPriceTextField;

    private void Awake()
    {
        itemDescTextField.text = "Select an item to view the description";
        itemPriceTextField.text = "0.0";
    }

    public void OnHover()
    {
        gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    public void OnHoverExit()
    {
        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void OnSelected()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Stock Item"))
        {
            go.GetComponent<Image>().enabled = false;
        }

        gameObject.GetComponent<Image>().enabled = true;

        RefreshItemInfo();
        RefreshDescriptionForSelectedItem();
    }

    private void RefreshItemInfo()
    {
        selectedItem = new Item(gameObjectName: gameObject.name);
    }

    private void RefreshDescriptionForSelectedItem()
    {
        itemDescTextField.text = selectedItem.Desc;
        itemPriceTextField.text = selectedItem.Price + ".0";
    }
}
