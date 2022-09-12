using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.04f;
   
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;    
    
    [Header("Choices UI")]
    [SerializeField] private GameObject[] choiceButtons;
    private TextMeshProUGUI[] choicesText;

    [Header("Layout UI")]
    [SerializeField] private GameObject characterPortrait;
    [SerializeField] private GameObject caveBackground;
    
    private Story currentStory;
    private static DialogueManager instance;
    private GameData gd;

    private bool canContinueToNextLine = false;
    private bool completeLine = false;
    private Coroutine displayLineCoroutine;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string BACKGROUND_TAG = "background";

    [SerializeField] public EventManager em;
    private void Awake() 
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        choicesText = new TextMeshProUGUI[choiceButtons.Length];
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            choicesText[i] = choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>();
        }

        gd = GameObject.Find("GameData").GetComponent<GameData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canContinueToNextLine && Input.GetMouseButtonUp(0))
        {
            Debug.Log("Up in Update");
            ContinueStory();
        } else if (displayLineCoroutine != null && Input.GetMouseButtonUp(0))
        {
            completeLine = true;
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

        characterPortrait.SetActive(false);
        caveBackground.SetActive(false);

        em.StartListening(currentStory);

        ContinueStory();
    }

    public void ExitDialogueMode()
    {
        dialogueUI.SetActive(false);
        dialogueText.text = "";
        characterPortrait.SetActive(false);
        caveBackground.SetActive(false);

        // Level progression
        gd.GetLevelCompletion(gd.currentRegion)[gd.currentLevel-1] = false;
        if (gd.currentLevel <= gd.GetLevelCompletion(gd.currentRegion).Length -1)
        {
            gd.GetLevelCompletion(gd.currentRegion)[gd.currentLevel] = true;
        }
        SceneManager.LoadScene(gd.previousLevel);

        em.StopListening(currentStory);
    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }

            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));

            HandleTags(currentStory.currentTags);
        } 
        else if (currentStory.currentChoices.Count > 0)
        {
            dialogueText.text = "";
            DisplayChoices();
        }

        else
        {
            ExitDialogueMode();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (!(currentChoices.Count > choicesText.Length))
        {
            for (int i = 0; i < currentChoices.Count; i++)
            {
                choiceButtons[i].gameObject.SetActive(true);
                choicesText[i].text = currentChoices[i].text;
            }
        }
    }

    private void HideChoices()
    {
        foreach (GameObject choiceButton in choiceButtons)
        {
            choiceButton.SetActive(false);
        }
    }

    private void RemoveChoices()
    {
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            choicesText[i].text = "";
            choiceButtons[i].gameObject.SetActive(false);
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        if (canContinueToNextLine)
        {
            RemoveChoices();

            currentStory.ChooseChoiceIndex(choiceIndex);
        }
    }

    private IEnumerator DisplayLine (string line)
    {
        dialogueText.text = "";

        canContinueToNextLine = false;

        foreach( char letter in line.ToCharArray())
        {
            if (completeLine)
            {
                dialogueText.text = line;
                completeLine = false;
                break;
            }

            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        canContinueToNextLine = true;
    }

    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');

            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch(tagKey)
            {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG: 
                    characterPortrait.SetActive(true);
                    break;
                case BACKGROUND_TAG:
                    caveBackground.SetActive(true);
                    break;
            }
            
        }
    }
}
