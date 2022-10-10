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
    
    [Header("Choices UI")]
    [SerializeField] private GameObject[] choiceButtons;
    private TextMeshProUGUI[] choicesText;

    [Header("Layout UI")]
    [SerializeField] private GameObject characterPortrait;
    [SerializeField] private GameObject background;

    [Header("Audio")]
    [SerializeField] private AudioSource audio;
    [SerializeField] private List<AudioClip> writingSFX;
    
    public bool dialogueRunning; // Bool to check whether or not dialogue is currently running

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
        dialogueRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canContinueToNextLine && Input.GetMouseButtonDown(0))
        {
            ContinueStory();
        } else if (displayLineCoroutine != null && Input.GetMouseButtonDown(0))
        {
            Debug.Log("completing line");
            completeLine = true;
        }

        Debug.Log("DialogueRunning is : " + dialogueRunning);
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
            ContinueStory();
        }
    }

    private IEnumerator DisplayLine (string line)
    {
        audio.clip = writingSFX[Random.Range(0, writingSFX.Count)];
        audio.Play();

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
        completeLine = false;

        audio.Stop();
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
            }
            
        }
    }
}
