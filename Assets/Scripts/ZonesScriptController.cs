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
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().SetLevel1(transform.parent);
            zoneCollider.isTrigger = false;
            if (transform.parent.name.Equals("Zone1"))
            {
                GameObject spawner = GameObject.Find("Spawner1");
                if (spawner.transform.childCount != 0)
                {
                    for (int ii = 0; ii < spawner.transform.childCount; ii++)
                    {
                        spawner.transform.GetChild(ii).GetComponent<SpawnerController>().GatilhoZona();
                        Invoke("Sleep", 5f);
                    }
                }
                else
                {
                    spawner.GetComponent<SpawnerController>().GatilhoZona();
                }
            }else if (transform.parent.name.Equals("Zone2"))
            {
                GameObject spawner = GameObject.Find("Spawner2");
                if (spawner.transform.childCount != 0)
                {
                    for (int ii = 0; ii < spawner.transform.childCount; ii++)
                    {
                        spawner.transform.GetChild(ii).GetComponent<SpawnerController>().GatilhoZona();
                        Invoke("Sleep", 5f);
                    }
                }
                else
                {
                    spawner.GetComponent<SpawnerController>().GatilhoZona();
                }
            }
            else
            {
                GameObject spawner = GameObject.Find("Spawner3");
                if (spawner.transform.childCount != 0)
                {
                    for (int ii = 0; ii < spawner.transform.childCount; ii++)
                    {
                        spawner.transform.GetChild(ii).GetComponent<SpawnerController>().GatilhoZona();
                        Invoke("Sleep", 5f);
                    }
                }
                else
                {
                    spawner.GetComponent<SpawnerController>().GatilhoZona();
                }
            }
        }
    }

    public void Sleep()
    {
        return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && transform.tag.Equals("zoneFinish"))
        {
            zoneCollider.isTrigger = false;
        }
    }
}
