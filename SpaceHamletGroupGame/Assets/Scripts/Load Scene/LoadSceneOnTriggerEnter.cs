using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnTriggerEnter : MonoBehaviour
{
    public KeyCode sceneLoadButton;
    public string sceneToLoad;
    bool hasTriggered;


    void OnTriggerEnter(Collider other)
    {
        //when we detect a collision, if the object we collided with is the player
        //reload the level
        //this could be used for an enemy, or a hazard like a cactus
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(sceneToLoad);
            Debug.Log("Loading new scene: "+sceneToLoad);
        }
    }
}
