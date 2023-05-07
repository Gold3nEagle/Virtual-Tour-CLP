using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayShop : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.instance.isNearShop = true;
            GameManager.instance.ShowPrompt("Press F to open the shop");
        } 
    }
    private void OnTriggerExit(Collider other)
    {
        
        GameManager.instance.isNearShop = false;
        GameManager.instance.HidePrompt();
    }
}
