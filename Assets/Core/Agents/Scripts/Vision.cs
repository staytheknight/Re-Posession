using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    public GameObject target;
    private bool canSeeTarget;

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
