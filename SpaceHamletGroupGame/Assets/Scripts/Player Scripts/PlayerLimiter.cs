using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLimiter : MonoBehaviour
{
    public int barrierSize;
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > barrierSize)
        {
            gameObject.transform.position = new Vector2(barrierSize, transform.position.y);
        }

        if (transform.position.x < (barrierSize*-1))
        {
            gameObject.transform.position = new Vector2((barrierSize*-1), transform.position.y);
        }

        if (transform.position.y > barrierSize)
        {
            gameObject.transform.position = new Vector2(transform.position.x, barrierSize);
        }

        if (transform.position.y < (barrierSize * -1))
        {
            gameObject.transform.position = new Vector2(transform.position.x, (barrierSize * -1));
        }



    }
}
