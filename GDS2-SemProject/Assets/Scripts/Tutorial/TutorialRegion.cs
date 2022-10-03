using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRegion : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private LevelNode lvlOne;
    [SerializeField] private GameObject popup;
    private GameData gd;
    void Start()
    {
        gd = GameObject.Find("Managers").GetComponent<GameData>();
        if (gd.GetLevelCompletion(0)[0]) //If level one is unlocked
        {
            popup.SetActive(true);
            lvlOne.popup = true;
        }
        else
        {
            popup.GetComponent<PopupDisplay>().DisableBlocker();
            popup.transform.parent.gameObject.SetActive(false);
            lvlOne.popup = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //REALLY NEED TO FIND A BETTER WAY TO DO THIS 
        GameObject[] popups = GameObject.FindGameObjectsWithTag("Panel");
        if (popups.Length == 0)
        {
            lvlOne.popup = false;
        }
    }
}
