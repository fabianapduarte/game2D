using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonesScriptController : MonoBehaviour
{
    // Start is called before the first frame update
    private BoxCollider2D zoneCollider;
    void Start()
    {
        zoneCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && transform.tag.Equals("zoneInit"))
        {
            zoneCollider.isTrigger = false;
        }
    }
}
