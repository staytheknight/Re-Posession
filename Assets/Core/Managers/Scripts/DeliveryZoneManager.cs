using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryZoneManager : MonoBehaviour
{
    public ObjectReferenceManager orm;

    void OnTriggerEnter(Collider colliderObj)
    {   
        if (orm.getNecronomicon() != null)
        {
            if(colliderObj == orm.getNecronomicon().GetComponent<Collider>())
            {
                Debug.Log("Delivered!");
            }
        }

    }
}
