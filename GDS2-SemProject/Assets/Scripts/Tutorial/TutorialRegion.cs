using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRegion : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private LevelNode lvlOne;
    void Start()
    {
        if ( lvlOne && lvlOne.unlocked)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
