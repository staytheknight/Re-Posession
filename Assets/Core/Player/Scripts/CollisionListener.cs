using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionListener : MonoBehaviour
{   
    GameObject agentCollided = null;

    void OnCollisionEnter(Collision obj)
    {
        if(obj.gameObject.tag == "Agent")
        {
            //Debug.Log("Agent collided");
            agentCollided = obj.gameObject;
        }
    }

    public GameObject getAgentCollided()
    {
        return agentCollided;
    }

    public void setAgentCollided(GameObject b)
    {
        agentCollided = b;
    }
}
