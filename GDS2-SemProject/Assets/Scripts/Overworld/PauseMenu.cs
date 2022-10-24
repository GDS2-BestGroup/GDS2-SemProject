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
    private GameData gd;
    private AudioManager am;
    private AudioSource masterAudio;
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
        masterAudio = GameObject.Find("AudioManager").GetComponent<AudioSource>();
        volumeSlider.value = masterAudio.volume * 100;
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
        pauseCanvas.gameObject.SetActive(true);
        gd.paused = true;
        Time.timeScale = 0;
    }

    public void GoBackToScene(string sceneName)
    {
        pauseCanvas.gameObject.SetActive(false);
        SceneManager.LoadScene(sceneName);
        Time.timeScale = time;
        gd.paused = false;
    }

    private void OnLevelWasLoaded(int level)
    {
        pauseCanvas.worldCamera = Camera.main;
        confirmUI.worldCamera = Camera.main;
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

        if (level == 11 || level == 12 || level == 13 || level == 14 || level == 15 || level == 16 || level == 17 || level == 18 || level == 6 || level == 7) //Battle Levels
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

    public void VolumeAdjust()
    {
        masterAudio.volume = volumeSlider.normalizedValue;
    }

}
