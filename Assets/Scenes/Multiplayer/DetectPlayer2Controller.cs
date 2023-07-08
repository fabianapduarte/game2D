using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectPlayer2Controller : MonoBehaviour
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

            if (transform.parent.name.Equals("Zone1P2"))
            {
                GameObject spawner = GameObject.Find("Spawner1P2");
                if (spawner.transform.childCount != 0)
                {
                    for (int ii = 0; ii < spawner.transform.childCount; ii++)
                    {
                        spawner.transform.GetChild(ii).GetComponent<SpawnerControllerPlayer2>().GatilhoZona();
                        Invoke("Sleep", 5f);
                    }
                }
                else
                {
                    spawner.GetComponent<SpawnerControllerPlayer2>().GatilhoZona();
                }
            }
            else if (transform.parent.name.Equals("Zone2P2"))
            {
                GameObject spawner = GameObject.Find("Spawner2P2");
                if (spawner.transform.childCount != 0)
                {
                    for (int ii = 0; ii < spawner.transform.childCount; ii++)
                    {
                        spawner.transform.GetChild(ii).GetComponent<SpawnerControllerPlayer2>().GatilhoZona();
                        Invoke("Sleep", 5f);
                    }
                }
                else
                {
                    spawner.GetComponent<SpawnerControllerPlayer2>().GatilhoZona();
                }
            }
            else if (transform.parent.name.Equals("Zone3P2"))
            {
                GameObject spawner = GameObject.Find("Spawner3P2");
                if (spawner.transform.childCount != 0)
                {
                    for (int ii = 0; ii < spawner.transform.childCount; ii++)
                    {
                        spawner.transform.GetChild(ii).GetComponent<SpawnerControllerPlayer2>().GatilhoZona();
                        Invoke("Sleep", 5f);
                    }
                }
                else
                {
                    spawner.GetComponent<SpawnerControllerPlayer2>().GatilhoZona();
                }
            }
            else if (transform.parent.name.Equals("Zone1Lv2P2"))
            {
                GameObject spawner = GameObject.Find("Spawner1Lv2P2");
                if (spawner.transform.childCount != 0)
                {
                    for (int ii = 0; ii < spawner.transform.childCount; ii++)
                    {
                        spawner.transform.GetChild(ii).GetComponent<SpawnerControllerPlayer2>().GatilhoZona();
                        Invoke("Sleep", 5f);
                    }
                }
                else
                {
                    spawner.GetComponent<SpawnerControllerPlayer2>().GatilhoZona();
                }
            }
            else if (transform.parent.name.Equals("Zone2Lv2P2"))
            {
                GameObject spawner = GameObject.Find("Spawner2Lv2P2");
                if (spawner.transform.childCount != 0)
                {
                    for (int ii = 0; ii < spawner.transform.childCount; ii++)
                    {
                        spawner.transform.GetChild(ii).GetComponent<SpawnerControllerPlayer2>().GatilhoZona();
                        Invoke("Sleep", 5f);
                    }
                }
                else
                {
                    spawner.GetComponent<SpawnerControllerPlayer2>().GatilhoZona();
                }
            }
            else if (transform.parent.name.Equals("Zone1Lv3P2"))
            {
                GameObject spawner = GameObject.Find("Spawner1Lv3P2");
                if (spawner.transform.childCount != 0)
                {
                    for (int ii = 0; ii < spawner.transform.childCount; ii++)
                    {
                        spawner.transform.GetChild(ii).GetComponent<SpawnerControllerPlayer2>().GatilhoZona();
                        Invoke("Sleep", 5f);
                    }
                }
                else
                {
                    spawner.GetComponent<SpawnerControllerPlayer2>().GatilhoZona();
                }
            }
            else if (transform.parent.name.Equals("Zone2Lv3P2"))
            {
                GameObject spawner = GameObject.Find("Spawner2Lv3P2");
                if (spawner.transform.childCount != 0)
                {
                    for (int ii = 0; ii < spawner.transform.childCount; ii++)
                    {
                        spawner.transform.GetChild(ii).GetComponent<SpawnerControllerPlayer2>().GatilhoZona();
                        Invoke("Sleep", 5f);
                    }
                }
                else
                {
                    spawner.GetComponent<SpawnerControllerPlayer2>().GatilhoZona();
                }
            }
            else if (transform.parent.name.Equals("Zone3Lv3P2"))
            {
                GameObject spawner = GameObject.Find("Spawner3Lv3P2");
                if (spawner.transform.childCount != 0)
                {
                    for (int ii = 0; ii < spawner.transform.childCount; ii++)
                    {
                        spawner.transform.GetChild(ii).GetComponent<SpawnerControllerPlayer2>().GatilhoZona();
                        Invoke("Sleep", 5f);
                    }
                }
                else
                {
                    spawner.GetComponent<SpawnerControllerPlayer2>().GatilhoZona();
                }
            }
        }
    }
    public void Sleep()
    {
        return;
    }
}
