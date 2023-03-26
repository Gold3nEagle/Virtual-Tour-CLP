using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemsDisplayer : MonoBehaviour
{
    public static Items items;

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
    }

    private void OnEnable()
    {
        DisplayItems();
    }

    private void OnDisable()
    {
        // Destroy grid items game objects
        for (int i = itemsGrid.transform.childCount - 1; i >= 1; i--)
        {
            Destroy(itemsGrid.transform.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// Populates the items list with items
    /// </summary>
    private void PopulateItems()
    {
        items.ToList.Clear();

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
        itemPrefab.SetActive(true); // To be able to instantiate game objects.

        foreach (Item item in items.ToList)
        {
            //Debug.Log($"item found: {item.Name}\n" +
            //    $"desc: {item.Desc}\n" +
            //    $"price: {item.Price}\n" +
            //    $"isObtained: {item.IsObtained}\n");

            if (isInventoryMenu && !item.IsObtained) continue;

            Sprite itemIcon = item.Icon;
            string itemLabel = item.Name;

            GameObject itemGO = Instantiate(itemPrefab);

            itemGO.name = itemLabel;
            itemGO.transform.GetChild(0).GetComponent<Image>().sprite = itemIcon;
            itemGO.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemLabel;

            itemGO.transform.SetParent(itemsGrid.transform);
            itemGO.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        itemPrefab.SetActive(false); // To hide the prefab game object.
    }
}
