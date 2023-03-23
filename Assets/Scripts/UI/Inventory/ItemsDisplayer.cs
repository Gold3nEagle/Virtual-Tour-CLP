using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemsDisplayer : MonoBehaviour
{
    private Items items;

    [SerializeField] private bool isInventoryMenu;
    [SerializeField] private TextMeshProUGUI menuTitleTextField;
    [SerializeField] private Button buyButton;
    [SerializeField] private GameObject itemsGrid;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private List<ItemScriptableObj> itemsScriptableObjects;

    private void Awake()
    {
        items = new Items();

        // Gets the TMPro child game object for the button.
        TextMeshProUGUI buyButtonTextField = buyButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

        // Set the menu title
        menuTitleTextField.text = isInventoryMenu ? "Inventory" : "Shop";

        // Set the button text depending on the player's item status
        // TODO: change the "open shop" text to either "available" or "owned"
        buyButtonTextField.text = isInventoryMenu ? "Open Shop" : "Buy Now";

        PopulateItems();
        DisplayItems();

        itemPrefab.SetActive(false);
    }

    private void PopulateItems()
    {
        for (int i = 0; i < itemsScriptableObjects.Count; i++)
        {
            Item item = new Item(itemsScriptableObjects[i]);
            items.AddItem(item);
        }
    }

    private void DisplayItems()
    {
        foreach (Item item in items.List)
        {
            if (isInventoryMenu)
            {
                if (!item.IsObtained) break;
            }

            Sprite itemIcon = item.Icon;
            string itemLabel = item.Name;
            string itemDesc = item.Desc;
            string itemPrice = item.Price + ".0";

            GameObject itemGO = Instantiate(itemPrefab);

            itemGO.name = itemLabel;
            itemGO.transform.GetChild(0).GetComponent<Image>().sprite = itemIcon;
            itemGO.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemLabel;
            itemGO.transform.SetParent(itemsGrid.transform);

            // To display the selected item
            itemGO.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
