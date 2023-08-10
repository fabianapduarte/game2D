using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextController : MonoBehaviour
{
    float speed = 1.6f;

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < 1000f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + speed, 0f);
        }
        else
        {
            Invoke("ResetCredits", 1f);
        }
    }
 
    private void ResetCredits()
    {
        transform.position = new Vector3(transform.position.x, -900f, 0f);
    }
}
