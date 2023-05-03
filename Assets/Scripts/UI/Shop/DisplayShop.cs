using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayShop : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("im here in the shop " + other.name);
        GameManager.instance.isNearShop = true;
        GameManager.instance.ShowPrompt("Press F to open the shop");
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("im not in the shop " + other.name);
        GameManager.instance.isNearShop = false;
        GameManager.instance.HidePrompt();
    }
}
