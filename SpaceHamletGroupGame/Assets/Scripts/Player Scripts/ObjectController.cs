using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public enum MoveType { Rotate, Walk, SingleAction }
    public bool isPossessed = false;
    public MoveType myMoveType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPossessed)
        {
            switch (myMoveType)
            {
                case MoveType.Rotate:
                    break;
                case MoveType.Walk:
                    break;
                case MoveType.SingleAction:
                    break;
                default:
                    break;
            }
        }
    }
}
