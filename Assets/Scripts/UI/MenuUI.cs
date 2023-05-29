using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI
{
    private List<Menu> menusList;

    /// <summary>
    /// To control various menus functionalities
    /// </summary>
    public MenuUI()
    {
        menusList = new List<Menu>();

        menusList.Add(new Menu("Inventory Menu", "MenuCanvas"));
        menusList.Add(new Menu("Shop Menu", "MenuCanvas"));
        menusList.Add(new Menu("Pause Menu", "MenuCanvas"));

        CloseAllMenus();
    }
    
    /// <summary>
    /// Toggles between the available menus using an index (list below):
    ///     <list type="number">
    ///         <item>
    ///             <term>Index #0</term>
    ///             <description>Inventory menu</description>
    ///         </item>
    ///         <item>
    ///             <term>Index #1</term>
    ///             <description>Shop menu</description>
    ///         </item>
    ///         <item>
    ///             <term>Index #2</term>
    ///             <description>Pause menu</description>
    ///         </item>
    ///     </list>
    /// </summary>
    public void ToggleMenuVisibility(int menuIndex)
    {
        if (menusList[menuIndex].IsMenuOpen)
        {
            CloseMenu(menuIndex);
            // Debug.Log($"{menusList[menuIndex].Title} has been closed...");
        }
        else
        {
            CloseAllMenus();
            OpenMenu(menuIndex);
            // Debug.Log($"{menusList[menuIndex].Title} has been opened...");
        }
    }

    private void OpenMenu(int index)
    {
        if (SceneLoader.instance.IsTransitioning()) return;
        Time.timeScale = 0.0f;
        CursorManager.instance.SetCursorVisibility(true);
        menusList[index].MenuGameObject.SetActive(true);
        menusList[index].IsMenuOpen = true;
        if (GameManager.instance.isInVehicle)
        {
            AudioManager.instance.Stop("CarDriving");
        }
    }

    private void CloseMenu(int index)
    {
        if (SceneLoader.instance.IsTransitioning()) return;
        Time.timeScale = 1.0f;
        CursorManager.instance.SetCursorVisibility(false);
        menusList[index].MenuGameObject.SetActive(false);
        menusList[index].IsMenuOpen = false;
        if (GameManager.instance.isInVehicle)
        {
            AudioManager.instance.Play("CarDriving");
        }
    }

    public void CloseAllMenus()
    {
        Time.timeScale = 1.0f;
        CursorManager.instance.SetCursorVisibility(false);

        foreach (Menu menu in menusList)
        {
            menu.MenuGameObject.SetActive(false);
            menu.IsMenuOpen = false;
            // Debug.Log($"{menu.Title} has been closed...");
        }
    }

    public void ResumeGame()
    {
        CloseMenu(2); // Close pause menu
    }
}
