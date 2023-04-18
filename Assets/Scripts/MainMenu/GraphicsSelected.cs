using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsSelected : MonoBehaviour
{
   
    public void low(bool quality)
    {
        if (quality)
        {
            QualitySettings.SetQualityLevel(0);
            PlayerPrefs.SetInt("currentQ", 0);
        }
        
    }
    public void medium(bool quality)
    {
        if (quality)
        {
            QualitySettings.SetQualityLevel(1);
            PlayerPrefs.SetInt("currentQ", 1);

        }

    }
    public void high(bool quality)
    {
        if (quality)
        {
            QualitySettings.SetQualityLevel(2);
            PlayerPrefs.SetInt("currentQ", 2);

        }

    }
  
}
