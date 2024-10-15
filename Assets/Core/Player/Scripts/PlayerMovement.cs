using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public new Camera camera;
    public ObjectReferenceManager orm;
    public EnergyManager em;
    public DisplayClickIndicator clickIndicatorScript;
    public ClickEventListener cel;

    private Collider colliderHit;
    private Vector3 rayCastPoint;
    private Vector3 target;
    private float targetRadius = 2.0f;
    public float defaultMovementSpeed = 3.5f;
    public float fasterMovementSpeed = 6.5f;

    // Click variables
    public float doubleClickTime = 1.5f;
    public float lastClickTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        cel = GetComponentInParent<ClickEventListener>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            // Casts a ray from the assigned camera to the mouse position
            Ray movePosition = camera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(movePosition, out var hitInfo);
            rayCastPoint = hitInfo.point;
            colliderHit = hitInfo.collider;

            bool doubleClicked = cel.doubleClickTracker();
            if(colliderHit != null)
            {
                if(!doubleClicked && colliderHit.tag == "Floor")
                {
                    moveAgent(rayCastPoint, defaultMovementSpeed, false);
                }
                else if(doubleClicked && colliderHit.tag == "Floor")
                {
                    moveAgent(rayCastPoint, fasterMovementSpeed, true);
                }
            }
        }


        // When the player gets close enough to the target position, toggle regenerating energy
        if (Mathf.Abs(orm.getPlayerTransform().position.x - target.x) <= targetRadius &&
            Mathf.Abs(orm.getPlayerTransform().position.z - target.z) <= targetRadius)
        {
            em.toggleSEnergyReduce = false;
        }

        // If the player runs out of energy, return to default speed and regen energy
        if(em.getSpeedEnergy() <= 0)
        {
            moveAgent(rayCastPoint, defaultMovementSpeed, false);
        }
    }

    public void moveAgent(Vector3 destination, float movementSpeed, bool doubleClicked)
    {
        if (agent != null)
        {
            if (agent.enabled)
            {
                agent.SetDestination(destination);
                agent.speed = movementSpeed;
            }
        }
        target = destination;        
        clickIndicatorScript.displayClickIndicator(destination, doubleClicked);
        em.toggleSEnergyReduce = doubleClicked;
    }

    public float getDefaultMovementSpeed()
    {
        return defaultMovementSpeed;
    }

}
