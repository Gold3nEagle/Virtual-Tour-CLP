using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionWaypoint : MonoBehaviour
{
    public Image img;
    public Transform target;
    public TextMeshProUGUI meter;
    public Vector3 offset;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = img.GetPixelAdjustedRect().height;// / 1.5f;
        float maxY = Screen.height - (minY / 1.5f);

        Vector2 pos = Camera.main.WorldToScreenPoint(target.position + offset);
        //Vector2 pos = GameManager.instance.GetActiveCamera().WorldToScreenPoint(target.position + offset);
        
        if (Vector3.Dot(target.position - Camera.main.gameObject.transform.position,Camera.main.gameObject.transform.forward) < 0)
        {
            if(pos.x < Screen.width)
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
        meter.text = ((int)Vector3.Distance(target.position, Camera.main.gameObject.transform.position)).ToString() + "m";
    }
}
