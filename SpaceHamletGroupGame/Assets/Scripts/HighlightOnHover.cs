using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightOnHover : MonoBehaviour
{
    public Color highlightColor = Color.red;
    public Color previousColor = Color.white;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "EditorOnly") GetComponentInChildren<SpriteRenderer>().color = highlightColor;
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "EditorOnly") GetComponentInChildren<SpriteRenderer>().color = previousColor;
    }
}
