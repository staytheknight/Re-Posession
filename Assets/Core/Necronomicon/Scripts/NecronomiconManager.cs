using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecronomiconManager : MonoBehaviour
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
        // Get which object the collision listener has stored
        GameObject collidedObj = CL.getAgentCollided();
        // If the object is valid
        if(collidedObj != null)
        {
            // Set the collision object to null
            CL.setAgentCollided(null);
            // If the current target carrying the necronomicon is the player
            if (targetCarrying == player)
            {
                // Switch the target carrying from the player to the agent that collided with the player
                targetCarrying = collidedObj;
            }   
        }
        followTarget(targetCarrying);
        
    }

    public GameObject getTargetCarrying()
    {
        return targetCarrying;
    }

    public void setTargetCarrying(GameObject target)
    {
        targetCarrying = target;
    }
}
