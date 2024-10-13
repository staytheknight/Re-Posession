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

    Transform playerTransform;                                      // Transform of the player obj

    Transform parentTransform;                                      // Transform of the obj this script is attached to
    public Vector3 targetDestination;                               // Target destionation for navMesh
    private float targetRadius = 2.0f;                              // Radius around target position the nav mesh agent will considered arrived
    float movementRotationRange = 5.0f;                             // Degree in radians the nav mesh agent will rotate it's raycast

    // Time management
    float[] timeToSwitchTurnRange = {5.0f, 7.0f};                   // Range in seconds for the timeToSwitchTurn variable (will be randomized in range)
    float timeToSwitchTurn = 5.0f;                                  // How much time in seconds before nav mesh agent will switch turning (left or right)
    float timeSinceLastFrame = 0.0f;                                // Time in seconds since last frame
    float timeAccumulator = 0.0f;                                   // Frame time accumulator

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

            // Accumulates times between frames, changes direction once accumulator reaches certain threshold
            timeAccumulator = Time.time + timeSinceLastFrame;
            if(timeAccumulator > timeToSwitchTurn)
            {
                movementRotationRange *= -1.0f;
                timeAccumulator = 0.0f;
                // Gets a random value between the ranges of given seconds - this determines how long before the agent switches it's turn direction
                timeToSwitchTurn = Random.Range(timeToSwitchTurnRange[0], timeToSwitchTurnRange[1]);
            }
            timeSinceLastFrame = Time.time;

            // Rotate the transform towards a direction in the given range
            parentTransform.Rotate(0,Random.Range(0, movementRotationRange),0);
            // Casts a ray towards that direction with infinite length
            Physics.Raycast(parentTransform.position, parentTransform.forward, out var hitInfo);
            // Move towards point hit by RayCast
            agent.destination = hitInfo.point;
            targetDestination = hitInfo.point;
            //clickIndicator.displayClickIndicator(hitInfo.point, true);   // Debug visual

            
        }
    }
}
