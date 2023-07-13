using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, 7.5f, 215.5f),
                                                     Mathf.Clamp(transform.position.y, -27.4f, 8.2f), transform.position.z);
            }
            else if (SceneManager.GetActiveScene().name.Equals("MLevelTree"))
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, 11.5f, 345.5f),
                                     Mathf.Clamp(transform.position.y,0f, 22f), transform.position.z);
            }
            else if (SceneManager.GetActiveScene().name.Equals("MLevelFour"))
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, 10f, 231.7f),
                         Mathf.Clamp(transform.position.y, 22f, 32f), transform.position.z);
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
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, 7.5f, 215.5f),
                                     Mathf.Clamp(transform.position.y, -97.8f, -62f), transform.position.z);
            }
            else if (SceneManager.GetActiveScene().name.Equals("MLevelTree"))
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, 11.5f, 345.5f),
                                     Mathf.Clamp(transform.position.y, -70f, -40f), transform.position.z);
            }
            else if (SceneManager.GetActiveScene().name.Equals("MLevelFour"))
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, 10f, 231.7f),
                         Mathf.Clamp(transform.position.y, -19.5f, -10f), transform.position.z);
            }
        }
    }
}
