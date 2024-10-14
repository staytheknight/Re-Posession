using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] private new Light light;
    public float[] intensityRange = new float[2];
    public float intensityChangePerFrame = 0.1f;
    bool decreaseLightIntensity = false;

    // Update is called once per frame
    void Update()
    {
        if(light.intensity >= intensityRange[1])
        {
            decreaseLightIntensity = true;
        }
        else if(light.intensity <= intensityRange[0])
        {
            decreaseLightIntensity = false;
        }

        if(decreaseLightIntensity)
        {
            light.intensity -= intensityChangePerFrame;
        }
        else
        {
            light.intensity += intensityChangePerFrame;
        }
    }
}
