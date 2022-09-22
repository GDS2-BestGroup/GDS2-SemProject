using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCanvas : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Dialogue");

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
        
    }

    private void OnLevelWasLoaded(int level)
    {
        canvas.worldCamera = Camera.main;
    }
}
