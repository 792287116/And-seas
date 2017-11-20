using UnityEngine;
using System.Collections;

public delegate void OnTapKey(KeyCode kc);

public class InputKeyEvent : MonoBehaviour
{

    public OnTapKey onTapKey;

    // Use this for initialization  
    void Start()
    {
        onTapKey += OnGetKeyDown;
    }
    void OnGUI()
    {
        if (Input.anyKeyDown)
        {
            Event e = Event.current;
            if (e != null && e.isKey && e.keyCode != KeyCode.None)
            {
                //              KeyCode currentKey = e.keyCode;  
                //              Debug.Log("Current Key is : " + currentKey.ToString());  
                if (onTapKey != null)
                    onTapKey(e.keyCode);
            }
        }
    }

    void OnGetKeyDown(KeyCode kc)
    {
        Debug.Log("Current Key is : " + kc);
    }
}