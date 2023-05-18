using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonesScriptController : MonoBehaviour
{
    // Start is called before the first frame update
    private BoxCollider2D zoneCollider;
    private Transform player;
    void Start()
    {
        zoneCollider = GetComponent<BoxCollider2D>();
        player = GameObject.Find("player").transform;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && transform.tag.Equals("zoneInit") && Mathf.Abs(player.position.x) > Mathf.Abs(transform.position.x))
        {
            zoneCollider.isTrigger = false;
            GameObject.Find("Spawner").GetComponent<SpawnerController>().GatilhoZona();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && transform.tag.Equals("zoneFinish"))
        {
            zoneCollider.isTrigger = false;
        }
    }
}
