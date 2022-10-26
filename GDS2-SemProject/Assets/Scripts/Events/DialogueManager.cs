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
    [SerializeField] private GameObject dialogueNameUI;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText; 
    [SerializeField] private Animator backgroundAnimator;
    [SerializeField] private Animator characterPortraitAnimator;
    [SerializeField] private Animator dialogueAnimator;
    private Color defaultTextColor = new Color(120/255f, 68/255f, 48/255f, 255/255f);
    
    [Header("Choices UI")]
    [SerializeField] private GameObject[] choiceButtons;
    private TextMeshProUGUI[] choicesText;
    private List<string> choicesInvalidity;
    private Color textErrorColor = new Color(191/255f, 53/255f, 0/255f, 255/255f);

    [Header("Layout UI")]
    [SerializeField] private GameObject characterPortrait;
    [SerializeField] private GameObject background;

    [Header("Error UI")]
    [SerializeField] private GameObject errorUI;
    [SerializeField] private TextMeshProUGUI errorText;
    [SerializeField] private GameObject continueText;
    [SerializeField] private GameObject errorButtons;

    [Header("Audio")]
    [SerializeField] private AudioSource audio;
    [SerializeField] private List<AudioClip> writingSFX;
    
    public bool dialogueRunning; // Bool to check whether or not dialogue is currently running
    public int awaitingErrorChoice;

    public Story currentStory;
    private static DialogueManager instance;
    private GameData gd;

    private bool canContinueToNextLine = false;
    private bool completeLine = false;
    private Coroutine displayLineCoroutine;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string BACKGROUND_TAG = "background";
    private const string GOLD_TAG = "gold";

    [SerializeField] public EventManager em;
    private void Awake() 
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        choicesText = new TextMeshProUGUI[choiceButtons.Length];
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            choicesText[i] = choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>();
        }

        gd = GameObject.Find("Managers").GetComponent<GameData>();

        choicesInvalidity = new List<string>();
        dialogueRunning = false;
        awaitingErrorChoice = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (errorUI.activeInHierarchy && awaitingErrorChoice < 0)
            {
                errorUI.SetActive(false);
                continueText.SetActive(false);
            } else if (canContinueToNextLine)
            {
                ContinueStory();
            } else if (displayLineCoroutine != null)
            {
                Debug.Log("Completing Line");
                completeLine = true;
            }
        }
    }

    public void EnterDialogueMode(TextAsset inkText)
    {
        currentStory = new Story(inkText.text);
        
        background.SetActive(false);
        characterPortrait.SetActive(false);
        dialogueUI.SetActive(true);
        dialogueAnimator.Play("appear");

        em.StartListening(currentStory);
        dialogueRunning = true;

        ContinueStory();
    }

    public void ExitDialogueMode()
    {
        canContinueToNextLine = false;
        completeLine = false;

        dialogueUI.SetActive(false);
        dialogueNameUI.SetActive(false);
        dialogueText.text = "";
        characterPortrait.SetActive(false);
        background.SetActive(false);

        errorButtons.SetActive(false);
        choicesInvalidity.Clear();
        foreach(TextMeshProUGUI text in choicesText)
        {
            text.color = defaultTextColor;
        }

        Debug.Log(gd.currentLevel);
        
        // Level progression
        gd.GetLevelCompletion(gd.currentRegion)[gd.currentLevel-1] = false;
        Debug.Log(gd.GetLevels(gd.currentRegion)[gd.currentLevel - 1].name);
        LevelNode currentlvl = null;
        LevelNode[] levels = gd.GetLevels(gd.currentRegion); 
        foreach (LevelNode level in levels)
        {
            if (level.name == "LvlNode" + gd.currentRegion + "." + (gd.currentLevel))
            {
                level.LevelLock();
                currentlvl = level;
            }
        }

        if (currentlvl && currentlvl.GetNeighbours().Length > 0)
        {
            foreach(LevelNode level in currentlvl.GetNeighbours())
            {
                gd.GetLevelCompletion(gd.currentRegion)[(int)level.levelNum - 1] = true;
                level.LevelUnlock();
            }
        }

        em.StopListening(currentStory);
        dialogueRunning = false;
    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
                completeLine = false;
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
            backgroundAnimator.SetTrigger("end_dialogue");
            characterPortraitAnimator.SetTrigger("end_dialogue");
            dialogueAnimator.SetTrigger("end_dialogue");
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
                choicesInvalidity.Add(CheckChoiceValidity(i));

                choicesText[i].text = currentChoices[i].text;
                if (choicesInvalidity[i] != "")
                {
                    choicesText[i].color = textErrorColor;
                }
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

    public void CheckChoice (int choiceIndex)
    {
        if (choicesInvalidity[choiceIndex] == "")
        {
            MakeChoice(choiceIndex);
        } else 
        { 
            errorText.text = BuildChoiceErrorText(choiceIndex);
            errorUI.SetActive(true);
        }
    }

    private string BuildChoiceErrorText(int choiceIndex)
    {
        bool moraleReason = false;
        string errorText = "";

        string[] errors = choicesInvalidity[choiceIndex].Split(",", System.StringSplitOptions.RemoveEmptyEntries);

        foreach(string error in errors)
        {
            if (error == "morale")
            {
                moraleReason = true;
            }
        }

        int errorsLength = errors.Length;
        Debug.Log("Errors Length = " + errorsLength);
        if (moraleReason)
        {
            errorsLength -= 1;
        }

        if (errorsLength > 0)
        {
            for(int i = 0; i < errorsLength; i++)
            {
                if (i == 0)
                {
                    errorText += "You don't have enough ";
                }

                errorText += errors[i];
                if (errors.Length - i == 2)
                {
                    errorText += " and ";
                } else if (errors.Length -i > 2)
                {
                    errorText += ", ";
                }
            }

            errorText += " to make this choice.\n\n";

            continueText.SetActive(true);
        } else if (moraleReason)
        {
            errorText = "Choosing this option will cause you to lose the game. Are you sure?";
            awaitingErrorChoice = choiceIndex;
            errorButtons.SetActive(true);
        }

        return errorText;
    }

    public void MakeChoice(int choiceIndex)
    {
        if (canContinueToNextLine && choiceIndex >= 0)
        {
            RemoveChoices();

            currentStory.ChooseChoiceIndex(choiceIndex);
            ContinueStory();
        } else if (choiceIndex == -1 && awaitingErrorChoice >= 0)
        {
            RemoveChoices();

            currentStory.ChooseChoiceIndex(awaitingErrorChoice);
            ContinueStory();
        }
    }

    private IEnumerator DisplayLine (string line)
    {
        audio.clip = writingSFX[Random.Range(0, writingSFX.Count)];
        audio.Play();

        dialogueText.text = "";

        canContinueToNextLine = false;

        string tag = "";

        for (int i = 0; i < line.Length; i++)
        {
            if (completeLine)
            {
                dialogueText.text = line;
                completeLine = false;
                break;
            }

            if (line[i] == '<') 
            {

                for (int j = i; j < line.Length; j++)
                {
                    tag += line[j];
                    if (line[j] == '>')
                    {
                        tag += line[j+1];
                        i = j + 1;
                        break;
                    }
                }

                dialogueText.text += tag;
                tag = "";
            } else
            {
                dialogueText.text += line[i];
                yield return new WaitForSeconds(typingSpeed);
            }  
        }

        canContinueToNextLine = true;
        completeLine = false;

        audio.Stop();
    }

    // Returns the reason for failure as a string e.g. "Gold", "GoldMorale"
    public string CheckChoiceValidity (int choiceIndex)
    {
        string reason = "";
        foreach (string tag in currentStory.currentTags)
        {
            string[] splitTag = tag.Split(':');

            string tagChoice = splitTag[0].Trim();
            string tagKey = splitTag[1].Trim();
            string tagValue = splitTag[2].Trim();

            if (int.Parse(tagChoice) == choiceIndex)
            {
                switch(tagKey)
                {
                    case "gold":
                        if (!gd.CheckCost(-int.Parse(tagValue)))
                        {
                            
                            reason += "gold,";
                        }
                        break;
                    case "morale":
                        if (gd.morale - (-int.Parse(tagValue)) <= 0)
                        {
                            reason += "morale,";
                        }
                        break;
                }
            }
        }

        return reason;
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
                    if (tagValue != "")
                    {
                        dialogueNameUI.SetActive(true);
                        displayNameText.text = tagValue;
                    }
                    break;
                case PORTRAIT_TAG:
                    characterPortrait.SetActive(true);
                    characterPortraitAnimator.Play(tagValue);
                    break;
                case BACKGROUND_TAG:
                    background.SetActive(true);
                    backgroundAnimator.Play(tagValue);
                    break;
                default:
                    break;
            }
            
        }
    }
}
