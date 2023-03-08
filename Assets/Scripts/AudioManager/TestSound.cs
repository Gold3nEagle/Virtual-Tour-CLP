using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;
public class TestSound : MonoBehaviour
{
    public GameObject soundObj;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            AudioManager.instance.Play("Test");
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            AudioManager.instance.Play("Attack");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            AudioManager.instance.Stop("Attack");
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            AudioManager.instance.Play("Test", new Vector3(2, 2, 2));
        }
        else if(Input.GetKeyDown(KeyCode.G))
        {
            AudioManager.instance.Play("Attack", followObject: gameObject);
        }
        else if(Input.GetKeyDown(KeyCode.H))
        {
            soundObj = AudioManager.instance.Play("Failed");
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            soundObj = AudioManager.instance.Play("Win", followObject: gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            Destroy(gameObject);
        }
    }
}
