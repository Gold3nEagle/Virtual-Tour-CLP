using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int money =0 ;
    
    public Vector3 playerPosition;
    public GameData()
    {
       
        money = 0;
        
        playerPosition = new Vector3(0, 2.85f, 0);
        
    }
}
