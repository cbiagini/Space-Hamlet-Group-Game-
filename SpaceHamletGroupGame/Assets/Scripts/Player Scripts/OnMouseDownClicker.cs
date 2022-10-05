using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnMouseDownClicker: MonoBehaviour
{
    public bool hoveringOver;
    public bool mouseOver;
    public bool clickTriggered;

    // Start is called before the first frame update
    void Start()
    {
        //timer = FindObjectOfType<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseOver) {
            if (Input.GetMouseButtonDown(0)) ClickTriggered(); //this is the same as the onmousedown function but idk why that one isn't working
            //clickTriggered = false; //immediately change it back so it's clickable again
        } else
        {
            //do whatever but not
        }
    }

    private void OnTriggerStay(Collider other) //check if colliding with triggers. since the only triggers on scene are the player i didn't add any tags here but it might be needed later
    {
        if (other.tag == "Player") hoveringOver = true;
        if ((other.tag == "EditorOnly")&&(hoveringOver)) mouseOver = true;
    }

    private void OnTriggerExit(Collider other) //check if player left collider
    {
        Debug.Log("Stopped touching " + other);
        hoveringOver = false;
        mouseOver = false;
        clickTriggered = false;
    }

    /*private void OnMouseDown()
    {
        if (hoveringOver == true)
        {
            ClickTriggered();
        }
    }*/

    void ClickTriggered()
    {
        print("Object Clicked!");
        clickTriggered = true;
    }

}
