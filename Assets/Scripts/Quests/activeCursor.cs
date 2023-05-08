using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeCursor : MonoBehaviour
{
    public void EnableCursor()
    {
        // Debug.Log("Show Cursor");
        CursorManager.instance.SetCursorVisibility(true);
    }
    public void DisableCursor()
    {
        // Debug.Log("Hide Cursor");
        CursorManager.instance.SetCursorVisibility(false);
    }
}
