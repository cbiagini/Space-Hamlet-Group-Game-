using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    bool timerActive = false;
    bool failStateTriggered;
    float currentTime;
    public float startMinutes;
    public Text currentTimeText;
    public string Level1;
    public OnMouseDownVictory onMouseDownVictory;
    public Timer timer;







    // Start is called before the first frame update
    void Start()
    {
        currentTime = startMinutes * 60;
        //to store current time as seconds

        DontDestroyOnLoad(this.gameObject);

        StartTimer();
        //timer starts when scene loads
    }
    
   



    public void StartTimer()
    {
        timerActive = true;
    }
    //when level is loaded, timer will start

    // Update is called once per frame
    void Update()
    {
        if (timerActive == true){
            currentTime = currentTime - Time.deltaTime;
        }
        //check if timer is active.
        //If true, each frame decreases current time with number of milliseconds that passed since the last update. 

        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        //will store amount of time to convert seconds into minutes (as per what we are trying to do in this instance)


        currentTimeText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();

        //each frame text will be updated with the latest value

        if (currentTime <= 0 && failStateTriggered == false)
        {
            TriggerFailState();

        }
        //fail state is triggered when timer ends

        if (currentTime <= 0)
        {
            StopTimer();
        }
        //timer stops at 0

        if (currentTime <= 0)
        {
            SceneManager.LoadScene("FailScene");

            timer.gameObject.SetActive(false);
        }
        //timer ends and disappears so player gets sent to fail scene
    }
    
    
    public void StopTimer()
    {
        timerActive = false;
    }
    

    void TriggerFailState() 
    {
        print("Game Failed");
        failStateTriggered = true;
    }
    //testing to make sure fail works
}
