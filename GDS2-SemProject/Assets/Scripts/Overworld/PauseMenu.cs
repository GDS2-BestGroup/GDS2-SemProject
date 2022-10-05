using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class PauseMenu : MonoBehaviour
{
    private Canvas pauseCanvas;
    [SerializeField] private Button backBtn;
    [SerializeField] private TMP_Text backBtnTxt;
    private GameData gd;
    private bool pauseAllowed;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        pauseCanvas = GameObject.Find("PauseUI").GetComponentInChildren<Canvas>(true);
        pauseCanvas.gameObject.SetActive(false);
        //backButton = GameObject.Find("PauseUI").GetComponentInChildren<Button>(true);
        gd = GameObject.Find("Managers").GetComponent<GameData>();
        time = 1;
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
    }

    private void OnLevelWasLoaded(int level)
    {
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
            backBtnTxt.text = "Return to Main Menu";
            backBtn.onClick.RemoveAllListeners();
            backBtn.onClick.AddListener(delegate { GoBackToScene("MainMenu"); });
        }

        if (level == 2 || level == 3 || level == 4)
        {
            backBtnTxt.text = "Return to Overworld";
            backBtn.onClick.RemoveAllListeners();
            backBtn.onClick.AddListener(delegate { GoBackToScene("Overworld"); });
        }

        if (level == 11 || level == 12 || level == 13 || level == 14 || level == 15 || level == 16 || level == 17 || level == 18 || level == 6 || level == 7) //Battle Levels
        {
            GameController gc = GameObject.Find("GameController").GetComponent<GameController>();
            backBtnTxt.text = "Forfeit Battle";
            backBtn.onClick.RemoveAllListeners();
            backBtn.onClick.AddListener(Resume);
            backBtn.onClick.AddListener(delegate { gc.EndGame(false); });
        }
    }

}
