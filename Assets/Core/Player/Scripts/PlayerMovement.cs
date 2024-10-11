using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public new Camera camera;
    public ControlManager cm;

    public GameObject clickLocObject;

    bool doubleClickToggle = false;
    public float defaultMovementSpeed = 3.5f;
    public float fasterMovementSpeed = 6.5f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // Casts a ray from the assigned camera to the mouse position
            Ray movePosition = camera.ScreenPointToRay(Input.mousePosition);
            // If ray hit
            if(Physics.Raycast(movePosition, out var hitInfo))
            {
                agent.SetDestination(hitInfo.point);
                //Debug.Log("Ray: " + hitInfo.point);
                Instantiate(clickLocObject, hitInfo.point, Quaternion.identity);
            }

            /*
            // Alternative way to do mouse to screen position, I left this here for debug
            Vector3 worldPosition = camera.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("Screen to World: " + worldPosition);
            */
            
        }

        if(cm.DoubleClickDetector())
        {   
            // Toggles the double click toggle;
            doubleClickToggle = !doubleClickToggle;

            // When double click is toggled, move faster
            if(doubleClickToggle)
            {
                agent.speed = fasterMovementSpeed;
            }
            else
            {
                agent.speed = defaultMovementSpeed;
            }
        }
    }
}
