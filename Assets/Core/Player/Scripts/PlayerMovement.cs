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

    private Vector3 rayCastPoint;
    private Vector3 target;
    private float targetRadius = 2.0f;
    public float defaultMovementSpeed = 3.5f;
    public float fasterMovementSpeed = 6.5f;

    // Click variables
    public float doubleClickTime = 0.2f;
    public float lastClickTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // Casts a ray from the assigned camera to the mouse position
        Ray movePosition = camera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(movePosition, out var hitInfo);
        rayCastPoint = hitInfo.point;

        // Originally tried to put this into a click manager but could not get it working in time
        // Double click detector
        if (Input.GetMouseButtonDown(0))
        {
            float timeSinceLastClick = Time.time - lastClickTime;

            // If double click and has energy, move faster
            if(timeSinceLastClick <= doubleClickTime && em.getSpeedEnergy() > 0)
            {
                moveAgent(fasterMovementSpeed, true);
            }
            // If single click move default speed
            else
            {
                moveAgent(defaultMovementSpeed, false);
            }
            
            lastClickTime = Time.time;
        }

        // When the player gets close enough to the target position, toggle regenerating energy
        if (Mathf.Abs(orm.getPlayerTransform().position.x - target.x) <= targetRadius &&
            Mathf.Abs(orm.getPlayerTransform().position.y - target.y) <= targetRadius)
        {
            em.toggleSEnergyReduce = false;
        }

        // If the player runs out of energy, return to default speed and regen energy
        if(em.getSpeedEnergy() <= 0)
        {
            moveAgent(defaultMovementSpeed, false);
        }
    }

    private void moveAgent(float movementSpeed, bool doubleClicked)
    {
        agent.SetDestination(rayCastPoint);
        target = rayCastPoint;
        agent.speed = movementSpeed;
        clickIndicatorScript.displayClickIndicator(rayCastPoint, doubleClicked);
        em.toggleSEnergyReduce = doubleClicked;
    }

}
