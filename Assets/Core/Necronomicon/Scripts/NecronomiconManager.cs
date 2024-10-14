using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public ObjectReferenceManager orm;
    GameObject player;
    CollisionListener CL;
    GameObject targetCarrying;

    private Vector3 posOffset = new Vector3(0, 0, 1.5f);

    void Start()
    {   
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        CL = player.GetComponent<CollisionListener>();
    }

    void OnTriggerEnter(Collider colliderObj)
    {   
        // If the player collides with the necronomicon, set target carrying to player
        if (colliderObj == orm.getPlayerObject().GetComponent<CapsuleCollider>())
        {
            targetCarrying = player;
        }
    }

    // follows target carrying
    void followTarget(GameObject target)
    {
        if (target != null)
        {
            transform.position = posOffset + target.transform.position;
        }        
    }

    void Update()
    {   
        GameObject collidedObj = CL.getAgentCollided();
        if(collidedObj != null)
        {
            CL.setAgentCollided(null);
            targetCarrying = collidedObj;
        }
        followTarget(targetCarrying);
        
    }
}
