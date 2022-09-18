using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class LevelNode : MonoBehaviour
{
    public enum LevelType
    {
        Battle,
        Event
    }

    private SpriteRenderer sprite;
    [SerializeField]
    private Canvas confirmUI;
    private Button yesBtn;
    private GameObject[] panels;
    private bool panelActive;
    private int region;
    private bool unlockFirst;
    private bool[] lvlCompletion;
    private LineController lr;
    public Transform[] points;
    public bool unlocked;
    public double levelIndex;
    public LevelType level;
    public double levelNum;
    public GameData gameData;
    public Sprite[] nodeSprites;

    [SerializeField] public EventManager em;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        confirmUI = GameObject.Find("ConfirmationUI").GetComponentInChildren<Canvas>(true);
        gameData = GameObject.Find("GameData").GetComponent<GameData>();
        //panels = confirmUI.GetComponentsInChildren<UIP>
        unlocked = false;
        unlockFirst = false;
        region = Mathf.FloorToInt((float)levelIndex);
        lr = GetComponent<LineController>();
        if (points != null && lr)
        {
            Debug.Log("Line rendering");
            lr.SetUpLine(points);
        }
        
        levelNum = (levelIndex * 10) - (region * 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (!unlockFirst)
        {
            UnlockFirstLevel();
        }

        if (!unlocked)
        {
            sprite.color = Color.black;
        }
    }


    /// <summary>
    /// Does something when user clicks on region collider   	
    /// </summary>
    void OnMouseDown()
    {
        if (confirmUI && unlocked)
        {
            confirmUI.gameObject.SetActive(true);
            yesBtn = GameObject.Find("YesBtn").GetComponent<Button>();

            if (yesBtn)
            {
                yesBtn.onClick.RemoveAllListeners();
                yesBtn.onClick.AddListener(OnLevelClick);
            }
        }
    }

    public void OnLevelClick()
    {
        gameData.previousLevel = SceneManager.GetActiveScene().name;
        gameData.currentLevel = (int)levelNum;
        gameData.currentRegion = region;
        if (level == LevelType.Battle)
        {
            SceneManager.LoadScene("BattleMap" + levelIndex);
            Debug.Log("Battle Level");
        }
        else if (level == LevelType.Event)
        {
            SceneManager.LoadScene("Dialogue"); //This should be changed to a random event once we know how many event levels there are
            // Debug.Log("Event Level");
        }
    }

    /// <summary>
    /// Checks to see if user is hovering over region and triggers hover anim 	
    /// </summary>
    void OnMouseOver()
    {
        //Checks whether any panels are active to stop btm hover anim
        //panelActive = false;
        /*foreach (GameObject panel in panels)
        {
            if (panel.activeSelf)
            {
                panelActive = true;
            }
        }*/
        //Currently changes colour of sprite, just placeholder for future anim
        if (unlocked)
        {
            sprite.sprite = nodeSprites[1];
        }
    }

    /// <summary>
    /// Checks to see if user has stopped hovering over region and removes hover anim
    /// </summary>
    void OnMouseExit()
    {
        //Currently changes colour of sprite, just placeholder for future anim
        sprite.sprite = nodeSprites[0];
    }

    public void CloseUI()
    {
        confirmUI.gameObject.SetActive(false);
    }

    public void GoBack()
    {
        SceneManager.LoadScene("Overworld");
    }

    public void LevelUnlock()
    {
        sprite.color = Color.white;
    }

    /// <summary>
    /// Unlocks the first level of the region	
    /// </summary>
    public void UnlockFirstLevel()
    {
        LevelNode[] levels = gameData.GetLevels(region);
        if (gameData)
        {
            //Debug.Log(levels.Length + "region: " + region);
            if (region == 1)
            {
                lvlCompletion = gameData.lvlStatusRegionOne;
            }
            else
            {
                lvlCompletion = gameData.lvlStatusRegionTwo;
            }

            double level = (levelIndex * 10) - (region * 10);
            if (lvlCompletion[(int)level - 1] == true)
            {
                unlocked = true;
                unlockFirst = true;
            }
            else
            {
                unlocked = false;
                unlockFirst = true;
            }

        }

    }
}
