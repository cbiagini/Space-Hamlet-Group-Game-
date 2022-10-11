using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Ink.Runtime;

//https://www.youtube.com/watch?v=vY0Sk93YUhA <- tutorial used

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance; // singleton

    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public GameObject npcDiaPanel;
    public TextMeshProUGUI npcDiaPanelText;
    //[SerializeField] private Animator currentAnimator;
    public Story currentStory;
    public bool dialogueIsPlaying { get; private set; }

    public GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    [SerializeField] private GameObject player;
    [HideInInspector] public DialogueVarDictionary dialogueVarDictionary;
    [SerializeField] private TextAsset loadGlobalsJSON;

    private const string SPEAKER_TAG = "speaker";
    private const string EMOTION_TAG = "emotion";
    private const string ACTION_TAG = "goto";
    private const string ACTION_WHAT_TAG = "to";

    public KeyCode sceneLoadButton;
    public string sceneToLoad;
    //bool hasTriggered;
    private CollisionTriggerDialogue victoryEndingTriggered;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("There are more than one dialogue managers on the scene.");
        }
        instance = this;
        dialogueVarDictionary = new DialogueVarDictionary(loadGlobalsJSON);
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(dialogueIsPlaying);
        npcDiaPanel.SetActive(dialogueIsPlaying);
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        var Click = (Input.GetButtonDown("Fire1"));
        //var Space = Input.GetKeyDown(KeyCode.Space);
        if (!dialogueIsPlaying) return;
        if ((Click) && (currentStory.currentChoices.Count <= 0)) //CHANGE - had to make it like this so it wouldn't close out of the window when you're trying to pick something
            //CHANGE 2 - added a redundant variable to try to account for the continueStory in the dialogue mode entrance;
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode (TextAsset inkJSON, GameObject focus) //, Animator animator
    {
        Debug.Log("Entering dialogue mode..." + gameObject);
        currentStory = new Story(inkJSON.text);

        dialogueIsPlaying = true;
        dialoguePanel.SetActive(dialogueIsPlaying);

        dialogueVarDictionary.StartListening(currentStory);
        TimerScript.GetInstance().timerPaused = true;
        /*if (animator != null) {
            currentAnimator = animator;
        }*/
        //copied code from continue story WITHOUT the exit dialogue tag. test on launch to see if it works or if it does the same thing as on the editor!
        Debug.Log("Story of " + gameObject + " is starting...");
        dialogueText.text = currentStory.Continue();
        npcDiaPanelText.text = dialogueText.text; //redundant, so i don't have to rewrite this
        Debug.Log(dialogueText.text);
        DisplayChoices();
        InkTagHandler(currentStory.currentTags);
        dialogueIsPlaying = true;
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            Debug.Log("Story of " + gameObject + " is continuing...");
            dialogueText.text = currentStory.Continue();
            npcDiaPanelText.text = dialogueText.text; //redundant, so i don't have to rewrite this
            Debug.Log(dialogueText.text);
            DisplayChoices();
            InkTagHandler(currentStory.currentTags);
        }
        else
        {
            Debug.Log("Story is over. Exiting...");
            StartCoroutine(ExitDialogueMode());
        }
    }

    public void MakeChoice(int choiceIndex) // these are called by the onclick buttons.
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;
        //defensive check
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("Too many choices for too few game objects!");
        }
        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        } //and also hide all unused choices
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
    }

    private void InkTagHandler(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2) Debug.LogError("Tag has incorrect methodology: " + tag);
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    if (tagValue == "NPC") { 
                        npcDiaPanel.SetActive(dialogueIsPlaying);
                        dialoguePanel.SetActive(!dialogueIsPlaying);
                    } else //if (tagValue == "Player")
                    {
                        npcDiaPanel.SetActive(!dialogueIsPlaying);
                        dialoguePanel.SetActive(dialogueIsPlaying);
                    }
                    break;
                case EMOTION_TAG:
                    //if (currentAnimator != null) currentAnimator.SetTrigger(tagValue);
                    //else Debug.Log("Attempted to give " + tagValue + " to animator but there is no animator");
                    Debug.Log("Add emotion handler on characters for emotion: " + tagValue); //TODO https://www.youtube.com/watch?v=tVrxeUIEV9E
                    break;
                case ACTION_TAG:
                    SceneManager.LoadScene(tagValue); //
                    break;
                default:
                    Debug.LogWarning("Incorrectly handled tag! " + tag);
                    break;
            }

        }
    }

    public void SetVariableState(string variableName, Ink.Runtime.Object variableValue) //ink.runtime.object if using the commented out one
    {
        currentStory.variablesState["variableName"] = variableValue;

        if (dialogueVarDictionary.variables.ContainsKey(variableName))
        {
            dialogueVarDictionary.variables.Remove(variableName);
            dialogueVarDictionary.variables.Add(variableName, variableValue);
            Debug.Log("Changed " + variableName + " to " + variableValue);
        } else
        {
            Debug.LogWarning("No variable called " + variableName + "in dictionary");
        }
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.1f);
        dialogueVarDictionary.StopListening(currentStory);
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(dialogueIsPlaying);
        npcDiaPanel.SetActive(dialogueIsPlaying);
        dialogueText.text = "";
        TimerScript.GetInstance().timerPaused = false;

        if (dialogueIsPlaying == false && victoryEndingTriggered == true)
            SceneManager.LoadScene(sceneToLoad);
        //hasTriggered = true;
        
    }

}