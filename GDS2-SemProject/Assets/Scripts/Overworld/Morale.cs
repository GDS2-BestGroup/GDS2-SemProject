using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Morale : MonoBehaviour
{
    // Start is called before the first frame update
    TMP_Text moraleTxt;
    GameData gd;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Respawn");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        moraleTxt = GetComponentInChildren<TMP_Text>();
        gd = GameObject.Find("GameData").GetComponent<GameData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moraleTxt)
        {
            moraleTxt.text = "Morale: " + gd.morale;
        }
        else
        {
            Debug.Log("Component not found");
        }
    }
}
