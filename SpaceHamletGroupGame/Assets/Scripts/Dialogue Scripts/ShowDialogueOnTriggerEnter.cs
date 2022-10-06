using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDialogueOnTriggerEnter : MonoBehaviour
{
    [HideInInspector]
    public DialogueManager dialogueManager;
    public string dialogueLine;
    public GameObject whoEnters;
    public bool playOnce = false;
    private bool played = false;

    //when entering the collision, the UI Canvas is enabled
    private void OnTriggerEnter(Collider other)
    {
        //now you can make it only play the dialogue once!
        if (played == false)
        {
            if ((whoEnters != null) && (other.gameObject == whoEnters)) //if you've set a separate gameobject to collide and it is colliding
            {
                dialogueManager.ShowDialogue(this); //show the dialogue
            }
            //this is the old script; it still works.
            if (other.gameObject.tag == "Player") 
            {
                dialogueManager.ShowDialogue(this);

            }
        }
    }

    //when exiting the collision, the UI Canvas is disabled
    private void OnTriggerExit(Collider other)
    {
        if ((whoEnters != null) && (other.gameObject == whoEnters)) //if you've set a separate gameobject to collide and it is colliding
        {
            dialogueManager.HideDialogue(this);
        }
        if (other.gameObject.tag == "Player")
        {
            dialogueManager.HideDialogue(this);
        }
        //if the dialogue is set to play once, disable its ability to play again
        if (playOnce == true)
        {
            played = true;
        }
    }
}
