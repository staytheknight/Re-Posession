using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavScript : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public ObjectReferenceManager orm;
    public DisplayClickIndicator clickIndicator;                    // For debug visuals
    GameObject necroZone;
    GameObject necronomicon;

    // Scripts
    public GameObject vc;
    public Vision visionScript;
    EyeColourManager eyeColourManager;

    Transform playerTransform;                                      // Transform of the player obj
    Transform parentTransform;                                      // Transform of the obj this script is attached to
    
    // Movement
    public Vector3 targetDestination;                               // Target destionation for navMesh
    private float targetRadius = 2.0f;                             // Radius around target position the nav mesh agent will considered arrived

    // Stuck checks
    float stuckCheckInterval = 2.0f;
    float stuckTimeAccumulator = 0.0f;
    Vector3 lastCheckedPosition;
    
    float navMeshHitRadius = 25.0f;                                // Size of radius that agents check around themselves for new destination

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();        // Nav mesh agent
        parentTransform = agent.GetComponentInParent<Transform>();  // Transform of parent that this script is attached to
        lastCheckedPosition = parentTransform.position;             // initializes last checked position
        targetDestination = parentTransform.position;               // initializes the targetDestination (this should start the agent moving)
        playerTransform = orm.getPlayerTransform();                 // Player object reference
        visionScript = vc.GetComponentInChildren<Vision>();         // Vision script attached to this object (inside of VisionCone)

        eyeColourManager = GetComponent<EyeColourManager>();
        necroZone = GameObject.FindGameObjectsWithTag("Necronomicon Zone")[0];
        necronomicon = GameObject.FindGameObjectsWithTag("Necronomicon")[0];
    }

    // Update is called once per frame
    void Update()
    {   
        // If the agent is carrying the necronomicon, take it back to the necro zone
        if(necronomicon.GetComponent<NecronomiconManager>().getTargetCarrying() == gameObject)
        {   
            eyeColourManager.changeLightColour(Color.green);
            agent.destination = necroZone.GetComponent<Transform>().position;
            targetDestination = necroZone.GetComponent<Transform>().position;
        }
        // If the agent can see the player follow them
        else if(visionScript.getCanSeeTarget())
        {
            eyeColourManager.changeLightColour(Color.magenta);
            // Follow the player
            agent.destination = playerTransform.position;
            targetDestination = playerTransform.position;            
        }
        // Otherwise roam
        else
        {
            eyeColourManager.changeLightColour(Color.white);
            
            // Checks the stuck status of the agent within the stuckCheckInterval
            stuckTimeAccumulator += Time.deltaTime;
            if (stuckTimeAccumulator >= stuckCheckInterval)
            {
                // If stuck, get a new target point to move to
                if(checkStuck())
                {
                    randomPointOnNavMesh(parentTransform.position);
                    agent.destination = targetDestination;
                }
                stuckTimeAccumulator = 0.0f;
                lastCheckedPosition = parentTransform.position;
            }

            // If close enough to target, find another target
            if (Mathf.Abs(parentTransform.position.x - targetDestination.x) <= targetRadius &&
                Mathf.Abs(parentTransform.position.z - targetDestination.z) <= targetRadius)
            {
                randomPointOnNavMesh(parentTransform.position);
                agent.destination = targetDestination;
            }
        }
    }

    // Gets a random point on the navmesh within a radius around the agent
    // calls the function again if no point was found, sets point as target destination if found
    void randomPointOnNavMesh(Vector3 center)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * navMeshHitRadius;
        NavMeshHit hit;
        if (!NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            randomPointOnNavMesh(center);
        }
        else
        {
            targetDestination = hit.position;
        }
    }

    // Function that returns true if the agent is near it's last checked position
    bool checkStuck()
    {   
        if (Mathf.Abs(parentTransform.position.x - lastCheckedPosition.x) <= targetRadius &&
            Mathf.Abs(parentTransform.position.z - lastCheckedPosition.z) <= targetRadius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
