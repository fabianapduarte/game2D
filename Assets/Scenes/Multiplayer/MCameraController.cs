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
        player1 = GameObject.Find("Jogador1").transform;
        player2 = GameObject.Find("Jogador2").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.name.Equals("CameraPlayer1"))
        {
            Vector3 newPosition = player1.position + new Vector3(0f, 0f, -10);
            transform.position = newPosition;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, 6.5f, 215.5f),
                     Mathf.Clamp(transform.position.y, 0f, 0f), transform.position.z);
        }
        else
        {
            Vector3 newPosition = player2.position + new Vector3(0f, 0f, -10);
            transform.position = newPosition;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, 6.5f, 215.5f),
                     Mathf.Clamp(transform.position.y, -31f, -31f), transform.position.z);
        }
    }
}
