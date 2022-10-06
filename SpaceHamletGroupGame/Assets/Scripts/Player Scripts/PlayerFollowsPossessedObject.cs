using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowsPossessedObject : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject focus;

    void Update()
    {
        if (focus != null) transform.position = focus.transform.position;
        if (Input.GetKeyDown(KeyCode.Space)) enabled = false;
    }
}
