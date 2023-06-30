using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCameraController : MonoBehaviour
{
    private Transform player1;
    private Transform player2;
    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.Find("Player1").transform;
        player2 = GameObject.Find("Player2").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.name.Equals("CameraPlayer1"))
        {
            Vector3 newPosition = player1.position + new Vector3(0f, 0f, -10);
            transform.position = newPosition;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -24.43768f, 181.1f),
                     Mathf.Clamp(transform.position.y, 3.64f, 5.65f), transform.position.z);
        }
        else
        {
            Vector3 newPosition = player2.position + new Vector3(0f, 0f, -10);
            transform.position = newPosition;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -45.8f, 160f),
                     Mathf.Clamp(transform.position.y, 3.64f, 5.65f), transform.position.z);
        }
    }
}
