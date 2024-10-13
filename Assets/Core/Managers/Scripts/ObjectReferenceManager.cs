using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReferenceManager : MonoBehaviour
{   
    public GameObject playerObject = null;
    public Transform playerTransform = null;
    public GameObject necronomicon = null;

    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerTransform = playerObject.GetComponent<Transform>();
        necronomicon = GameObject.FindGameObjectWithTag("Necronomicon");
    }

    public GameObject getPlayerObject()
    {
        return playerObject;
    }

    public Transform getPlayerTransform()
    {
        return playerTransform;
    }

    public GameObject getNecronomicon()
    {
        return necronomicon;
    }
}
