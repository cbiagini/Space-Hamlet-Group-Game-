using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDialogueOnTriggerEnter : MonoBehaviour
{
    [HideInInspector]
    public DialogueManager dialogueManager;
    public string dialogueLine;

    //when entering the collision, the UI Canvas is enabled
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            dialogueManager.ShowDialogue(this);

        }
    }

    //when exiting the collision, the UI Canvas is disabled
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            dialogueManager.HideDialogue(this);

        }
    }
}
