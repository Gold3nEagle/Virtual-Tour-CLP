using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCapture : MonoBehaviour
{
    public GameObject questCam;
    public GameObject uiPanel;
    public GameObject camPanel;
    public GameObject targetObject; // Add a reference to the target GameObject
    public float rotationSpeed = 1.0f; // Add a variable for controlling the speed of rotation

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (questCam.activeSelf) // Only rotate the camera if it's active
        {
            RotateCameraToTarget();
        }
    }

    public void TakePicture()
    {
        GameManager.instance.cameraOBJ.SetActive(false);
        questCam.SetActive(true);
        Switch();
    }
    public void Switch()
    {
        // Disable panel1 and enable panel2
        uiPanel.SetActive(false);
        camPanel.SetActive(true);

        // Wait for 3 seconds before reversing the process
        StartCoroutine(WaitAndSwitch());
    }

    IEnumerator WaitAndSwitch()
    {
        yield return new WaitForSeconds(3);

        // Disable panel2 and enable panel1
        camPanel.SetActive(false);
        uiPanel.SetActive(true);
        questCam.SetActive(false);
        GameManager.instance.cameraOBJ.SetActive(true);
        
    }

    // Modify the RotateCameraToTarget method to smoothly rotate the camera
    void RotateCameraToTarget()
    {
        if (questCam != null && targetObject != null)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetObject.transform.position - questCam.transform.position);
            questCam.transform.rotation = Quaternion.Slerp(questCam.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}