using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private AudioManager audioManager;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioSource = gameObject.GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        //audioManager.ChangeMusic();
        audioSource.Play();
        SceneManager.LoadScene("Overworld");
    }

    public void Instructions()
    {
        audioSource.Play();
        SceneManager.LoadScene("Instructions");
    }
}
