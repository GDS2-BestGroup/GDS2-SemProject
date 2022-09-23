using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialOverworld : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject popup;
    private GameData gd;
    void Start()
    {
        gd = GameObject.Find("Managers").GetComponent<GameData>();
        if (gd.overworldStatus[1] == false)
        {
            popup.SetActive(true);
        }
        else
        {
            popup.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
