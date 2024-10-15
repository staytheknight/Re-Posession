using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingPowerManager : MonoBehaviour
{
    GameObject wallClicked = null;
    GameObject wallHovered = null;

    [SerializeField] public new Camera camera;
    GameObject player;
    ClickEventListener cel;
    CollisionListener playerCL;

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
        cel = player.GetComponent<ClickEventListener>();
        playerCL = player.GetComponent<CollisionListener>();
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

        if (playerCL.getAgentCollided() != null)
        {
            if (playerCL.getAgentCollided().tag == "Agent")
            {
                GameObject nearestWall = findNearestHidingWall();
                wallClicked = nearestWall;
                Vector3 nearestWallPos = nearestWall.GetComponent<Transform>().position;
                GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().moveAgent(nearestWallPos, 1000.0f, false);
            }   
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
            bool doubleClicked = cel.doubleClickTracker();
            // Casts a ray from the assigned camera to the mouse position
            Ray clickedPosition = camera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(clickedPosition, out hitInfo);

            // If the click hit a hiding wall
            if (hitInfo.collider.tag == "HidingWall")
            {
                // Store a reference to that wall obj
                wallClicked = hitInfo.collider.gameObject;

                float movementSpeed;
                if(doubleClicked)
                {
                    movementSpeed = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().getFastMovementSpeed();
                }
                else
                {
                    movementSpeed = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().getDefaultMovementSpeed();
                }
                 
                // Move the player towards the wall
                GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().moveAgent(hitInfo.point, movementSpeed, doubleClicked);
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
    
    // TODO: Make radial raycast instead of distance from transform
    // Finds the nearest wall to the player - this is better done with a radial raycast around the player
    // as the current way uses the center of the transform which is not the most accurate way
    // I just didn't have time to make it a radial raycast
    public GameObject findNearestHidingWall()
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("HidingWall");
        float smallestDistance = 999999.9f;
        GameObject closestWall = walls[0];
        foreach(GameObject wall in walls)
        {
            float distance = Vector3.Distance(wall.GetComponent<Transform>().position,player.GetComponent<Transform>().position);
            if (distance <= smallestDistance)
            {
                smallestDistance = distance;
                closestWall = wall;
            }
        }
        return closestWall;
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
