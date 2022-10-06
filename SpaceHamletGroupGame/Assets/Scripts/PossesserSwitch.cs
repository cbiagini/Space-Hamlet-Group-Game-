using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossesserSwitch : MonoBehaviour
{
    private static PossesserSwitch instance;
    public GameObject player;
    public List<GameObject> controllableObjects = new List<GameObject>(); //https://stackoverflow.com/questions/15677194/check-a-value-exist-in-array-in-unity-3d

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("There are more than one PossesserSwitch on the scene. Kill the spare");
        }
        instance = this;
        player = GameObject.Find("Player");
    }

    public static PossesserSwitch GetInstance()
    {
        return instance;
    }

    public void changeControl(GameObject control)
    {
        Debug.Log("Switching with " + control + "...");
        if (!controllableObjects.Contains(control))
        {
            Debug.LogError("The object is not a controllable object on the array! (Did you forget to add it?)"); //safety in case the object isn't in the interactables list
            return; //don't try to deactivate anything, leave code
        }
        else
        {
            foreach (GameObject controllable in controllableObjects)
            {
                controllable.GetComponent<PlayerController>().enabled = false; //turn off control for all player controllers
            }
            player.GetComponentInChildren<Renderer>().enabled = false;
            GetComponent<PlayerFollowsPossessedObject>().enabled = true;
            GetComponent<PlayerFollowsPossessedObject>().focus = control;
            control.GetComponent<PlayerController>().enabled = true; //...except for the one that was selected
            control.GetComponentInChildren<Renderer>().enabled = true;
        }
    }

}
