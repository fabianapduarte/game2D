using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public Transform player;

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector3 newPosition = player.position + new Vector3(0f, 0f, -10);
        transform.position = newPosition;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -3f, 179f),
                                             Mathf.Clamp(transform.position.y, 1.08f, 5.3f), transform.position.z);
    }
}
