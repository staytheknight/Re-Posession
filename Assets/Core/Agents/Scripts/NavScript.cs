using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavScript : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public ObjectReferenceManager orm;
    public DisplayClickIndicator clickIndicator;                    // For debug visuals

    public GameObject vc;
    public Vision visionScript;

    Transform playerTransform;

    float movementRotationRange = 25.0f;
    Transform parentTransform;
    public Vector3 targetDestination;
    private float targetRadius = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();        // Nav mesh agent
        parentTransform = agent.GetComponentInParent<Transform>();  // Transform of parent that this script is attached to
        targetDestination = parentTransform.position;               // initializes the targetDestination (this should start the agent moving)
        playerTransform = orm.getPlayerTransform();                 // Player object reference
        visionScript = vc.GetComponentInChildren<Vision>();         // Vision script attached to this object (inside of VisionCone)
    }

    // Update is called once per frame
    void Update()
    {
        
        if(visionScript.getCanSeeTarget())
        {
            // Follow the player
            agent.destination = playerTransform.position;
            targetDestination = playerTransform.position;
        }
        else
        {
            autonomousMove();
        }
    }

    void autonomousMove()
    {        
        // If the agent is close enough to its target destination, find a new target
        if (Mathf.Abs(parentTransform.position.x - targetDestination.x) <= targetRadius &&
            Mathf.Abs(parentTransform.position.y - targetDestination.y) <= targetRadius)
        {
            // Rotate the transform towards a direction in the given range
            parentTransform.Rotate(0,Random.Range(movementRotationRange, -movementRotationRange),0);
            // Casts a ray towards that direction with infinite length
            Physics.Raycast(parentTransform.position, parentTransform.forward, out var hitInfo);
            // Move towards point hit by RayCast
            agent.destination = hitInfo.point;
            targetDestination = hitInfo.point;
            //clickIndicator.displayClickIndicator(hitInfo.point, true);   // Debug visual
        }
    }
}
