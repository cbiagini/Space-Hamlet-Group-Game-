using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class TimerScript : MonoBehaviour
{
    private static TimerScript instance; // singleton
    bool timerActive = false;
    public bool timerPaused = false;
    bool startSequence = true;
    public GameObject player;
    public float currentTime;
    public float startMinutes;
    public TextAsset startTriggerJSON;
    public TextAsset endTriggerJSON;
    public TextMeshProUGUI currentTimeText;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("There are more than one timers on the scene.");
        }
        instance = this;
    }

    public static TimerScript GetInstance()
    {
        return instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startMinutes * 60;
        //to store current time as seconds

        //DontDestroyOnLoad(this.gameObject);
        StartTimer();
        //timer starts when scene loads
    }

    public void StartTimer()
    {
        timerActive = true;
        Debug.LogWarning("Timer is Starting...");
    }
    //when level is loaded, timer will start

    void Update()
    {
        if (startSequence)
        {
            DialogueManager.GetInstance().EnterDialogueMode(startTriggerJSON, player); //, GetComponentInChildren<Animator>()
            startSequence = false;
        }
        if (timerActive) currentTime -= Time.deltaTime;
        //check if timer is active. Of true, each frame decreases current time with number of milliseconds that passed since the last update. 
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        //will store amount of time to convert seconds into minutes (as per what we are trying to do in this instance)

        currentTimeText.text = time.ToString(@"mm"":""ss"); //time.Minutes.ToString() + ":" + time.Seconds.ToString();

        if ((currentTime <= 0) && timerActive)
        {
            timerActive = false;
            StopTimer();
            EndGame();

        } else {
            if (timerPaused && timerActive) //if timer is paused but timer is active
            {
                StopTimer(); //pause the timer
            }if (!timerPaused && !timerActive) //but if the timer ISN'T paused, but is still paused ingame
            {
                StartTimer(); //start it back up
            }
        }
    }


    public void StopTimer()
    {
        timerActive = false;
        Debug.LogWarning("Timer is Pausing...");
    }


    void EndGame()
    {
        Debug.LogWarning("Game Ending...");
        DialogueManager.GetInstance().EnterDialogueMode(endTriggerJSON, player); //, GetComponentInChildren<Animator>()
        SceneManager.LoadScene("Room1Fail"); //go to Room1Fail
    }
}