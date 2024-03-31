using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static bool CursorEnabled
    {
        get { return _cursorEnabled; }

        private set
        {
            _cursorEnabled = value;

            if (_cursorEnabled)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    private static bool _cursorEnabled = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CursorEnabled = true;
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        CursorEnabled = !hasFocus;
    }

}
