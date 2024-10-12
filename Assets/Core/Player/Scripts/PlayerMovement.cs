using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public new Camera camera;
    public ControlManager cm;
    public EnergyManager em;
    public DisplayClickIndicator clickIndicatorScript;

    private Vector3 rayCastPoint;
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
                    fasterMovement();
                    em.toggleSEnergyReduce = true;
                }
                else
                {   
                    //Debug.Log("DC - Not enough energy!");
                    defaultMovement();
                }
            }
            else
            {
                //Debug.Log("Single Click");
                defaultMovement();
            }
        }

        singleClick = false;
        doubleClick = false;

    }

    private void defaultMovement()
    {
        agent.SetDestination(rayCastPoint);
        agent.speed = defaultMovementSpeed;
        clickIndicatorScript.displayClickIndicator(rayCastPoint, false);
    }

    private void fasterMovement()
    {
        agent.SetDestination(rayCastPoint);
        agent.speed = fasterMovementSpeed;
        clickIndicatorScript.displayClickIndicator(rayCastPoint, true);
    }
}
