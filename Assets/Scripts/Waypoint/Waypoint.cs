using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Waypoint : MonoBehaviour
{
    public string waypointID;

    private Transform targetTransform;
    private Vector3 targetPos;
    private bool moveableTarget;
    private Image img;
    private TextMeshProUGUI meter;
    private Vector3 offset = new Vector3(0, 2.5f, 0);

    void Start()
    {
        img = GetComponent<Image>();
        meter = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = img.GetPixelAdjustedRect().height;// / 1.5f;
        float maxY = Screen.height - (minY / 1.5f);
        Vector3 followPos;
        if(moveableTarget)
        {
            followPos = targetTransform.position;
        }
        else
        {
            followPos = targetPos;
        }

        Vector2 pos = Camera.main.WorldToScreenPoint(followPos + offset);

        if (Vector3.Dot(followPos - Camera.main.gameObject.transform.position, Camera.main.gameObject.transform.forward) < 0)
        {
            if (pos.x < Screen.width)
            {
                pos.x = maxX;
            }
            else
            {
                pos.x = minX;
            }
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos;
        meter.text = ((int)Vector3.Distance(followPos, Camera.main.gameObject.transform.position)).ToString() + "m";
    }

    public void SetValues(string id, GameObject gameObjectToFollow)
    {
        waypointID = id;
        targetTransform = gameObjectToFollow.transform;
        moveableTarget = true;
    }

    public void SetValues(string id, Vector3 position)
    {
        waypointID = id;
        targetPos = position;
        moveableTarget = false;
    }

    private void OnDestroy()
    {
        WaypointManager.instance.waypointsList.Remove(gameObject);
    }
}
