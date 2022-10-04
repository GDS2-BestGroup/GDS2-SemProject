using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private Canvas pauseCanvas;
    private GameData gd;
    private bool pauseAllowed;
    // Start is called before the first frame update
    void Start()
    {
        pauseCanvas = GameObject.Find("PauseUI").GetComponentInChildren<Canvas>(true);
        pauseCanvas.gameObject.SetActive(false);
        gd = GameObject.Find("Managers").GetComponent<GameData>();
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
    }

    public void Pause()
    {
        pauseCanvas.gameObject.SetActive(true);
        gd.paused = true;
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
    }

}
