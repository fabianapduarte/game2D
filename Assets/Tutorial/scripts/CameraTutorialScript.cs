using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTutorialScript : MonoBehaviour
{
    public Transform player;

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector3 newPosition = player.position + new Vector3(0f, 0f, -10);
        transform.position = newPosition;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -1.98f, 65.7f),
                                             Mathf.Clamp(transform.position.y, 2.7f, 6.8f), transform.position.z);
    }
}
