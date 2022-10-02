using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnMouseDownVictory : MonoBehaviour
{
    public Timer timer;

    bool clickTriggered;

    private void OnMouseDown()
    {
        timer.StopTimer();

        SceneManager.LoadScene("VictoryScreen");

        timer.gameObject.SetActive(false);


    }



    // Start is called before the first frame update
    void Start()
    {
        timer = FindObjectOfType<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (clickTriggered == false)
        {
            ClickTriggered();
        }
        void ClickTriggered()
        {
            print("Object Clicked!");
            clickTriggered = true;
        }
        //testing to make sure object has been clicked!
    }
}
