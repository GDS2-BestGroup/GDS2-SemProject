using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas pauseCanvas;
    [SerializeField] private Button backBtn;
    [SerializeField] private TMP_Text backBtnTxt;
    [SerializeField] private Button yesBtn;
    [SerializeField] private TMP_Text confirmUIText;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider volumeSliderSFX;
    [SerializeField] private Image volImg;
    [SerializeField] private Sprite[] volIcons;
    [SerializeField] private Image volImgSFX;
    [SerializeField] private AudioClip btnClick;
    private GameData gd;
    private AudioManager am;
    private AudioSource masterAudio;
    private LevelTransition lvlTransition;
    [SerializeField] private Canvas confirmUI;
    private bool pauseAllowed;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        //pauseCanvas = GameObject.Find("PauseUI").GetComponentInChildren<Canvas>(true);
        pauseCanvas.gameObject.SetActive(false);
        //backButton = GameObject.Find("PauseUI").GetComponentInChildren<Button>(true);
        gd = GameObject.Find("Managers").GetComponent<GameData>();
        am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        lvlTransition = GameObject.Find("LevelTransition").GetComponent<LevelTransition>();
        masterAudio = am.GetAudioPlaying();
        volumeSlider.value = 100;
        volumeSliderSFX.value = 50;
        time = 1;

        confirmUI = GameObject.Find("ForfeitConfirmationUI").GetComponentInChildren<Canvas>(true);
        confirmUI.worldCamera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseAllowed)
        {
            if (gd.paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        am.PlaySfxAudio(btnClick);
        pauseCanvas.gameObject.SetActive(false);
        gd.paused = false;
        if (SceneManager.GetActiveScene().name.Contains("BattleMap"))
        {
            GameController gc = GameObject.Find("GameController").GetComponent<GameController>();
            Time.timeScale = gc.GetTime();
        }
        else
        {
            Time.timeScale = time;
        }
    }

    public void Pause()
    {
        am.PlaySfxAudio(btnClick);
        pauseCanvas.gameObject.SetActive(true);
        gd.paused = true;
        Time.timeScale = 0;
    }

    public void GoBackToScene(string sceneName)
    {
        am.PlaySfxAudio(btnClick);
        Time.timeScale = time;
        gd.paused = false;
        lvlTransition.FadeToLevel(sceneName);
        pauseCanvas.gameObject.SetActive(false);
        //SceneManager.LoadScene(sceneName);
    }

    private void OnLevelWasLoaded(int level)
    {
        pauseCanvas.worldCamera = Camera.main;
        confirmUI.worldCamera = Camera.main;
        lvlTransition = GameObject.Find("LevelTransition").GetComponent<LevelTransition>();
        if (level == 0)
        {
            pauseAllowed = false;
        }
        else
        {
            pauseAllowed = true;
        }

        if (level == 1)
        {
            backBtnTxt.text = "Main Menu";
            backBtn.onClick.RemoveAllListeners();
            backBtn.onClick.AddListener(delegate { GoBackToScene("MainMenu"); });
        }

        if (level == 2 || level == 3 || level == 4 || level == 19)
        {
            backBtnTxt.text = "Overworld";
            backBtn.onClick.RemoveAllListeners();
            backBtn.onClick.AddListener(delegate { GoBackToScene("Overworld"); });
        }

        if (level == 11 || level == 12 || level == 13 || level == 14 || level == 15 || level == 16 || level == 17 || level == 18 || level == 6 || level == 7 || level == 20 || level == 21 || level == 22 || level == 23) //Battle Levels
        {
            backBtnTxt.text = "Forfeit Battle";
            backBtn.onClick.RemoveAllListeners();
            backBtn.onClick.AddListener(OpenUI);
        }
    }

    public void OpenUI()
    {
        if (gd.morale - 300 <= 0)
        {
            confirmUIText.text = "Your morale will reach 0 if you forfeit and your troops will abandon you! Are you sure you want to forfeit?";
        }
        else
        {
            confirmUIText.text = $"Your morale will drop to {gd.morale - 300}! Are you sure you want to forfeit?";
        }
        confirmUI.gameObject.SetActive(true);
        GameController gc = GameObject.Find("GameController").GetComponent<GameController>();
        yesBtn.onClick.RemoveAllListeners();
        yesBtn.onClick.AddListener(Resume);
        yesBtn.onClick.AddListener(delegate { gc.EndGame(false); });
        yesBtn.onClick.AddListener(CloseUI);
    }

    public void CloseUI()
    {
        confirmUI.gameObject.SetActive(false);
    }

    public void GetAudio()
    {
        if (am)
        {
            masterAudio = am.GetAudioPlaying();
            VolumeAdjust();
        }
    }

    public void VolumeAdjust()
    {
        //masterAudio = am.GetAudioPlaying();
        masterAudio.volume = volumeSlider.normalizedValue;
        VolumeIconChange();
    }

    public void VolumeAdjustSFX()
    {
        //masterAudio = am.GetAudioPlaying();
        float vol = 0.3f * volumeSliderSFX.normalizedValue;
        am.swordAudio.volume = vol;
        am.knightAudio.volume = vol;
        am.mageAudio.volume = vol;
        am.archerAudio.volume = vol;
        am.wolfAudio.volume = vol;
        am.sfxAudio.volume = vol;
        VolumeIconChangeSFX();
    }

    public void VolumeIconChangeSFX()
    {
        if (volumeSliderSFX.normalizedValue >= 0.95)
        {
            volImgSFX.sprite = volIcons[3];
        }
        else if (volumeSliderSFX.normalizedValue >= 0.50 && volumeSliderSFX.normalizedValue < 0.95)
        {
            volImgSFX.sprite = volIcons[2];
        }
        else if (volumeSliderSFX.normalizedValue > 0 && volumeSliderSFX.normalizedValue < 0.50)
        {
            volImgSFX.sprite = volIcons[1];
        }
        else
        {
            volImgSFX.sprite = volIcons[0];
        }
    }

    public void VolumeIconChange()
    {
        if (masterAudio.volume >= 0.95)
        {
            volImg.sprite = volIcons[3];
        }
        else if (masterAudio.volume >= 0.50 && masterAudio.volume < 0.95)
        {
            volImg.sprite = volIcons[2];
        }
        else if (masterAudio.volume > 0 && masterAudio.volume < 0.50)
        {
            volImg.sprite = volIcons[1];
        }
        else
        {
            volImg.sprite = volIcons[0];
        }
    }

}
