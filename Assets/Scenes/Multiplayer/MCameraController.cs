using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCameraController : MonoBehaviour
{
    private Transform player1;
    private Transform player2;

    private float minX1 = -3f, maxX1 = 223.5f;

    private float minX2 = -10.5f, maxX2 = 217.2f;

    private float minX3 = 0.3f, maxX3 = 356.8f;

    private float minX4 = -2.9f, maxX4 = 239.1f;

    private float minX5 = 9.12f, maxX5 = 277.1f, minY5 = 0.5f;

    private float minX6 = -9.25f, maxX6 = 53.2f;
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
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, 6.5f, 224f),
                     Mathf.Clamp(transform.position.y, 0f, 0f), transform.position.z);
        }
        else
        {
            Vector3 newPosition = player2.position + new Vector3(0f, 0f, -10);
            transform.position = newPosition;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, 6.5f, 224f),
                     Mathf.Clamp(transform.position.y, -83.7f, -83.7f), transform.position.z);
        }
    }

    public void SetLevel1(string zone)
    {
        if (zone == "Zone1")
        {
            this.minX1 = 26.7f;
            this.maxX1 = 37.8f;
        }
        if (zone == "Zone2")
        {
            this.minX1 = 119.1f;
            this.maxX1 = 127f;
        }
        if (zone == "Zone3")
        {
            this.minX1 = 202.3f;
            this.maxX1 = 211.7f;
        }
    }
}
