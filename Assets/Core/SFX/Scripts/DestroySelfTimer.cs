using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfTimer : MonoBehaviour
{   

    public int timer = 1;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroySelf", timer);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
