using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeColourManager : MonoBehaviour
{
    Light[] lights;
    Renderer meshRenderer;

    // This would be better as a dictionary, I just didn't have enough time to figure it out
    public Material red;
    public Material green;
    public Material magenta;

    // Start is called before the first frame update
    void Start()
    {
        lights = this.GetComponentsInChildren<Light>();
        meshRenderer = this.GetComponent<Renderer>();
    }

    public void changeLightColour(Color colour)
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].color = colour;
        }

        // This is better as a dictionary (see material definitions above)
        if (colour == Color.white)
        {
            changeMeshMaterial(red);
        }
        else if (colour == Color.green)
        {
            changeMeshMaterial(green);  
        }
        else if (colour == Color.magenta)
        {
            changeMeshMaterial(magenta);
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

    void changeMeshMaterial(Material m)
    {
        meshRenderer.material = m;
    }
}
