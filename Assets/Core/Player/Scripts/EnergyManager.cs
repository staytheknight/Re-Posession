using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    private float speedEnergy = 1000.0f;
    private float speedEnergyMax = 1000.0f;
    public bool toggleSEnergyReduce = false;


    public void reduceSpeedEnergy()
    {
        if (speedEnergy > 0)
        {
            speedEnergy--;
        }
    }

    public void Update()
    {
        if(toggleSEnergyReduce)
        {
            reduceSpeedEnergy();
        }
    }

    public float getSpeedEnergy()
    {
        return speedEnergy;
    }

    public float getSpeedEnergyMax()
    {
        return speedEnergyMax;
    }
}
