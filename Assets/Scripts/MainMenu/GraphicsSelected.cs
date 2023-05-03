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
            
        }
        
    }
    public void medium(bool quality)
    {
        if (quality)
        {
            QualitySettings.SetQualityLevel(2);
            
        }

    }
    public void high(bool quality)
    {
        if (quality)
        {
            QualitySettings.SetQualityLevel(3);
            
        }

    }
  
}
