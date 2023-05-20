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
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 3.78f, 57.3f),
                                             Mathf.Clamp(transform.position.y, 3.16f, 5f), transform.position.z);
    }
}
