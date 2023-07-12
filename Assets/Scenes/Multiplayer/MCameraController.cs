using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MCameraController : MonoBehaviour
{
    private Transform player1;
    private Transform player2;

    private float minX1 = -3f, maxX1 = 223.5f;

    private float minX2 = -10.5f, maxX2 = 217.2f;

    private float minX3 = 0.3f, maxX3 = 356.8f;

    private float minX4 = -2.9f, maxX4 = 239.1f;

    private float minX5 = 11f, maxX5 = 277.1f, minY5 = 1.33f;

    private float minX6 = -9.25f, maxX6 = 53.2f;

    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.Find("Jogador1").transform;
        player2 = GameObject.Find("Jogador2").transform;
    }

    void FixedUpdate() {
        if (transform.name.Equals("CameraPlayer1"))
        {
            Vector3 newPosition = player1.position + new Vector3(0f, 0f, -10);
            transform.position = newPosition;
            if (SceneManager.GetActiveScene().name.Equals("MLevelOne"))
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, 7.5f, 215.5f),
                                     Mathf.Clamp(transform.position.y, 0f, 0f), transform.position.z);
            }
            else if (SceneManager.GetActiveScene().name.Equals("MLevelTwo"))
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX2, maxX2),
                                                     Mathf.Clamp(transform.position.y, -32.4f, 9.1f), transform.position.z);
            }
            else if (SceneManager.GetActiveScene().name.Equals("MLevelTree"))
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX3, maxX3),
                                     Mathf.Clamp(transform.position.y, -2.8f, 21.9f), transform.position.z);
            }
            else if (SceneManager.GetActiveScene().name.Equals("MLevelFour"))
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX4, maxX4),
                         Mathf.Clamp(transform.position.y, 1.24f, 3.45f), transform.position.z);
            }
        }
        else
        {
            Vector3 newPosition = player2.position + new Vector3(0f, 0f, -10);
            transform.position = newPosition;
            if (SceneManager.GetActiveScene().name.Equals("MLevelOne"))
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, 7.5f, 215.5f),
                                     Mathf.Clamp(transform.position.y, -31f, -31f), transform.position.z);
            }
            else if (SceneManager.GetActiveScene().name.Equals("MLevelTwo"))
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX2, maxX2),
                                                     Mathf.Clamp(transform.position.y, -32.4f, 9.1f), transform.position.z);
            }
            else if (SceneManager.GetActiveScene().name.Equals("MLevelTree"))
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX3, maxX3),
                                     Mathf.Clamp(transform.position.y, -2.8f, 21.9f), transform.position.z);
            }
            else if (SceneManager.GetActiveScene().name.Equals("MLevelFour"))
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX4, maxX4),
                         Mathf.Clamp(transform.position.y, 1.24f, 3.45f), transform.position.z);
            }
        }
    }
}
