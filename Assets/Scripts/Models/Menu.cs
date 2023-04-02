using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu
{
    private GameObject canvasGameObject;
    private GameObject menuGameObject;
    private bool isMenuOpen;

    public string Title;
    public string CanvasTitle;

    public GameObject CanvasGameObject { get => canvasGameObject; }
    public GameObject MenuGameObject { get => menuGameObject; }
    public bool IsMenuOpen { get => isMenuOpen; set { isMenuOpen = value; } }

    public Menu(string menuTitle, string canvasTitle)
    {
        Title = menuTitle;
        CanvasTitle = canvasTitle;
        canvasGameObject = GameObject.Find(canvasTitle);
        menuGameObject = canvasGameObject.transform.Find(menuTitle).gameObject;
        isMenuOpen = false;
    }
}
