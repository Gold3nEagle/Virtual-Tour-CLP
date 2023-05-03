using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeCursor : MonoBehaviour
{
    public void EnableCursor()
    {
        SetCursorVisibility(true);
    }
    public void DisableCursor()
    {
        SetCursorVisibility(false);
    }

    private void SetCursorVisibility(bool visible)
    {
        if (visible)
        {
            // Unhide and unlock the cursor
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            // Hide and lock the cursor
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
