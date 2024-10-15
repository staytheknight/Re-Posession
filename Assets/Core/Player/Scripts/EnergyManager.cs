using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    private float speedEnergy = 1000.0f;
    private float speedEnergyMax = 1000.0f;
    public float speedEnergyDrainRate = 2.0f;
    public float speedEnergyGainRate = 0.5f;
    public bool toggleSEnergyReduce = false;


    // TODO: Energy is used for hiding and speed boost, make generic energy management code
    public void reduceSpeedEnergy()
    {
        if (speedEnergy > 0)
        {
            speedEnergy -= speedEnergyDrainRate;
        }
    }

    public void increaseSpeedEnergy()
    {
        if(speedEnergy < speedEnergyMax)
        {
            speedEnergy += speedEnergyGainRate;
        }
    }

    public void Update()
    {
        if(toggleSEnergyReduce)
        {
            reduceSpeedEnergy();
        }
        else
        {
            increaseSpeedEnergy();
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
