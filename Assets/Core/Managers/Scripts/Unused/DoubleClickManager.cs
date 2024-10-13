using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Modified from
// https://gamedev.stackexchange.com/questions/116455/how-to-properly-differentiate-single-clicks-and-double-click-in-unity3d-using-c/
public class DoubleClickManager : MonoBehaviour
{
    public float doubleClickTimeLimit = 0.25f;
    private bool doubleClicked = false;

    protected void Start()
    {
        StartCoroutine(InputListener());
    }

    private IEnumerator InputListener()
    {
        while(enabled)
        {
            if(Input.GetMouseButtonDown(0))
            {
                yield return ClickEvent();
            }
            yield return null;
        }
    }

    private IEnumerator ClickEvent()
    {
        //Pause frame to only capture single mouse event
        yield return new WaitForEndOfFrame();

        float count = 0f;
        while(count < doubleClickTimeLimit)
        {
            if (Input.GetMouseButtonDown(0))
            {
                doubleClicked = true;
                yield break;
            }
            count += Time.deltaTime; // increment counter by change in time between frames
            yield return null; // wait for the next frame
        }
        doubleClicked = false;
    }

    public bool getDoubleClicked()
    {
        return doubleClicked;
    }
}
