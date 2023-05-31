using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerController : MonoBehaviour
{
    private int contabilizaStart = 0;


    public void setContabilizaStart()
    {
        contabilizaStart = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && contabilizaStart == 0)
        {
            contabilizaStart = 1;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().SetLevel1(transform.parent.name);
            transform.parent.GetComponent<ZonesScriptController>().ClosedZone();
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
            }
            else if (transform.parent.name.Equals("Zone2"))
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
}
