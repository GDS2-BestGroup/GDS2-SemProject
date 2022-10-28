using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionButton : MonoBehaviour
{
    private LevelTransition lvlTransition;
    public AudioClip click;
    private AudioManager am;

    // Start is called before the first frame update
    void Start()
    {
        lvlTransition = GameObject.Find("LevelTransition").GetComponent<LevelTransition>();
        am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadCredits()
    {
        am.PlaySfxAudio(click);
        lvlTransition.FadeToLevel("Credit");
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
