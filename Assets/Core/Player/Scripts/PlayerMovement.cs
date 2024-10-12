using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public new Camera camera;
    public ObjectReferenceManager orm;
    public ControlManager cm;
    public EnergyManager em;
    public DisplayClickIndicator clickIndicatorScript;

    private Vector3 rayCastPoint;
    private Vector3 target;
    private float targetRadius = 2.0f;
    public float defaultMovementSpeed = 3.5f;
    public float fasterMovementSpeed = 6.5f;

    bool singleClick;
    bool doubleClick;

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


        if (Input.GetMouseButtonDown(0))
        {
            bool[] clicks = cm.DoubleClickDetector();
            singleClick = clicks[0];
            doubleClick = clicks[1];
        }

        // If single click is detected
        if(singleClick)
        {   
            // If double click is detected
            if(doubleClick)
            {
                // If there is enough energy to 'move faster'
                if(em.getSpeedEnergy() > 0)
                {
                    //Debug.Log("DC!");
                    moveAgent(fasterMovementSpeed, true);
                }
                else
                {   
                    //Debug.Log("DC - Not enough energy!");
                    moveAgent(defaultMovementSpeed, false);
                }
            }
            else
            {
                //Debug.Log("Single Click");
                moveAgent(defaultMovementSpeed, false);
            }
        }

        singleClick = false;
        doubleClick = false;

        // When the player gets close enough to the target position, toggle regenerating energy
        if (Mathf.Abs(orm.getPlayerTransform().position.x - target.x) <= targetRadius &&
            Mathf.Abs(orm.getPlayerTransform().position.y - target.y) <= targetRadius)
        {
            em.toggleSEnergyReduce = false;
        }

        // If the player runs out of energy, return to default speed and regen energy
        if(em.getSpeedEnergy() <= 0)
        {
            em.toggleSEnergyReduce = false;
            agent.speed = defaultMovementSpeed;
            clickIndicatorScript.displayClickIndicator(target, false);
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
