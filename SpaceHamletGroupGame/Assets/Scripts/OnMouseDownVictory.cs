using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnMouseDownVictory : MonoBehaviour
{
    //public Timer timer;
    public bool clickTriggered;

    private void OnMouseDown()
    {
        ClickTriggered();
    }
    // Start is called before the first frame update
    void Start()
    {
        //timer = FindObjectOfType<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (clickTriggered) {
            clickTriggered = false; //immediately change it back so it's clickable again
        } else
        {
            //do whatever but not
        }
    }

    void ClickTriggered()
    {
        print("Object Clicked!");
        clickTriggered = true;
    }

}
