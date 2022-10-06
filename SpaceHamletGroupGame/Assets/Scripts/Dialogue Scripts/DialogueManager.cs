using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    /*This class enables and disables a GameObject called dialogueBox, which will probably be a Unity UI Object like a Canvas
    //It also can be given data from any ShowDialogueOnTriggerEnter components in the scene to update its child text component
    to accomplish this, we will give every ShowDialogueOnTriggerEnter component a reference to this script, and there should only be one in the scene!
    */

    public GameObject dialogueBox;
    private Text dialogueText;

    public ShowDialogueOnTriggerEnter currentDialogue;
    
    // Awake is called before the first frame update
    private void Awake()
    {
        dialogueText = dialogueBox.GetComponentInChildren<Text>();
        if(dialogueText == null)
        {
            Debug.LogError("DialogueManager could not find Text component");
        }

        // We are compiling an array, a collection, of all of these dialogueTrigger components in the scene before the game starts
        ShowDialogueOnTriggerEnter[] dialogueTriggerArray = FindObjectsOfType<ShowDialogueOnTriggerEnter>();

        //we then cycle through every single element of that array and making sure this component is hooked up to the dialogueManager variable in each of those triggers
        foreach (ShowDialogueOnTriggerEnter dialogueTrigger in dialogueTriggerArray)
        {
            dialogueTrigger.dialogueManager = this;
        }

        dialogueBox.SetActive(false);
    }

    /*This method is called from the ShowDialogueOnTriggerEnter script, and that component tells the dialogueManager what text to display and then makes the dialogue box appear
    //we are checking whether the dialogueBox is already visible and whether the currentDialogue is the same as the newDialogue so that, 
    if the dialogueBox had a an animation that plays when it is made active  we can avoid that being triggered redundantly
    */
    public void ShowDialogue(ShowDialogueOnTriggerEnter newDialogue)
    {
        if (currentDialogue != newDialogue && dialogueBox.activeSelf == false)
        {
            dialogueBox.SetActive(true);
            dialogueText.text = newDialogue.dialogueLine;
            currentDialogue = newDialogue;
        }
    }

    /*The HideDialogue method is also called from the ShowDialogueOnTriggerEnter script, and it checks if the dialogueBox is currentlyVisible and that the current dialogue
    //matches the trigger that is instigating this HideDialogue method. In case multiple dialogueTriggers overlap, this will prevent one trigger from prematurely closing
    a neigboring trigger
    */
    public void HideDialogue(ShowDialogueOnTriggerEnter instigator)
    {
        if(currentDialogue == instigator && dialogueBox.activeSelf == true)
        {
            dialogueBox.SetActive(false);
            currentDialogue = null;
        }
    }
}
