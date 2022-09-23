using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] backgroundMusic;
    public AudioSource audio;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void ChangeMusic ()
    {
        audio.clip = overworldClip;
        audio.Play();
    }*/

    private void OnLevelWasLoaded(int level)
    {
        if (level == 0 || level == 11 || level == 12 || level == 13 || level == 14 || level == 15 || level == 16 || level == 17 || level == 18 || level == 6 || level == 7)
        {
            audio.clip = backgroundMusic[0];
            audio.Play();
        }
        else
        {
            if (audio.clip != backgroundMusic[1])
            {
                audio.clip = backgroundMusic[1];
                audio.Play();
            }
        }
        
    }
}
