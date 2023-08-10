using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextController : MonoBehaviour
{
    float speed = 0.4f;

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < 1300f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + speed, 0f);
        }
        else
        {
            Invoke("ResetCredits", 1f);
        }
    }
 
    public void ResetCredits()
    {
        transform.position = new Vector3(transform.position.x, -800f, 0f);
    }
}
