using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInRangeDetector : MonoBehaviour
{
    private BoxCollider thisColl;

    private void Start() { thisColl = GetComponent<BoxCollider>(); }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.isPlayerWithinCarRange = true;
            GameManager.instance.ShowPrompt("Press E to enter the car");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.isPlayerWithinCarRange = false;
            GameManager.instance.HidePrompt();
        }
    }
}
