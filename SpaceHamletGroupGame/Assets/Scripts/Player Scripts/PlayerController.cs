using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //PLAYER CONTROLLER SCRIPT: Checks for mouse clicks, log mouse movement, move player character to where mouse clicked
    //Using this tutorial for the mouse movement: https://gamedevbeginner.com/how-to-convert-the-mouse-position-to-world-space-in-unity-2d-3d/

    //Variables setup: Player speed, mouse click storage; if the player is able to possess something at the moment
    [SerializeField] private float playerSpd = 0.5f;
    [SerializeField] private Vector3 mousePosition;
    [SerializeField] private Vector3 mousePosOnScreen;
    [SerializeField] private Vector2 mousePlaceOnClick; //this is a private variable, but we want to read it on the unity editor, so we add SerializeField to show it on the unity GUI
    [SerializeField] private GameObject hoveringOver;
    [SerializeField] private bool canPossess = false;
    private Animator myAnimator;

    public enum MoveType { Rotate, Walk, SingleAction, Ghost }
    public MoveType myMoveType = MoveType.Walk;
    private PlayerFollowsPossessedObject playerFollows;

    void Start()
    {
        myAnimator = GetComponent<Animator>();

    }

    void Update()
    {
        //Mouse Movement
        mousePosition = Input.mousePosition; //get mouse position and store in a variable
        mousePosition.z = Camera.main.transform.position.z * (-1); //get the camera position and invert it so that the mouse position doesn't get read as the camera's position on the following function
        mousePosOnScreen = Camera.main.ScreenToWorldPoint(mousePosition); //transform the absolute mouse position into the relative position of the mouse as seen by the camera

        if ((canPossess)&&(hoveringOver!=null))
        {
            //add colorchange
            //hoveringOver.GetComponent<Material>().color = Color.red;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PossesserSwitch.GetInstance().changeControl(hoveringOver);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (PossesserSwitch.GetInstance().player != gameObject)
            {
                PossesserSwitch.GetInstance().changeControl(PossesserSwitch.GetInstance().player);
            }
        }

            switch (myMoveType)
        {
            case MoveType.Rotate:
                UpdateRotation();
                break;
            case MoveType.Walk:
                UpdateVelocity();
                break;
            case MoveType.SingleAction:
                SingleAction();
                break;
            case MoveType.Ghost:
                UpdateVelocity();
                break;
            default:
                Debug.LogError("No movement type selected!");
                break;
        }
    }

    void UpdateVelocity()
    {
        if (Input.GetMouseButton(0)) //if clicking left mouse button
        {
            mousePlaceOnClick = mousePosOnScreen; //store place where left mouse button was clicked
            transform.position = Vector2.Lerp(transform.position, mousePlaceOnClick, playerSpd*0.01f); //interpolate player towards stored place
        }
    }

    void UpdateRotation()
    {
        if (Input.GetMouseButton(0)) {
            float angle = Mathf.Atan2(mousePosOnScreen.y, mousePosOnScreen.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.back);
        }
    }

    void SingleAction()
    {
        myAnimator.SetTrigger("SingleAnim");
        //TODO add in extra code
    }

    private void OnTriggerStay(Collider other) //logging possible possession check
    {
        if (other.tag == "Object")
        {
            canPossess = true;
            hoveringOver = other.gameObject;
            //Debug.Log("Colliding with" + other + ". Can possess: " + canPossess);
            
        } else
        {
            //canPossess = false;
            //hoveringOver = null;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canPossess = false;
    }
}
