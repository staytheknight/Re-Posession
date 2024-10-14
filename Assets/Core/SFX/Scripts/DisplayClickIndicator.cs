using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayClickIndicator : MonoBehaviour
{
    public GameObject clickLocObject;

    public void displayClickIndicator(Vector3 point, bool doubleClick)
    {
        destroyClickIndicators();

        switchColour(doubleClick);
        Instantiate(clickLocObject, point, Quaternion.identity);
    }

    void destroyClickIndicators()
    {
        GameObject[] clickIndicators = GameObject.FindGameObjectsWithTag("Click Indicator");
        foreach (GameObject obj in clickIndicators)
        {
            Destroy(obj);
        }
    }

    void switchColour(bool doubleClicked)
    {
        if(doubleClicked)
        {
            clickLocObject.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
        }
        else
        {
            clickLocObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
    }
}
