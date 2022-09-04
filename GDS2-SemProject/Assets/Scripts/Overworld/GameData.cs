using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public LevelNode[] regionOneLvls;
    public LevelNode[] regionTwoLvls;
    public bool[] lvlStatusRegionOne = {true, false};
    public bool[] lvlStatusRegionTwo = {true, false};


    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameController");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Region1")
        {
            regionOneLvls = FindObjectsOfType<LevelNode>();
        }
        else if (SceneManager.GetActiveScene().name == "Region2")
        {
            regionTwoLvls = FindObjectsOfType<LevelNode>();
        }


    }

    public LevelNode[] GetLevels(int region)
    {
        if (region == 1)
        {
            return regionOneLvls;
        }
        else if (region == 2)
        {
            return regionTwoLvls;
        }
        return null;
    }

    public bool[] GetLevelCompletion(int region)
    {
        if (region == 1)
        {
            return lvlStatusRegionOne;
        }
        else if (region == 2)
        {
            return lvlStatusRegionTwo;
        }
        return null;
    }
}
