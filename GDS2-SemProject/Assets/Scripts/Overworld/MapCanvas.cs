using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
public class MapCanvas : MonoBehaviour
{
    [SerializeField] private Sprite[] backgrounds;
    [SerializeField] private Image backgroundImg;
    [SerializeField] private Canvas canvas;
    [SerializeField] private TMP_Text text;
    [SerializeField] private TMP_Text moraleTxt;
    [SerializeField] private Button backBtn;
    [SerializeField] private Slider moraleSlider;
    [SerializeField] private Button pauseBtn;
    private GameData gd;
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Respawn");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update

    void Start()
    {
        gd = GameObject.Find("Managers").GetComponent<GameData>();
        canvas = this.GetComponent<Canvas>();
        EnableCanvas();
        moraleTxt.text = gd.morale + "/1000";
        moraleSlider.value = gd.morale;
        //backgroundImg = GameObject.Find("Background").GetComponent<Image>();
        //text = GameObject.Find("Title").GetComponent<TMP_Text>();
        //moraleTxt = GameObject.Find("MoraleCounter").GetComponent<TMP_Text>();
        //backBtn = GameObject.Find("BackBtn").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnLevelWasLoaded(int level)
    {
        //Regions start from int 1
        Debug.Log("This is level " + level);
        canvas.worldCamera = Camera.main;
        if (moraleTxt != null && gd != null)
        {
            moraleTxt.text = "Morale: " + gd.morale;
        }
        if (level == 1) //Overworld
        {
            EnableCanvas();
            text.text = "Overworld";
            backgroundImg.sprite = backgrounds[0];
        }
        else if (level == 2) //Tut Region
        {
            EnableCanvas();
            text.text = "Tutorial Region";
            backgroundImg.sprite = backgrounds[1];
        }
        else if (level == 3) //Region 1
        {
            EnableCanvas();
            text.text = "Region 1";
            backgroundImg.sprite = backgrounds[2];
        }
        else if (level == 4) //Region 2
        {
            EnableCanvas();
            text.text = "Region 2";
            backgroundImg.sprite = backgrounds[3];
        }
        else if (level == 19)
        {
            EnableCanvas();
            text.text = "Region 3";
            backgroundImg.sprite = backgrounds[4];
        }
        else
        {
            DisableCanvas();
        }
    }

    private void EnableCanvas()
    {
        if (backgroundImg)
        {
            backgroundImg.gameObject.SetActive(true);

        }
        if (moraleTxt)
        {
            moraleTxt.gameObject.SetActive(true);

        }
        if (text)
        {
            text.gameObject.SetActive(true);
        }
        if (backBtn)
        {
            backBtn.gameObject.SetActive(true);
        }
        if (moraleSlider)
        {
            moraleSlider.gameObject.SetActive(true);
        }
        if (pauseBtn)
        {
            pauseBtn.gameObject.SetActive(true);
        }
    }

    private void DisableCanvas()
    {
        backgroundImg.gameObject.SetActive(false);
        moraleTxt.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
        backBtn.gameObject.SetActive(false);
        moraleSlider.gameObject.SetActive(false);
        pauseBtn.gameObject.SetActive(false);
    }

    public void GoBack()
    {
        if (SceneManager.GetActiveScene().name == "Overworld")
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            SceneManager.LoadScene("Overworld");
        }
    }

    public void UpdateMorale()
    {
        if(gd.morale > 1000)
        {
            moraleTxt.text = 1000 + "/1000";
            moraleSlider.value = 1000;
        }
        else 
        {
            moraleTxt.text = gd.morale + "/1000";
            moraleSlider.value = gd.morale;
        }
    }
}
