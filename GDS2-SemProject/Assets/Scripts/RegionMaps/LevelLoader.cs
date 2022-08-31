using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    private SpriteRenderer sprite;
    public double levelIndex;
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
        SceneManager.LoadScene("BattleMap" + levelIndex);
    }

    /// <summary>
    /// Checks to see if user is hovering over region and triggers hover anim 	
    /// </summary>
    void OnMouseOver()
    {
        //Currently changes colour of sprite, just placeholder for future anim
        sprite.color = Color.blue;
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
