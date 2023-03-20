using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class ItemEvents : MonoBehaviour
{
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

        ItemUI.selectedItem = gameObject;
        gameObject.GetComponent<Image>().enabled = true;

        RefreshDescriptionForSelectedItem();
    }

    private void RefreshDescriptionForSelectedItem()
    {
        string path = "Assets/Scriptable Objects/Inventory Items/" + ItemUI.selectedItem.name + ".asset";
        ItemScriptableObj tempItemSO = AssetDatabase.LoadAssetAtPath<ItemScriptableObj>(path);

        TextMeshProUGUI itemDescriptionTF = GameObject.FindGameObjectWithTag("Stock Item Desc").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI itemPriceTF = GameObject.FindGameObjectWithTag("Stock Item Price").GetComponent<TextMeshProUGUI>();

        itemDescriptionTF.text = tempItemSO.description;
        itemPriceTF.text = tempItemSO.price + ".0";
    }
}
