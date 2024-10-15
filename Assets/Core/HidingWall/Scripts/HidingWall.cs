using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingWall : MonoBehaviour
{
    bool shakeBool = false;
    private Vector3 originalPosition;
    private Vector2 offsetPositionRange = new Vector2(-.2f,.2f);

    [SerializeField] public Material originalMaterial;
    [SerializeField] public Material shakeMaterial;
    [SerializeField] public Material hoveredMaterial;

    [SerializeField] public HidingPowerManager hpm;

    bool playerCollided = false;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = this.GetComponentInParent<Transform>().position;
        originalMaterial = this.GetComponentInParent<Renderer>().material;
        hpm = GameObject.FindWithTag("HidingPowerManager").GetComponent<HidingPowerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the shake boolean is turned on from a collision, shake the wall and turn it magenta
        if(shakeBool)
        {
            shake();
            this.GetComponentInParent<Renderer>().material = shakeMaterial;
        }
        // If the wall has been hovered over by player cursor, turn it blue
        else if(hpm.getWallHovered() == this.gameObject)
        {
            shake();
            this.GetComponentInParent<Renderer>().material = hoveredMaterial;
        }
        // Default wall position and colour / texture
        else
        {
            this.GetComponentInParent<Transform>().position = originalPosition;
            this.GetComponentInParent<Renderer>().material = originalMaterial;
        }
    }

    public void OnCollisionEnter(Collision c)
    {
        if(c.collider.tag == "Player" && hpm.getWallClicked() == this.gameObject)
        {
            playerCollided = true;
        }
    }

    public void OnTriggerEnter(Collider c)
    {
        if(c.GetComponent<Collider>().tag == "Player" && hpm.getWallClicked() == this.gameObject)
        {
            playerCollided = true;
        }   
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

    public bool getPlayerCollided()
    {
        return playerCollided;
    }

    public void setPlayerCollided(bool b)
    {
        playerCollided = b;
    }
}
