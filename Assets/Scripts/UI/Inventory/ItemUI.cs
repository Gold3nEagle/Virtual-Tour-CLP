using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemDescTextField;
    [SerializeField] private TextMeshProUGUI itemPriceTextField;
    private const string itemScriptableObjPath = "Assets/Scriptable Objects/Inventory Items/";

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

        ItemsDisplayer.selectedItem = gameObject;
        gameObject.GetComponent<Image>().enabled = true;

        RefreshDescriptionForSelectedItem();
    }

    private void RefreshDescriptionForSelectedItem()
    {
        string path = itemScriptableObjPath + ItemsDisplayer.selectedItem.name + ".asset";
        ItemScriptableObj tempItemSO = AssetDatabase.LoadAssetAtPath<ItemScriptableObj>(path);

        itemDescTextField.text = tempItemSO.description;
        itemPriceTextField.text = tempItemSO.price + ".0";
    }
}
