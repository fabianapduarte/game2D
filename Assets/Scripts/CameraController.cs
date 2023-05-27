using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public static CameraController instance = null;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            instance.transform.position = gameObject.transform.position;
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

        // Update is called once per frame
        private void FixedUpdate()
    {
        Vector3 newPosition = player.position + new Vector3(0f, 0f, -10);
        transform.position = newPosition;
        if (SceneManager.GetActiveScene().name.Equals("LevelOne"))
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -3f, 223.5f),
                                 Mathf.Clamp(transform.position.y, 1.08f, 2.6f), transform.position.z);
        }
        else if (SceneManager.GetActiveScene().name.Equals("LevelTwo"))
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -7.25f, 215f),
                                                 Mathf.Clamp(transform.position.y, -32.6f, 9.5f), transform.position.z);
        }
        else if (SceneManager.GetActiveScene().name.Equals("LevelTree"))
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0.3f, 356.8f),
                                 Mathf.Clamp(transform.position.y, -2.8f, 21.9f), transform.position.z);
        }
        else if (SceneManager.GetActiveScene().name.Equals("LevelFour"))
        {

        }

    }
}
