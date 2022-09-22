using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
// using Ink.UnityIntegration;
using System.IO;

public class EventManager : MonoBehaviour
{
    [SerializeField] List<TextAsset> inkText;
    // [SerializeField] private InkFile globalsInkFile;
    [SerializeField] public TextAsset globalVars;
    private Dictionary<string, Ink.Runtime.Object> variables;
    private DialogueManager dm;
    private GameData gd;

    // Start is called before the first frame update

    void Awake() 
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        Compile();
        gd = GameObject.Find("Managers").GetComponent<GameData>();
        dm = GameObject.Find("Managers").GetComponent<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartEvent()
    {
        int randEvent = Random.Range(0, inkText.Count);
        dm.EnterDialogueMode(inkText[randEvent]);
        inkText.RemoveAt(randEvent);
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

        if (name.Equals("morale"))
        {
            gd.morale -= 150;
        }
    }

    private void VariablesToStory(Story story)
    {
        foreach(KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }

    public void Compile()
    {
        Story globalVariablesStory = new Story(globalVars.text);

        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach(string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            Debug.Log("Initialized global variable: " + name + " = " + value);
        }
    }
}
