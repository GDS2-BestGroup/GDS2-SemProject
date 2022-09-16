using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PopupDisplay : MonoBehaviour
{
    //[SerializeField] private GameObject display; //The tutorial indicator.
    [SerializeField] private string[] instructions;
    private TMP_Text text;
    private int count;


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
        }
        else
        {
            gameObject.SetActive(false);

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
