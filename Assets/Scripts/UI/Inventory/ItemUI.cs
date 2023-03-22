using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    public static GameObject selectedItem;

    // For Text Mesh Pro fields (basically text fields)
    [SerializeField] private bool isInventoryMenu;
    [SerializeField] private TextMeshProUGUI menuTitleTF;
    [SerializeField] private Button buyButton;
    [SerializeField] private GameObject itemsGrid;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private List<ItemScriptableObj> itemsScriptableObjects;

    private TextMeshProUGUI buyButtonTF;
    private TextMeshProUGUI itemDescriptionTF;
    private TextMeshProUGUI itemPriceTF;
    private List<GameObject> itemsGOs;
    private Items items;

    private void Awake()
    {
        items = new Items();

        // Gets the text mesh pro child game object for the button.
        buyButtonTF = buyButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

        itemDescriptionTF = GameObject.FindGameObjectWithTag("Stock Item Desc").GetComponent<TextMeshProUGUI>();
        itemPriceTF = GameObject.FindGameObjectWithTag("Stock Item Price").GetComponent<TextMeshProUGUI>();

        itemsGOs = new List<GameObject>();

        // Set the menu title
        menuTitleTF.text = isInventoryMenu ? "Inventory" : "Shop";

        // Set the button text depending on the player's item status
        // TODO: change the "open shop" text to either "available" or "owned"
        buyButtonTF.text = isInventoryMenu ? "Open Shop" : "Buy Now";

        // Clear description and price
        itemDescriptionTF.text = "Select an item to view the description";
        itemPriceTF.text = "0.0";

        // Disable button
        // buyButton.enabled = false;

        PopulateItems();

        if (isInventoryMenu)
        {
            DisplayInventory();
        }
        else
        {
            DisplayShop();
        }

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

    private void DisplayInventory()
    {
        foreach (Item item in items.List)
        {
            if (item.IsObtained)
            {
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

    private void DisplayShop()
    {
        foreach (Item item in items.List)
        {
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
