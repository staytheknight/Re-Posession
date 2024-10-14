using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingWall : MonoBehaviour
{
    bool shakeBool = false;
    private Vector3 originalPosition;
    private Vector2 offsetPositionRange = new Vector2(-5.0f,5.0f);

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = this.GetComponentInParent<Transform>().position;    
    }

    // Update is called once per frame
    void Update()
    {
        if(shakeBool)
        {
            shake();
        }
        else
        {
            this.GetComponentInParent<Transform>().position = originalPosition;
        }
    }

    public void onCollision(Collision c)
    {
        // Call function in wall power manager
    }

    void shake()
    {   
        // If the object is not in it's original position, move it back
        if (this.GetComponentInParent<Transform>().position != originalPosition)
        {
            this.GetComponentInParent<Transform>().position = originalPosition;
        }
        // If the object is in it's original position move it to a random spot in range on XY plane
        else
        {
            Vector3 offset = new Vector3(Random.Range(offsetPositionRange[0],offsetPositionRange[1]),
                                         0,
                                         Random.Range(offsetPositionRange[0],offsetPositionRange[1]));
            this.GetComponentInParent<Transform>().position = this.GetComponentInParent<Transform>().position + offset;
        }
        
    }
}
