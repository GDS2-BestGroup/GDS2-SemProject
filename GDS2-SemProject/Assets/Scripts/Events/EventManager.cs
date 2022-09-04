using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class EventManager : MonoBehaviour
{
    [SerializeField] TextAsset inkText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartEvent()
    {
        DialogueManager.GetInstance().EnterDialogueMode(inkText);
    }

    public void StartListening(Story story)
    {
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }


    private void VariableChanged(string name, Ink.Runtime.Object value)
    {
        Debug.Log("Variable changed: " + name + " = " + value);
    }
}
