using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavScript : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public ObjectReferenceManager orm;

    public GameObject vc;
    public Vision visionScript;

    Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        getPlayerTransformFromORM();
        visionScript = vc.GetComponentInChildren<Vision>();
    }

    void getPlayerTransformFromORM()
    {
        playerTransform = orm.getPlayerTransform();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(visionScript.getCanSeeTarget())
        {
            // Follow the player
            agent.destination = playerTransform.position;
        }
    }
}
