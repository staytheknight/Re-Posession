using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeColourManager : MonoBehaviour
{
    Light[] lights;
    // Start is called before the first frame update
    void Start()
    {
        lights = this.GetComponentsInChildren<Light>();        
    }

    public void changeLightColour(Color colour)
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].color = colour;
        }
    }

    public Color[] getLightColours()
    {
        Color[] cLights = new Color[lights.Length];
        for (int i = 0; i < lights.Length; i++)
        {
            cLights[i] = lights[i].color;
        }

        return cLights;
    }
}
