using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroZoneManager : MonoBehaviour
{
   void OnTriggerEnter(Collider colliderObj)
    {   
        GameObject necronomicon = GameObject.FindGameObjectWithTag("Necronomicon");
        Collider necroCollider = necronomicon.GetComponent<Collider>();
        if (necroCollider != null)
        {
            if(colliderObj == necroCollider)
            {
                necronomicon.GetComponent<NecronomiconManager>().setTargetCarrying(null);
            }
        }

    }
}
