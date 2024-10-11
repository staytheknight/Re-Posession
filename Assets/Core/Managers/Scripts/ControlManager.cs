using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{   
    float clicked = 0;
    float clickTime = 0;
    float clickDelay = 0.5f;

    public bool DoubleClickDetector()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // Increment how many times clicked
            clicked++;
            // If clicked once
            if (clicked == 1)
            {
                // Capture the time that the first click happened
                clickTime = Time.time;
                return false;
            }

            // If clicked more than once, and the time between clicks is lower than click delay
            if (clicked > 1 && Time.time - clickTime < clickDelay)
            {   
                // Reset click count & click time
                clicked = 0;
                clickTime = 0;
                // Register double click / do action
                return true;
            }

            // If clicked more than twice, or the delay is too long, reset clicks
            else if (clicked > 2 || Time.time - clickTime > clickDelay)
            {
                clicked = 0;
                return false;
            }
        }
        return false;
    }
}
