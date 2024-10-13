using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public ObjectReferenceManager orm;

    void OnTriggerEnter(Collider colliderObj)
    {
        if (colliderObj == orm.getPlayerObject().GetComponent<CapsuleCollider>())
        {
            Debug.Log("Player collided");
        }
    }
}
