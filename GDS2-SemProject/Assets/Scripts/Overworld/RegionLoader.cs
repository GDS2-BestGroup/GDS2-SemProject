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
        SceneManager.LoadScene("Region"+ regionIndex);
    }

    /// <summary>
    /// Checks to see if user is hovering over region  	
    /// </summary>
    void OnMouseOver()
    {
        //Currently changes colour of sprite, just placeholder for future anim
        sprite.color = Color.blue;
    }

    /// <summary>
    /// Checks to see if user has stopped hovering over region  	
    /// </summary>
    void OnMouseExit()
    {
        //Currently changes colour of sprite, just placeholder for future anim
        sprite.color = Color.red;
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
