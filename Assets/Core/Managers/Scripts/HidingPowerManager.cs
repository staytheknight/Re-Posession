using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingPowerManager : MonoBehaviour
{
    GameObject wallClicked = null;
    GameObject wallHovered = null;

    [SerializeField] public new Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        Ray hoveredPosition = camera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(hoveredPosition, out var hitInfo);
        if(hitInfo.collider != null)
        {
            wallHovered = hitInfo.collider.gameObject;
        }        

        // If the player left clicks, clear the wall clicked
        if(Input.GetMouseButtonDown(0))
        {
            if (wallClicked != null)
            {
                wallClicked.GetComponent<HidingWall>().setPlayerCollided(false);
            }
            wallClicked = null;
        }
        if(Input.GetMouseButtonDown(1))
        {
            // Casts a ray from the assigned camera to the mouse position
            Ray clickedPosition = camera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(clickedPosition, out hitInfo);

            // If the click hit a hiding wall
            if (hitInfo.collider.tag == "HidingWall")
            {
                // Store a reference to that wall obj
                wallClicked = hitInfo.collider.gameObject;
                // get the default movement speed
                float movementSpeed = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().getDefaultMovementSpeed();
                // Move the player towards the wall
                GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().moveAgent(hitInfo.point, movementSpeed, false);
            }
        }


        if (wallClicked != null)
        {
            if (wallClicked.GetComponent<HidingWall>().getPlayerCollided())
            {
            
            }
        }

        //Debug.Log("Wall Hovered: " + wallHovered + " ,Wall Clicked: " + wallClicked);

    }

    public GameObject getWallHovered()
    {
        return wallHovered;
    }

    public GameObject getWallClicked()
    {
        return wallClicked;
    }
}
