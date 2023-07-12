using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MDetectPlayerController : MonoBehaviour
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
            GameObject.Find("AudioController").GetComponent<AudioController>().BattleSoundStart();
            contabilizaStart = 1;
            transform.parent.GetComponent<ZonesScriptController>().ClosedZone();
            if (transform.parent.name.Equals("Zone1"))
            {
                GameObject spawner = null;
                if (transform.parent.parent.name.Equals("LevelP1"))
                {
                    spawner = GameObject.Find("Spawner1P1");
                }
                else if (transform.parent.parent.name.Equals("LevelP2"))
                {
                    spawner = GameObject.Find("Spawner1P2");
                }
                if (spawner.transform.childCount != 0)
                {
                    for (int ii = 0; ii < spawner.transform.childCount; ii++)
                    {
                        spawner.transform.GetChild(ii).GetComponent<SpawnerControllerM>().GatilhoZona();
                    }
                    Invoke("Sleep", 5f);
                }
                else
                {
                   spawner.GetComponent<SpawnerControllerM>().GatilhoZona();
                }
            }
            else if (transform.parent.name.Equals("Zone2"))
            {
                GameObject spawner = null;
                if (transform.parent.parent.name.Equals("LevelP1"))
                {
                    spawner = GameObject.Find("Spawner2P1");
                }
                else if (transform.parent.parent.name.Equals("LevelP2"))
                {
                    spawner = GameObject.Find("Spawner2P2");
                }
                if (spawner.transform.childCount != 0)
                {
                    for (int ii = 0; ii < spawner.transform.childCount; ii++)
                    {
                        spawner.transform.GetChild(ii).GetComponent<SpawnerControllerM>().GatilhoZona();
                    }
                    Invoke("Sleep", 5f);
                }
                else
                {
                    spawner.GetComponent<SpawnerControllerM>().GatilhoZona();
                }
            }
            else
            {
                GameObject spawner = null;
                if (transform.parent.parent.name.Equals("LevelP1"))
                {
                    spawner = GameObject.Find("Spawner3P1");
                }
                else if (transform.parent.parent.name.Equals("LevelP2"))
                {
                    spawner = GameObject.Find("Spawner3P2");
                }
                if (spawner.transform.childCount != 0)
                {
                    for (int ii = 0; ii < spawner.transform.childCount; ii++)
                    {
                        spawner.transform.GetChild(ii).GetComponent<SpawnerControllerM>().GatilhoZona();
                    }
                    Invoke("Sleep", 5f);
                }
                else
                { 
                    spawner.GetComponent<SpawnerControllerM>().GatilhoZona();
                }
            }
        }
    }
    public void Sleep()
    {
        return;
    }
}
