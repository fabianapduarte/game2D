using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectPlayer1Controller : MonoBehaviour
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

            if (SceneManager.GetActiveScene().name.Equals("Multiplayer"))
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MCameraController>().SetLevel1(transform.parent.name);
            }

            if (transform.parent.name.Equals("Zone1P1"))
            {
                GameObject spawner = GameObject.Find("Spawner1P1");
                if (spawner.transform.childCount != 0)
                {
                    for (int ii = 0; ii < spawner.transform.childCount; ii++)
                    {
                        spawner.transform.GetChild(ii).GetComponent<SpawnerControllerPlayer1>().GatilhoZona();
                        Invoke("Sleep", 5f);
                    }
                }
                else
                {
                    spawner.GetComponent<SpawnerControllerPlayer1>().GatilhoZona();
                }
            }
            else if (transform.parent.name.Equals("Zone2P1"))
            {
                GameObject spawner = GameObject.Find("Spawner2P1");
                if (spawner.transform.childCount != 0)
                {
                    for (int ii = 0; ii < spawner.transform.childCount; ii++)
                    {
                        spawner.transform.GetChild(ii).GetComponent<SpawnerControllerPlayer1>().GatilhoZona();
                        Invoke("Sleep", 5f);
                    }
                }
                else
                {
                    spawner.GetComponent<SpawnerControllerPlayer1>().GatilhoZona();
                }
            }
            else
            {
                GameObject spawner = GameObject.Find("Spawner3P1");
                if (spawner.transform.childCount != 0)
                {
                    for (int ii = 0; ii < spawner.transform.childCount; ii++)
                    {
                        spawner.transform.GetChild(ii).GetComponent<SpawnerControllerPlayer1>().GatilhoZona();
                        Invoke("Sleep", 5f);
                    }
                }
                else
                {
                    spawner.GetComponent<SpawnerControllerPlayer1>().GatilhoZona();
                }
            }
        }
    }
    public void Sleep()
    {
        return;
    }
}
