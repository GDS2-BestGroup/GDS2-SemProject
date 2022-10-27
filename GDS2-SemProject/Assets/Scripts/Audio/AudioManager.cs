using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] backgroundMusic;
    public AudioSource audioCurrent;
    public AudioSource audioNext;
    public AudioSource audioPlaying;
    public AudioSource swordAudio;
    public AudioSource knightAudio;
    public AudioSource mageAudio;
    public AudioSource wolfAudio;
    public AudioSource archerAudio;
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
        //audioCurrent = GetComponent<AudioSource>();
        audioNext.volume = 0;
        audioPlaying = audioCurrent;
        rootTwelve = Mathf.Pow(2.0f, 1.0f / 12.0f); 
        Debug.Log("The 12th root of 2 to the power of 7 is: " + Mathf.Pow(rootTwelve, 7));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreasePitch()
    {
        if ((audioCurrent.pitch * rootTwelve) < Mathf.Pow(rootTwelve, 7))
        {
            audioCurrent.pitch *= rootTwelve;
        }
        else
        {
            audioCurrent.pitch = Mathf.Pow(rootTwelve, 7);
        }
    }

    IEnumerator Crossfade(AudioSource fadeOut, AudioSource fadeIn)
    {
        float totalTime = 3; // fade audio out over 3 seconds
        float currentTime = 0;
        float initialVolume = fadeOut.volume;
        while (fadeOut.volume > 0 && fadeIn.volume < initialVolume)
        {
            currentTime += Time.deltaTime;
            fadeOut.volume -= 0.01f; //Mathf.Lerp(initialVolume, 0, currentTime / totalTime);
            fadeIn.volume += 0.01f; //Mathf.Lerp(0, initialVolume, currentTime / totalTime);
            yield return null;
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        //audioCurrent.pitch = 1;
        if (level == 0) //Main Menu
        {
            AudioSwap(backgroundMusic[0]);
        }
        else if (level == 11 || level == 12 || level == 13 || level == 14 || level == 15 || level == 16 || level == 17 || level == 18 || level == 6 || level == 7 || level == 20 || level == 21 || level == 22 || level == 23) //Battle
        {
            AudioSwap(backgroundMusic[2]);
        }
        else //Overworld/Region Maps
        {
            AudioSwap(backgroundMusic[1]);
        }

    }

    public AudioSource GetAudioPlaying()
    {
        /*if (audioCurrent.volume != 0)
        {
            return audioCurrent;
        }
        else
        {
            return audioNext;
        }*/
        return audioPlaying;
    }

    public void PlaySfxAudio(string unitName, AudioClip audio)
    {
        if (!swordAudio.isPlaying && unitName.Contains("Sword"))
        {
            swordAudio.clip = audio;
            swordAudio.Play();
        }
        else if (!archerAudio.isPlaying && unitName.Contains("Archer"))
        {
            archerAudio.clip = audio;
            archerAudio.Play();
        }
        else if (!knightAudio.isPlaying && unitName.Contains("Knight"))
        {
            knightAudio.clip = audio;
            knightAudio.Play();
        }
        else if (!mageAudio.isPlaying && unitName.Contains("Mage"))
        {
            mageAudio.clip = audio;
            mageAudio.Play();
        }
        else if (!wolfAudio.isPlaying && unitName.Contains("Wolf"))
        {
            wolfAudio.clip = audio;
            wolfAudio.Play();
        }
    }

    private void AudioSwap(AudioClip newAudio)
    {
        if (audioCurrent.volume != 0)
        {
            if (audioCurrent.clip != newAudio)
            {
                audioNext.pitch = 1;
                audioNext.clip = newAudio;
                audioNext.volume = 0;
                audioNext.Play();
                StartCoroutine(Crossfade(audioCurrent, audioNext));
                audioPlaying = audioNext;
            }
        }
        else
        {
            if (audioNext.clip != newAudio)
            {
                audioCurrent.pitch = 1;
                audioCurrent.clip = newAudio;
                audioCurrent.volume = 0;
                audioCurrent.Play();
                StartCoroutine(Crossfade(audioNext, audioCurrent));
                audioPlaying = audioCurrent;
            }
        }
    }
}
