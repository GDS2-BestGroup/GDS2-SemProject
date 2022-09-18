using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PopupDisplay : MonoBehaviour
{
    [SerializeField] private GameObject display; //Highlight panel.
    [SerializeField] private int enableDisplay; //instruction number at which display should be enabled;
    [SerializeField] private string[] instructions; //Text to display on popup
    private TMP_Text text; //Actual text element of popup
    private int count; //Keeps track of what instruction is currently being displayed


    void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
        count = 0;
        text = GetComponentInChildren<TMP_Text>();
        //display.SetActive(false);
    }



    private void OnMouseDown()
    {
        Debug.Log("Clicked Popup");
        if (text.text != instructions[^1]) //if not last instruction
        {
            text.text = instructions[++count];
            if (count == enableDisplay)
            {
                display.SetActive(true);
            }

        }
        else
        {
            gameObject.SetActive(false);
            display.SetActive(false);
        }

    }


    /*private void PlayerCheck(Collider2D collision, bool active)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            display.SetActive(active);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerCheck(collision, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerCheck(collision, false);
    }*/
}
