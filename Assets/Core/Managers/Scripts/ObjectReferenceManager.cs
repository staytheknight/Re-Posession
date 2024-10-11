using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReferenceManager : MonoBehaviour
{   
    public GameObject playerObject = null;
    public Transform playerTransform = null;

    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerTransform = playerObject.GetComponent<Transform>();
    }

    public GameObject getPlayerObject()
    {
        return playerObject;
    }

    public Transform getPlayerTransform()
    {
        return playerTransform;
    }
}
