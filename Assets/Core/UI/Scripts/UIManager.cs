using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public ObjectReferenceManager orm;
    private EnergyManager em = null;
    public Image speedEnergyImg;

    public Texture2D cursor_point;
    public Texture2D cursor_click;
    Vector2 cursorHotspot = new Vector2(4,0);
    
    public void Start()
    {
        // Wait for the ORM to get a reference to the player object (does not happen right away);
        // There is probably a way to do it conditionally, (wait until playerObject is not null);
        Invoke("getPlayerObjectFromORM", 1);
    }

    public void getPlayerObjectFromORM()
    {
        em = orm.getPlayerObject().GetComponent<EnergyManager>();
    }

    public void Update()
    {   
        // If the reference to energy manager isn't null
        if(em != null)
        {
            // Update the image based on speed energy
            speedEnergyImg.fillAmount = em.getSpeedEnergy() / em.getSpeedEnergyMax();
        }

        if(Input.GetMouseButtonDown(0))
        {
            updateCursor(cursor_click);
        }
        if(Input.GetMouseButtonUp(0))
        {
            updateCursor(cursor_point);
        }
        
    }

    public void updateCursor(Texture2D texture)
    {
        Cursor.SetCursor(texture, cursorHotspot, CursorMode.Auto);
    }

    public void LoadMainLevel()
    {
        SceneManager.LoadScene(1);
    }
}
