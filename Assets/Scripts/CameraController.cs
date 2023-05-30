using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    private Transform player;

    private float minX1 = -3f, maxX1 = 223.5f;

    private float minX2 = -10.5f, maxX2 = 217.2f;

    private float minX3 = 0.3f, maxX3 = 356.8f;

    private float minX4 = -2.9f, maxX4 = 239.1f;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

        // Update is called once per frame
        private void FixedUpdate()
    {
        Vector3 newPosition = player.position + new Vector3(0f, 0f, -10);
        transform.position = newPosition;
        if (SceneManager.GetActiveScene().name.Equals("Tutorial"))
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, 1.6f, 63.68f),
                     Mathf.Clamp(transform.position.y, 3.64f, 5.65f), transform.position.z);
        }
        if (SceneManager.GetActiveScene().name.Equals("LevelOne"))
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX1, maxX1),
                                 Mathf.Clamp(transform.position.y, 1.08f, 2.6f), transform.position.z);
        }
        else if (SceneManager.GetActiveScene().name.Equals("LevelTwo"))
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX2, maxX2),
                                                 Mathf.Clamp(transform.position.y, -32.4f, 9.1f), transform.position.z);
        }
        else if (SceneManager.GetActiveScene().name.Equals("LevelTree"))
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX3, maxX3),
                                 Mathf.Clamp(transform.position.y, -2.8f, 21.9f), transform.position.z);
        }
        else if (SceneManager.GetActiveScene().name.Equals("LevelFour"))
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX4, maxX4),
                     Mathf.Clamp(transform.position.y, 1.24f, 3.45f), transform.position.z);
        }
    }

    public void SetLevel1(Transform zone)
    {
        minX1 = zone.GetChild(0).position.x;
        maxX1 = zone.GetChild(1).position.x;    
    }

    public void SetLevel2(Transform zone)
    {
        minX2 = zone.GetChild(0).position.x;
        maxX2 = zone.GetChild(1).position.x;
    }
    public void SetLevel3(Transform zone)
    {
        minX3 = zone.GetChild(0).position.x;
        maxX3 = zone.GetChild(1).position.x;
    }
    public void SetLevel4(Transform zone)
    {
        minX4 = zone.GetChild(0).position.x;
        maxX4 = zone.GetChild(1).position.x;
    }

    public void ResetCoordX(int level)
    {
        if(level == 1)
        {
            minX1 = -3f;
            maxX1 = 223.5f;
        }
        if(level == 2)
        {
            minX2 = -10.5f;
            maxX2 = 217.2f;
        }
        if(level == 3)
        {
            minX3 = 0.3f;
            maxX3 = 356.8f;
        }

        if(level == 4)
        {
            minX4 = -2.9f;
            maxX4 = 239.1f;
        }
    }
}

