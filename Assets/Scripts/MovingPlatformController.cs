using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 endPosition;

    private float secs = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        startPosition = transform.position;
        endPosition = transform.position + new Vector3(20f, 0f, 0f);
    }
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(startPosition, endPosition, Mathf.SmoothStep(0f, 1f, Mathf.PingPong(Time.timeSinceLevelLoad / secs, 1f)));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerController>().GetStatusInFloor())
                collision.gameObject.transform.parent = transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
