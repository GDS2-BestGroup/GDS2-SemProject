using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMainMenu()
    {
        //Restart Game on Lose
        if (SceneManager.GetActiveScene().name == "LoseScene")
        {
            GameData gd = GameObject.Find("Managers").GetComponent<GameData>();
            MapCanvas mc = GameObject.Find("Map Canvas").GetComponent<MapCanvas>();
            GameObject dc = GameObject.Find("DialogueCanvas");
            GameObject am = GameObject.Find("AudioManager");

            Destroy(mc.gameObject);
            Destroy(gd.gameObject);
            Destroy(dc);
            Destroy(am);
        }
        SceneManager.LoadScene("MainMenu");
    }
}
