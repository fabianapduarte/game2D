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

    private float minX5 = 9.12f, maxX5 = 277.1f, minY5 = 0.5f;

    private float minX6 = -9.25f, maxX6 = 53.2f;


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
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, 1.6f, 71.6f),
                     Mathf.Clamp(transform.position.y, 3.64f, 5.65f), transform.position.z);
        }
        else if (SceneManager.GetActiveScene().name.Equals("LevelOne"))
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
        else if (SceneManager.GetActiveScene().name.Equals("LevelFive"))
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX5, maxX5),
                         Mathf.Clamp(transform.position.y, minY5, 11.2f), transform.position.z);
            /*
            if (transform.position.x < 153f)
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX5, maxX5),
                         Mathf.Clamp(transform.position.y, 0.01f, 3.8f), transform.position.z);
            }
            else
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX5, maxX5),
                         Mathf.Clamp(transform.position.y, 3.57f, 3.8f), transform.position.z);
            }*/
        }
        else if (SceneManager.GetActiveScene().name.Equals("Acidron1"))
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX6, maxX6),
                    Mathf.Clamp(transform.position.y, 1.7f, 4.3f), transform.position.z);
        }
        else if (SceneManager.GetActiveScene().name.Equals("Acidron2"))
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX6, maxX6),
                    Mathf.Clamp(transform.position.y, 3.55f, 6.71f), transform.position.z);
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

    public void SetLevel2(string zone)
    {
        if (zone == "Zone1")
        {
            this.minX2 = 44.4f;
            this.maxX2 = 60.2f;
        }
        if (zone == "Zone2")
        {
            this.minX2 = 184.4f;
            this.maxX2 = 202.1f;
        }
    }
    public void SetLevel3(string zone)
    {
        if (zone == "Zone1")
        {
            this.minX3 = 27.5f;
            this.maxX3 = 36.7f;
        }
        if (zone == "Zone2")
        {
            this.minX3 = 119.6f;
            this.maxX3 = 134.7f;
        }
        if (zone == "Zone3")
        {
            this.minX3 = 248.8f;
            this.maxX3 = 286f;
        }
    }
    public void SetLevel4(string zone)
    {
        if (zone == "Zone1")
        {
            this.minX4 = 48.6f;
            this.maxX4 = 67.2f;
        }
        if (zone == "Zone2")
        {
            this.minX4 = 118.1f;
            this.maxX4 = 130.4f;
        }
        if (zone == "Zone3")
        {
            this.minX4 = 211.6f;
            this.maxX4 = 228f;
        }
    }

    public void SetLevel5(string zone)
    {
        if (zone == "Zone1")
        {
            this.minX5 = 80.9f;
            this.maxX5 = 127f;
        }
        if (zone == "Zone2")
        {
            this.minX5 = 173.8f;
            this.maxX5 = 203.3f;
            this.minY5 = 5.07f;
        }
        if (zone == "Zone3")
        {
            this.minX5 = 239.8f;
            this.maxX5 = 260f;
            this.minY5 = 5.07f;
        }
        if (zone == "EntradaCastle")
        {
            this.minY5 = 5.07f;
        }
    }

    public void SetLevel6(string zone)
    {
        if (zone == "Zone1")
        {
            this.minX6 = 16.75f;
            this.maxX6 = 25.87f;
        }
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

        if (level == 5)
        {
            minX5 = 9.12f;
            maxX5 = 277.1f;
            minY5 = 0.5f;
        }

        if(level == 6)
        {
            minX6 = -9.25f;
            maxX6 = 53.2f;
        }
    }
}

