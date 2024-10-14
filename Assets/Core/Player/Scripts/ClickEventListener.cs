using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class ClickEventListener : MonoBehaviour
{
    UnityEvent clickListener;
    int timesClicked = 0;
    bool doubleClicked = false;
    float doubleClickTimeThreshold = 0.25f;
    float timeSinceLastClick = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (clickListener == null)
        {
            clickListener = new UnityEvent();
        }
        //clickListener.AddListener(doubleClickTracker);    
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetMouseButtonDown(0))
        {
            clickListener.Invoke();
        }
        */
    }

    public bool doubleClickTracker()
    {
        timesClicked++;
        
        //Debug.Log("Times clicked: " + timesClicked + " ,time since last click: " + (Time.time - timeSinceLastClick));

        if(timesClicked >= 2 && Time.time - timeSinceLastClick <= doubleClickTimeThreshold)
        {
            doubleClicked = true;
            timesClicked = 0;
            timeSinceLastClick = Time.time;
            return doubleClicked;
        }
        else
        {
            doubleClicked = false;
            timeSinceLastClick = Time.time;
            return doubleClicked;
        }
    }

    public bool getDoubleClicked()
    {
        return doubleClicked;
    }

    public void invokeListener()
    {
        clickListener.Invoke();
    }
}
