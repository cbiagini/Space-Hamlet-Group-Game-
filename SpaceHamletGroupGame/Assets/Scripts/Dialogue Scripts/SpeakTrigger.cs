using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject visualCue;
    public GameObject focusVisual;
    //public Animator myAnimator;
    //public AudioSource mySpeech;
    public TextAsset inkJSON;
    private bool playerInRange;
    private bool playerClicking;

    private void Awake()
    {
        if (focusVisual == null) focusVisual = gameObject; //sets to self if not set
        //visualCue = transform.GetChild(0).gameObject;
        //myAnimator = GetComponentInChildren<Animator>();
        playerInRange = false;
        playerClicking = false;
        //visualCue.SetActive(false);
    }

    private void Update()
    {
        playerClicking = Input.GetKeyDown(KeyCode.Space);//Input.GetButtonDown("Fire1"); //fire1 = left click
        //if (visualCue.activeInHierarchy != playerInRange) visualCue.SetActive(playerInRange);
        if ((playerInRange) && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            if (playerClicking)
            {
                Debug.Log("Attempting to speak with " + gameObject);
                /*if (mySpeech != null)
                {
                    mySpeech.pitch = Random.Range(0.9f, 1.2f);
                    mySpeech.Play();
                }*/
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON, focusVisual);
            }
        }
    }

    private void OnTriggerEnter(Collider other) //this trigger collider is on child(1)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = true;
            Debug.Log("In range of " + gameObject);
        }
    }

    private void OnTriggerExit(Collider other) //this trigger collider is on child(1)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
            Debug.Log("Exited range of " + gameObject);
        }
    }
}
