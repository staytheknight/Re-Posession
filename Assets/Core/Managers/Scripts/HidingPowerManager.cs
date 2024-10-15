using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingPowerManager : MonoBehaviour
{
    GameObject wallClicked = null;
    GameObject wallHovered = null;

    [SerializeField] public new Camera camera;
    GameObject player;

    bool playerInWall = false;
    GameObject playerInWallObj = null;

    float hidingEnergy = 1000.0f;
    float hidingEnergyMax = 1000.0f;
    [SerializeField] float hidingEnergyDrainRate = 2.0f;
    [SerializeField] float hidingEnergyGainRate = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
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
            unHidePlayer();
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
            // If the player collided with the wall clicked 'hide' in the wall
            if (wallClicked.GetComponent<HidingWall>().getPlayerCollided())
            {
                togglePlayerComponents(false);
                playerInWall = true;
                playerInWallObj = wallClicked;

                wallClicked.GetComponent<HidingWall>().setShakeBool(true);
            }
        }

        if(playerInWall)
        {
            reduceHidingEnergy();
        }
        else
        {
            increaseHidingEnergy();
        }

        if(hidingEnergy <= 0)
        {
            unHidePlayer();
        }

        //Debug.Log("Wall Hovered: " + wallHovered + " ,Wall Clicked: " + wallClicked);

    }

    void togglePlayerComponents(bool toggle)
    {
        player.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = toggle;
        player.GetComponent<CapsuleCollider>().enabled = toggle;
        player.GetComponent<Renderer>().enabled = toggle;
        player.GetComponentInChildren<SpriteRenderer>().enabled = toggle;
        player.GetComponent<Light>().enabled = toggle;
    }

    void unHidePlayer()
    {
        playerInWall = false;
        if (playerInWallObj != null) 
        {
            playerInWallObj.GetComponent<HidingWall>().setShakeBool(false);
        }            
        togglePlayerComponents(true);
        player.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = player.GetComponent<PlayerMovement>().getDefaultMovementSpeed();
    }

    // TODO: Energy is used for hiding and speed boost, make generic energy management code
    public void reduceHidingEnergy()
    {
        if (hidingEnergy > 0)
        {
            hidingEnergy -= hidingEnergyDrainRate;
        }
    }

    public void increaseHidingEnergy()
    {
        if (hidingEnergy < hidingEnergyMax)
        {
            hidingEnergy += hidingEnergyGainRate;
        }
    }

    public GameObject getWallHovered()
    {
        return wallHovered;
    }

    public GameObject getWallClicked()
    {
        return wallClicked;
    }

    public bool getPlayerInWall()
    {
        return playerInWall;
    }

    public float getHidingEnergy()
    {
        return hidingEnergy;
    }

    public float getHidingEnergyMax()
    {
        return hidingEnergyMax;
    }
}
