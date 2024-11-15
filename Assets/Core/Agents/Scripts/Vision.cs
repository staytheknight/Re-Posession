using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    public GameObject target;
    private bool canSeeTarget;

    HidingPowerManager hpm;

    void Start()
    {
        target = GameObject.FindGameObjectsWithTag("Player")[0];
        hpm = GameObject.FindGameObjectWithTag("HidingPowerManager").GetComponent<HidingPowerManager>();
    }

    void Update()
    {
        if(hpm.getPlayerInWall())
        {
            canSeeTarget = false;
        }
    }

    void OnTriggerEnter(Collider objCollider)
    {
        if (objCollider == target.GetComponent<Collider>())
        {
            canSeeTarget = true;
        }
    }

    void OnTriggerExit(Collider objCollider)
    {
        if (objCollider == target.GetComponent<Collider>())
        {
            canSeeTarget = false;
        }
    }

    public bool getCanSeeTarget()
    {
        return canSeeTarget;
    }
}
