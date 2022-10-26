using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RegionLoader : MonoBehaviour /*, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler*/ //For UI Clicks
{
    // Start is called before the first frame update
    private SpriteRenderer sprite;
    public int regionIndex;
    public Color mouseExitColor;
    public Color mouseOverColor;
    private GameData gd;
    private LevelTransition lvlTransition;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        gd = GameObject.Find("Managers").GetComponent<GameData>();
        mouseExitColor = sprite.color;
        mouseOverColor = sprite.color;
        mouseOverColor.a = 0.5f;
        lvlTransition = GameObject.Find("LevelTransition").GetComponent<LevelTransition>();
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
        if (gd.overworldStatus[regionIndex] && !gd.paused)
        {
            //SceneManager.LoadScene("Region" + regionIndex);
            lvlTransition.FadeToLevel("Region" + regionIndex);
        }

    }

    /// <summary>
    /// Checks to see if user is hovering over region  	
    /// </summary>
    void OnMouseOver()
    {
        //Currently changes colour of sprite, just placeholder for future anim
        if (gd.overworldStatus[regionIndex] && !gd.paused)
        {
            sprite.color = mouseOverColor;
        }
    }

    /// <summary>
    /// Checks to see if user has stopped hovering over region  	
    /// </summary>
    void OnMouseExit()
    {
        //Currently changes colour of sprite, just placeholder for future anim
        if (gd.overworldStatus[regionIndex])
        {
            sprite.color = mouseExitColor;
        }
    }

    /* FOR UI CLICKS
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse Exit");
    }
    */
}
