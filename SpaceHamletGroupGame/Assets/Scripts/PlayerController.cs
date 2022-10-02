using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //PLAYER CONTROLLER SCRIPT: Checks for mouse clicks, log mouse movement, move player character to where mouse clicked
    //Using this tutorial for the mouse movement: https://gamedevbeginner.com/how-to-convert-the-mouse-position-to-world-space-in-unity-2d-3d/

    //Variables setup: Player speed, mouse click storage; if the player is able to possess something at the moment
    public float playerSpd;
    [SerializeField] private Vector3 mousePosition;
    [SerializeField] private Vector3 mousePosOnScreen;
    [SerializeField] private Vector2 mousePlaceOnClick; //this is a private variable, but we want to read it on the unity editor, so we add SerializeField to show it on the unity GUI
    public bool canPossess = false;
    void Start()
    {
        
    }

    void Update()
    {
        //Mouse Movement
        mousePosition = Input.mousePosition; //get mouse position and store in a variable
        mousePosition.z = Camera.main.transform.position.z * (-1); //get the camera position and invert it so that the mouse position doesn't get read as the camera's position on the following function
        mousePosOnScreen = Camera.main.ScreenToWorldPoint(mousePosition); //transform the absolute mouse position into the relative position of the mouse as seen by the camera
        UpdateVelocity(); //storing in a function so we can stop it easily if necessary (also to copy the platformer controller we already have)
    }

    void UpdateVelocity()
    {
        if (Input.GetMouseButton(0)) //if clicking left mouse button
        {
            mousePlaceOnClick = mousePosOnScreen; //store place where left mouse button was clicked
            transform.position = Vector2.Lerp(transform.position, mousePlaceOnClick, playerSpd*0.01f); //interpolate player towards stored place
        }
    }

    private void OnTriggerEnter(Collider other) //logging possible possession check
    {
        if (other.tag == "Object")
        {
            canPossess = true;
        } else
        {
            canPossess = false;
        }
        Debug.Log("Colliding with"+other+". Can possess: " + canPossess);
    }
}
