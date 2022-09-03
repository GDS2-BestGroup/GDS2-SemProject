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
    private int region;
    private bool unlockFirst;
    private bool[] lvlCompletion;
    public bool unlocked;
    public double levelIndex;
    public LevelType level;
    public GameData gameData;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        confirmUI = GameObject.Find("ConfirmationUI").GetComponentInChildren<Canvas>(true);
        gameData = GameObject.Find("GameData").GetComponent<GameData>();
        unlocked = false;
        unlockFirst = false;
        region = Mathf.FloorToInt((float)levelIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (!unlockFirst) //2nd boolean is to stop level 1 being unlocked after winning the level and leaving the region
        {
            UnlockFirstLevel();
        }

        if (!unlocked)
        {
            sprite.color = new Color(1f, 1f, 1f, 0.5f);
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
        

        /*if (level == LevelType.Battle)
        {
            SceneManager.LoadScene("BattleMap" + levelIndex);
        }
        else if (level == LevelType.Event)
        {
            SceneManager.LoadScene("Event" + levelIndex); //This should be changed to a random event once we know how many event levels there are
        }*/
        
    }

    public void OnLevelClick()
    {
        if (level == LevelType.Battle)
        {
            //SceneManager.LoadScene("BattleMap" + levelIndex);
            Debug.Log("Battle Level");
        }
        else if (level == LevelType.Event)
        {
            //SceneManager.LoadScene("Event" + levelIndex); //This should be changed to a random event once we know how many event levels there are
            Debug.Log("Event Level");
        }
    }

    /// <summary>
    /// Checks to see if user is hovering over region and triggers hover anim 	
    /// </summary>
    void OnMouseOver()
    {
        //Currently changes colour of sprite, just placeholder for future anim
        if (unlocked)
        {
            sprite.color = Color.black;
        }
    }

    /// <summary>
    /// Checks to see if user has stopped hovering over region and removes hover anim
    /// </summary>
    void OnMouseExit()
    {
        //Currently changes colour of sprite, just placeholder for future anim
        sprite.color = Color.white;
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
            /*else if (levels[levels.Length - 1].levelIndex == levelIndex)
            {
                unlocked = true;
                unlockFirst = true;
            }*/
            
            
            
           
        }
      
    }
}
