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
    [SerializeField] public bool popup;
    [SerializeField] public bool isFinalLevel;
    [SerializeField] private LevelNode[] neighbours;

    [SerializeField] public EventManager em;
    [SerializeField] public DialogueManager dm;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        confirmUI = GameObject.Find("ConfirmationUI").GetComponentInChildren<Canvas>(true);
        // gameData = GameObject.Find("Managers").transform.Find("GameData").GetComponent<GameData>();

        // Managers
        gameData = GameObject.Find("Managers").GetComponent<GameData>();
        //panels = confirmUI.GetComponentsInChildren<UIP>
        em = GameObject.Find("Managers").GetComponent<EventManager>();

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

        if (level == LevelType.Battle)
        {
            sprite.sprite = nodeSprites[2];
        }
        else
        {
            sprite.sprite = nodeSprites[0];
        }
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
            if (level == LevelType.Battle)
            {
                sprite.sprite = nodeSprites[5];
            }
            else
            {
                sprite.sprite = nodeSprites[4];
            }
        }
    }


    /// <summary>
    /// Does something when user clicks on region collider   	
    /// </summary>
    void OnMouseDown()
    {
        if (confirmUI && unlocked && !popup && !gameData.paused && !dm.dialogueRunning)
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
        gameData.neighbours = neighbours;
        if (level == LevelType.Battle)
        {
            CloseUI();
            SceneManager.LoadScene("BattleMap" + levelIndex);
            Debug.Log("Battle Level");
        }
        else if (level == LevelType.Event)
        {
            CloseUI();
            // SceneManager.LoadScene("Dialogue"); //This should be changed to a random event once we know how many event levels there are
            em.StartEvent();
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
        if (unlocked && !popup && !gameData.paused && level == LevelType.Event)
        {
            sprite.sprite = nodeSprites[1];
        }
        else if (unlocked && !gameData.paused && !popup && level == LevelType.Battle)
        {
            sprite.sprite=nodeSprites[3];
        }
    }

    /// <summary>
    /// Checks to see if user has stopped hovering over region and removes hover anim
    /// </summary>
    void OnMouseExit()
    {
        //Currently changes colour of sprite, just placeholder for future anim
        if (level == LevelType.Event)
        {
            sprite.sprite = nodeSprites[0];
        }
        else
        {
            sprite.sprite = nodeSprites[2]; 
        }

    }

    public void CloseUI()
    {
        confirmUI.gameObject.SetActive(false);
    }

    public void LevelUnlock()
    {
        Debug.Log("Level " + levelNum + " unlocked");
        if (level == LevelType.Event)
        {
            sprite.sprite = nodeSprites[0];

        }
        {
            sprite.sprite = nodeSprites[2];
        }
        unlocked = true;
    }

    public void LevelLock()
    {
        Debug.Log("Level " + levelNum + " Locked");
        if (!unlocked)
        {
            if (level == LevelType.Battle)
            {
                sprite.sprite = nodeSprites[5];
            }
            else
            {
                sprite.sprite = nodeSprites[4];
            }
        }
        unlocked = false;
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
            else if (region == 2)
            {
                lvlCompletion = gameData.lvlStatusRegionTwo;
            }
            else if (region == 0)
            {
                lvlCompletion = gameData.lvlStatusRegionZero;
            }
            else if (region == 3)
            {
                lvlCompletion = gameData.lvlStatusRegionThree;
            }

            //double level = (levelIndex * 10) - (region * 10);
            if (lvlCompletion[(int)levelNum - 1] == true)
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

    public LevelNode[] GetNeighbours()
    {
        return neighbours;
    }
}
