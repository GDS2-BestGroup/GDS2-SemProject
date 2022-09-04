using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using Ink.UnityIntegration;
using System.IO;

public class EventManager : MonoBehaviour
{
    [SerializeField] TextAsset[] inkText;
    [SerializeField] private InkFile globalsInkFile;
    private Dictionary<string, Ink.Runtime.Object> variables;
    private GameData gd;

    // Start is called before the first frame update
    void Start()
    {

        Compile(globalsInkFile.filePath);
        // gd = GameObject.Find("GameData").GetComponent<GameData>();
        StartEvent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartEvent()
    {
        foreach(KeyValuePair<string, Ink.Runtime.Object> kp in variables)
        {
            Debug.Log(kp);
        }
        DialogueManager.GetInstance().EnterDialogueMode(inkText[0]);
        // DialogueManager.GetInstance().EnterDialogueMode(inkText[gd.currentRegion-1]);
    }

    public void StartListening(Story story)
    {
        VariablesToStory(story);

        story.variablesState.variableChangedEvent += VariableChanged;
    }

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }

    private void VariableChanged(string name, Ink.Runtime.Object value)
    {
        Debug.Log("Variable changed: " + name + " = " + value);

        if (variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value);
        }
    }

    private void VariablesToStory(Story story)
    {
        foreach(KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }

    public void Compile(string globalsFilePath)
    {
        string inkFileContents = File.ReadAllText(globalsFilePath);
        Ink.Compiler compiler = new Ink.Compiler(inkFileContents);
        Story globalVariablesStory = compiler.Compile();

        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach(string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            Debug.Log("Initialized global variable: " + name + " = " + value);
        }
    }
}
