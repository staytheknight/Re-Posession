using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavScript : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public ObjectReferenceManager orm;
    public DisplayClickIndicator clickIndicator;                    // For debug visuals

    // Scripts
    public GameObject vc;
    public Vision visionScript;
    EyeColourManager eyeColourManager;

    Transform playerTransform;                                      // Transform of the player obj
    Transform parentTransform;                                      // Transform of the obj this script is attached to
    
    // Movement
    public Vector3 targetDestination;                               // Target destionation for navMesh
    private float targetRadius = 2.0f;                             // Radius around target position the nav mesh agent will considered arrived
    
    Vector2[] playAreaBoundaries = {new Vector2(-24.0f, 25.0f),new Vector2(-17.0f, 42.0f)};

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();        // Nav mesh agent
        parentTransform = agent.GetComponentInParent<Transform>();  // Transform of parent that this script is attached to
        targetDestination = parentTransform.position;               // initializes the targetDestination (this should start the agent moving)
        playerTransform = orm.getPlayerTransform();                 // Player object reference
        visionScript = vc.GetComponentInChildren<Vision>();         // Vision script attached to this object (inside of VisionCone)

        eyeColourManager = GetComponent<EyeColourManager>();
    }

    // Update is called once per frame
    void Update()
    {

        // If the agent can see the player follow them
        if(visionScript.getCanSeeTarget())
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
            
            // If close enough to target, find another target
            if (Mathf.Abs(parentTransform.position.x - targetDestination.x) <= targetRadius &&
                Mathf.Abs(parentTransform.position.z - targetDestination.z) <= targetRadius)
            {
                rayCastDown();
                agent.destination = targetDestination;
            }
        }
    }
    
    void rayCastDown()
    {
        // Ray cast down above the play area, in the range of the play area
        Vector3 rayStart = new Vector3(0,10,0);
        Vector3 down = new Vector3(0,-1,0);
        rayStart.x = Random.Range(playAreaBoundaries[0][0],playAreaBoundaries[0][1]);
        rayStart.z = Random.Range(playAreaBoundaries[1][0],playAreaBoundaries[1][0]);
        
        Physics.Raycast(rayStart, down, out var hitInfo);

        // if there is no collider hit by the ray, try again
        if(hitInfo.collider == null)
        {
            rayCastDown();
        }
        else
        {   
            // If the collider hit is NOT the floor, try again
            if(hitInfo.collider.tag != "Floor")
            {
                rayCastDown();
            }
            // If the floor was hit, set that as the new target position
            else
            {
                targetDestination = hitInfo.point;
            }
        }
    }
}
