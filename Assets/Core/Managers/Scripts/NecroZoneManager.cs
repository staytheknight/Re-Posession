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
                NecronomiconManager nm = necronomicon.GetComponent<NecronomiconManager>();
                if(nm.getTargetCarrying() != GameObject.FindGameObjectWithTag("Player"))
                {
                    necronomicon.GetComponent<NecronomiconManager>().setTargetCarrying(null);
                }                
            }
        }

    }
}
