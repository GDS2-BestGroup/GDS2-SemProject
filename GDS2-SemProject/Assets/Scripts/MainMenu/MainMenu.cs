using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private AudioManager audioManager;
    private AudioSource audioSource;
    [SerializeField] private LevelTransition levelTransition;
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
        audioManager.PlaySfxAudio(audioSource.clip);
        //SceneManager.LoadScene("Overworld");
        levelTransition.FadeToLevel("Overworld");
    }

    public void Credits()
    {
        audioManager.PlaySfxAudio(audioSource.clip);
        levelTransition.FadeToLevel("Credit");
    }

    public void Instructions()
    {
        audioSource.Play();
        SceneManager.LoadScene("Instructions");
    }
}
