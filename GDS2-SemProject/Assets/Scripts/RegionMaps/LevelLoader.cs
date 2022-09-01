using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public enum LevelType
    {
        Battle,
        Event
    }

    private SpriteRenderer sprite;
    public double levelIndex;
    public LevelType level;
    
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Does something when user clicks on region collider   	
    /// </summary>
    void OnMouseDown()
    {
        //Debug.Log(this.name + " selected");
        if (level == LevelType.Battle)
        {
            SceneManager.LoadScene("BattleMap" + levelIndex);
        }
        else if (level == LevelType.Event)
        {
            SceneManager.LoadScene("Event" + levelIndex); //This should be changed to a random event once we know how many event levels there are
        }
        
    }

    /// <summary>
    /// Checks to see if user is hovering over region and triggers hover anim 	
    /// </summary>
    void OnMouseOver()
    {
        //Currently changes colour of sprite, just placeholder for future anim
        sprite.color = Color.black;
    }

    /// <summary>
    /// Checks to see if user has stopped hovering over region and removes hover anim
    /// </summary>
    void OnMouseExit()
    {
        //Currently changes colour of sprite, just placeholder for future anim
        sprite.color = Color.white;
    }
}
