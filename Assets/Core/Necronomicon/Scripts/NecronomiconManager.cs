using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public ObjectReferenceManager orm;

    private bool followPlayerToggle = false;
    private Vector3 posOffset = new Vector3(0, 0, 1.5f);

    void OnTriggerEnter(Collider colliderObj)
    {
        if (colliderObj == orm.getPlayerObject().GetComponent<CapsuleCollider>())
        {
            followPlayerToggle = true;
        }
    }

    void followPlayer()
    {
        transform.position = posOffset + orm.getPlayerTransform().position;
    }

    void Update()
    {
        if (followPlayerToggle)
        {
            followPlayer();
        }
    }
}
