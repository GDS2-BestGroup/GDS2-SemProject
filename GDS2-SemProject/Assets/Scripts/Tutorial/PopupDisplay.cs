using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PopupDisplay : MonoBehaviour
{
    [SerializeField] private GameObject highlight; //Highlight panel.
    [SerializeField] private GameObject clickBlocker; //Transparent panel that stops the usrr interacting with the rest of the scene.
    [SerializeField] public bool clickBlockerEnabled;
    [SerializeField] private GameObject nextPopup; //Next popup to enable, if any
    [SerializeField] private int enableDisplay; //instruction number at which display should be enabled;
    [SerializeField] private bool resume;
    [SerializeField] private bool pause;
    [SerializeField] private bool notLast;
    [SerializeField] private string[] instructions; //Text to display on popup
    private TMP_Text text; //Actual text element of popup
    private int count; //Keeps track of what instruction is currently being displayed
    private float previousTimeScale;
    private GameController gc;
    private GameData gd;

    private void Awake()
    {
        if (clickBlocker != null)
        {
            clickBlocker.SetActive(clickBlocker);
        }
    }

    void Start()
    {
        //clickBlocker = GameObject.Find("ClickBlocker", true);
        GetComponent<Collider2D>().isTrigger = true;
        count = 0;
        text = GetComponentInChildren<TMP_Text>();
        gd = GameObject.Find("Managers").GetComponent<GameData>();
        if (GameObject.Find("GameController").GetComponent<GameController>())
        {
            gc = GameObject.Find("GameController").GetComponent<GameController>();
        }
        //display.SetActive(false);
    }

    private void Update()
    {
        if (gameObject.activeSelf && pause)
        {
            previousTimeScale = Time.timeScale;
            Debug.Log(previousTimeScale);
            Time.timeScale = 0;
            Debug.Log("Paused");
        }
    }


    private void OnMouseDown()
    {
        if (!gd.paused)
        {
            Debug.Log("Clicked Popup");
            if (text.text != instructions[^1]) //if not last instruction
            {
                text.text = instructions[++count];
                if (count == enableDisplay)
                {
                    highlight.SetActive(true);
                }

            }
            else
            {
                if (resume && gc)
                {
                    Time.timeScale = gc.GetTime();
                    Debug.Log("Unpaused");

                }

                gameObject.SetActive(false);
                if (clickBlocker)
                {
                    clickBlocker.SetActive(false);

                }
                if (highlight)
                {
                    highlight.SetActive(false);
                }
                if (nextPopup)
                {
                    nextPopup.SetActive(true);
                }
                else if (!notLast)
                {
                    this.transform.parent.gameObject.SetActive(false);
                }

            }
        }

    }

    public void DisableBlocker()
    {
        clickBlocker.SetActive(false);
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
