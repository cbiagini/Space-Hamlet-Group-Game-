using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnMouseDownTimerStop : MonoBehaviour
{
    public Timer timer;



    private void OnMouseDown()
    {
        timer.StopTimer();

        SceneManager.LoadScene("Room1Victory");

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

    }
}
