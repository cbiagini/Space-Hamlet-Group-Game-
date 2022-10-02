using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    [SerializeField] private Vector3 mousePosition;
    [SerializeField] private Vector3 mousePosOnScreen;

    void Update()
    {
        mousePosition = Input.mousePosition; //get mouse position and store in a variable
        mousePosition.z = Camera.main.transform.position.z * (-1); //get the camera position and invert it so that the mouse position doesn't get read as the camera's position on the following function
        mousePosOnScreen = Camera.main.ScreenToWorldPoint(mousePosition); //transform the absolute mouse position into the relative position of the mouse as seen by the camera
        transform.position = Vector2.MoveTowards(transform.position, mousePosOnScreen, 1000);
    }
}
