using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] backgroundMusic;
    public AudioSource audio;
    public float rootTwelve;
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Audio");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        rootTwelve = Mathf.Pow(2.0f, 1.0f / 12.0f); 
        Debug.Log("The 12th root of 2 to the power of 7 is: " + Mathf.Pow(rootTwelve, 7));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreasePitch()
    {
        if ((audio.pitch * rootTwelve) < Mathf.Pow(rootTwelve, 7))
        {
            audio.pitch *= rootTwelve;
        }
        else
        {
            audio.pitch = Mathf.Pow(rootTwelve, 7);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        audio.pitch = 1;
        if (level == 0) //Main Menu
        {
            audio.clip = backgroundMusic[0];
            audio.Play();
        }
        else if (level == 11 || level == 12 || level == 13 || level == 14 || level == 15 || level == 16 || level == 17 || level == 18 || level == 6 || level == 7 || level == 20 || level == 21 || level == 22 || level == 23) //Battle
        {
            audio.clip = backgroundMusic[2];
            audio.Play();
        }
        else //Overworld/Region Maps
        {
            if (audio.clip != backgroundMusic[1])
            {
                audio.clip = backgroundMusic[1];
                audio.Play();
            }
        }
        
    }
}
