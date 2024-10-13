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
    private float targetRadius = 10.0f;                             // Radius around target position the nav mesh agent will considered arrived
    float movementRotationRange = 5.0f;                             // Degree in radians the nav mesh agent will rotate it's raycast
    bool isStuck = false;                                           // Boolean if the agent is 'stuck' (in the same spot for too long)
    public Vector3 lastCheckedPos;                                       // The last position the agent was recorded at
    float stuckTimeCheck = 3.0f;                                    // Time in seconds for how often to check if the agent is stuck
    float stuckTimeAccumulator = 0.0f;
    float stuckDistanceThreshold = 3.0f;                            // Distance threshold for considering stuck                    

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
        lastCheckedPos = parentTransform.position;
        targetDestination = parentTransform.position;               // initializes the targetDestination (this should start the agent moving)
        playerTransform = orm.getPlayerTransform();                 // Player object reference
        visionScript = vc.GetComponentInChildren<Vision>();         // Vision script attached to this object (inside of VisionCone)
    }

    // Update is called once per frame
    void Update()
    {
        
        // Accumulates times between frames, changes direction once accumulator reaches certain threshold
        timeAccumulator += Time.time - timeSinceLastFrame;
        if(timeAccumulator > timeToSwitchTurn)
        {
            movementRotationRange *= -1.0f;
            timeAccumulator = 0.0f;
            // Gets a random value between the ranges of given seconds - this determines how long before the agent switches it's turn direction
            timeToSwitchTurn = Random.Range(timeToSwitchTurnRange[0], timeToSwitchTurnRange[1]);
        }

        stuckTimeAccumulator += Time.time - timeSinceLastFrame;
        checkStuck(stuckTimeAccumulator);

        timeSinceLastFrame = Time.time;



        // If the agent can see the player follow them
        if(visionScript.getCanSeeTarget())
        {
            // Follow the player
            agent.destination = playerTransform.position;
            targetDestination = playerTransform.position;
        }
        // Otherwise roam
        else
        {
            autonomousMove();
        }
    }

    void autonomousMove()
    {   
        // If the agent is close enough to its target destination, find a new target
        if (Mathf.Abs(parentTransform.position.x - targetDestination.x) <= targetRadius &&
            Mathf.Abs(parentTransform.position.z - targetDestination.z) <= targetRadius)
        {
            // Rotate the transform towards a direction in the given range
            parentTransform.Rotate(0,Random.Range(0, movementRotationRange),0);
            moveAgent(parentTransform);
        }
        else if(isStuck)
        {   
            isStuck = false;
            parentTransform.Rotate(0, 180, 0);
            moveAgent(parentTransform);            
        }
    }

    void moveAgent(Transform pTransform)
    {
            // Casts a ray towards that direction with infinite length
            Physics.Raycast(pTransform.position, pTransform.forward, out var hitInfo);
            // Move towards point hit by RayCast
            agent.destination = hitInfo.point;
            targetDestination = hitInfo.point;
            //clickIndicator.displayClickIndicator(hitInfo.point, true);   // Debug visual    
    }

    void checkStuck(float time)
    {   
        // Checks stuck every passed seconds vs threshold
        if (time >= stuckTimeCheck)
        {   
            // Stuck is considered if in the same position + threshold since last time checked
            if (Mathf.Abs(lastCheckedPos.x - parentTransform.position.x) <= stuckDistanceThreshold &&
                Mathf.Abs(lastCheckedPos.z - parentTransform.position.z) <= stuckDistanceThreshold)
            {
                isStuck = true;
            }
            lastCheckedPos = parentTransform.position;
            stuckTimeAccumulator = 0.0f;
        }
    }
}
