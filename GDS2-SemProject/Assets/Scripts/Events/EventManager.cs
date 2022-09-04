using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] TextAsset inkText;

    // Start is called before the first frame update
    void Start()
    {
        DialogueManager.GetInstance().EnterDialogueMode(inkText);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartEvent()
    {
        
    }
}
