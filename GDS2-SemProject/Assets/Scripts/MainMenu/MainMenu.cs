using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private AudioManager audioManager; 
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        audioManager.ChangeMusic();
        SceneManager.LoadScene("Overworld");
    }

    public void Instructions()
    {
        SceneManager.LoadScene("Instructions");
    }
}
