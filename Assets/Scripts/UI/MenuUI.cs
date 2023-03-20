using UnityEngine;

public class MenuUI
{
    public MenuUI(string menuTitle, string canvasTitle = "MenuCanvas")
    {
        this.menuTitle = menuTitle;
        this.canvasTitle = canvasTitle;

        canvasGO = GameObject.Find(canvasTitle);

        switch (menuTitle)
        {
            case "inv":
                childTransform = canvasGO.transform.Find("Inventory Menu");
                break;
            case "shop":
                childTransform = canvasGO.transform.Find("Shop Menu");
                break;
            case "pause":
                childTransform = canvasGO.transform.Find("Pause Menu");
                break;
            default:
                Debug.LogError($@"The entered menu title [{menuTitle}] doesn't exist...
                Please pass either of the following parameters to activate the GameObjects within the canvas:
                1. `inv` - To activate 'Inventory Menu' GameObject.
                2. `shop` - To activate 'Shop Menu' GameObject.
                3. `pause` - To activate 'Pause Menu' GameObject.");
                return;
        }

        menuGO = childTransform.gameObject;
    }

    private string menuTitle;
    private string canvasTitle;
    private Transform childTransform;
    private GameObject canvasGO;
    private GameObject menuGO;
    private bool isMenuOpen = false;

    public string MenuTitle { get; set; }
    public bool IsMenuOpen { get; }

    public void OpenMenu()
    {
        if (isMenuOpen)
        {
            Debug.Log($"The menu [{menuTitle}] is already opened, will close now...");
            CloseMenu();
            return;
        }

        // FindMenu();

        Time.timeScale = 0.0f;
        menuGO.SetActive(true);
        isMenuOpen = true;
    }

    public void CloseMenu()
    {
        if (!isMenuOpen)
        {
            Debug.Log($"The menu [{menuTitle}] is not yet opened...");
            return;
        }

        Time.timeScale = 1.0f;
        menuGO.SetActive(false);
        isMenuOpen = false;
    }

    // public static void CloseAllMenus(string canvasTitle = "MenuCanvas")
    // {
    //     Time.timeScale = 1.0f;
    //     GameObject canvasGO = GameObject.Find(canvasTitle);
    //     for (int i = 0; i < canvasGO.transform.childCount; i++)
    //     {
    //         Transform childTransform = canvasGO.transform.GetChild(i);
    //         GameObject menuObject = childTransform.gameObject;
    //         menuObject.SetActive(false);
    //     }
    // }
}
