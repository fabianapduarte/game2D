using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformController : MonoBehaviour
{
    public float fallTime = 1f;
    public float respawnTime = 9f;

    private Rigidbody2D rb;
    private Vector3 pos;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pos = transform.position;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Invoke("PlatFall", fallTime);
        }
    }

    void PlatFall()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        Invoke("Respawn", respawnTime);
    }

    void Respawn()
    {
        rb.bodyType = RigidbodyType2D.Static;
        transform.position = pos;
    }
}
