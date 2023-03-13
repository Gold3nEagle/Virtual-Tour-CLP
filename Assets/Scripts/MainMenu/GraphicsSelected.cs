using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsSelected : MonoBehaviour
{
    public void low(bool quality)
    {
        if (quality)
        {
        QualitySettings.SetQualityLevel(1);
            Debug.Log("Low is activated");
        }
        
    }
    public void medium(bool quality)
    {
        if (quality)
        {
            QualitySettings.SetQualityLevel(2);
            Debug.Log("Medium is activated");
        }

    }
    public void high(bool quality)
    {
        if (quality)
        {
            QualitySettings.SetQualityLevel(3);
            Debug.Log("High is activated");
        }

    }
  
}
