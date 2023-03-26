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
    [SerializeField] private GameObject itemsGrid;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private List<ItemScriptableObj> itemsScriptableObjects;

    private void Awake()
    {
        items = new Items();

        // Set the menu title
        menuTitleTextField.text = isInventoryMenu ? "Inventory" : "Shop";

        PopulateItems();
        DisplayItems();

        itemPrefab.SetActive(false);
    }

    /// <summary>
    /// Populates the items list with items
    /// </summary>
    private void PopulateItems()
    {
        for (int i = 0; i < itemsScriptableObjects.Count; i++)
        {
            Item item = new Item(itemsScriptableObjects[i]);
            items.AddItem(item);
        }
    }

    /// <summary>
    /// Creates new item gameObjects and place them inside the item grid display gameObject.
    /// </summary>
    private void DisplayItems()
    {
        foreach (Item item in items.ToList)
        {
            if (isInventoryMenu)
            {
                if (!item.IsObtained) continue;
            }

            Sprite itemIcon = item.Icon;
            string itemLabel = item.Name;
            string itemDesc = item.Desc;
            string itemPrice = item.Price + ".0";

            GameObject itemGO = Instantiate(itemPrefab);

            itemGO.name = itemLabel;
            itemGO.transform.GetChild(0).GetComponent<Image>().sprite = itemIcon;
            itemGO.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemLabel;
            itemGO.transform.localScale = new Vector3(1f, 1f, 1f);

            itemGO.transform.SetParent(itemsGrid.transform);
        }
    }
}
