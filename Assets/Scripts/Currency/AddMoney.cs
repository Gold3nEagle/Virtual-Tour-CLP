using UnityEngine;
using UnityEngine.UI;

public class AddMoney : MonoBehaviour
{

    //add money to the user on collition"testing"
    private void OnCollisionEnter(Collision collision)
    {
        CurrencySystem.instance.AddMoney(10);
    }
}
