using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayClickIndicator : MonoBehaviour
{
    public GameObject clickLocObject;
    public Material m1;
    public Material m2;

    // Destroys all instances of click indicator, and then creates a new one
    public void displayClickIndicator(Vector3 point, bool doubleClick)
    {
        GameObject[] clickIndicators = GameObject.FindGameObjectsWithTag("Click Indicator");
        foreach (GameObject obj in clickIndicators)
        {
            Destroy(obj);
        }

        // if double click is passed, change the material to indicate double click
        if(doubleClick)
        {
            clickLocObject.GetComponent<Renderer>().material = m2;
        }
        else
        {
            clickLocObject.GetComponent<Renderer>().material = m1;
        }

        Instantiate(clickLocObject, point, Quaternion.identity);
    }
}
