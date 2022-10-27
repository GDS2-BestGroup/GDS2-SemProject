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
    [SerializeField] private TMP_Text goldTxt;
    [SerializeField] private Button backBtn;
    [SerializeField] private Slider moraleSlider;
    [SerializeField] private Button pauseBtn;
    [SerializeField] private Button upgradeBtn;
    [SerializeField] private Image goldImg;
    private LevelTransition lvlTransition;

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
        lvlTransition = GameObject.Find("LevelTransition").GetComponent<LevelTransition>();
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
        lvlTransition = GameObject.Find("LevelTransition").GetComponent<LevelTransition>();
        if (moraleTxt != null && gd != null)
        {
            moraleTxt.text = gd.morale + "/1000";
            moraleSlider.value = gd.morale;
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
            text.text = "Region One";
            backgroundImg.sprite = backgrounds[2];
        }
        else if (level == 4) //Region 2
        {
            EnableCanvas();
            text.text = "Region Two";
            backgroundImg.sprite = backgrounds[3];
        }
        else if (level == 19) //Region 3
        {
            EnableCanvas();
            text.text = "Region Three";
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
        if (upgradeBtn)
        {
            upgradeBtn.gameObject.SetActive(true);
        }
        if (goldTxt)
        {
            goldTxt.gameObject.SetActive(true);
        }
        if (goldImg)
        {
            goldImg.gameObject.SetActive(true);
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
        upgradeBtn.gameObject.SetActive(false);
        goldTxt.gameObject.SetActive(false);
        goldImg.gameObject.SetActive(false);
    }

    public void GoBack()
    {
        if (SceneManager.GetActiveScene().name == "Overworld")
        {
            //SceneManager.LoadScene("MainMenu");
            lvlTransition.FadeToLevel("MainMenu");
        }
        else
        {
            //SceneManager.LoadScene("Overworld");
            lvlTransition.FadeToLevel("Overworld");
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

    public void UpdateGold()
    {
        goldTxt.text = gd.GetGold().ToString();
    }
}
