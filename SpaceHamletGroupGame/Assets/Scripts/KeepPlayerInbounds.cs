using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepPlayerInbounds : MonoBehaviour
{
    public Collider[] bounds;
    // Start is called before the first frame update
    void Start()
    {
        bounds = GetComponentsInChildren<Collider>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("its working");
        }
    }
}
