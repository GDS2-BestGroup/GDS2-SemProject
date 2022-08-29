using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private TextMeshProUGUI dialogueText;
    private Story currentStory;
    private static DialogueManager instance;

    private void Awake() 
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ContinueStory();
        }
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    public void EnterDialogueMode(TextAsset inkText)
    {
        currentStory = new Story(inkText.text);
        dialogueUI.SetActive(true);
    }

    public void ExitDialogueMode()
    {
        dialogueUI.SetActive(false);
        dialogueText.text = "";
    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
        } 
        else if (currentStory.currentChoices.Count > 0)
        {
            string choiceText = "";
            for (int i = 0; i < currentStory.currentChoices.Count; ++i) 
            {
                Choice choice = currentStory.currentChoices[i];
                choiceText += (i + 1) + ". " + choice.text + "\n";
            }
            dialogueText.text = choiceText;
        } 
        else
        {
            ExitDialogueMode();
        }
    }
}
