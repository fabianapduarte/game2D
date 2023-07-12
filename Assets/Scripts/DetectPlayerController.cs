using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            GameObject.Find("AudioController").GetComponent<AudioController>().BattleSoundStart();
            contabilizaStart = 1;
            transform.parent.GetComponent<ZonesScriptController>().ClosedZone();
            if (SceneManager.GetActiveScene().name.Equals("LevelOne"))
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().SetLevel1(transform.parent.name);
            }
            else if (SceneManager.GetActiveScene().name.Equals("LevelTwo"))
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().SetLevel2(transform.parent.name);
            }
            else if (SceneManager.GetActiveScene().name.Equals("LevelTree"))
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().SetLevel3(transform.parent.name);
            }
            else if (SceneManager.GetActiveScene().name.Equals("LevelFour"))
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().SetLevel4(transform.parent.name);
            }
            else if (SceneManager.GetActiveScene().name.Equals("LevelFive"))
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().SetLevel5(transform.parent.name);
            }
            else if (SceneManager.GetActiveScene().name.Equals("Acidron1"))
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().SetLevel6(transform.parent.name);
            }
            if (transform.parent.name.Equals("Zone1"))
            {
                GameObject spawner = null;
                if (SceneManager.GetActiveScene().name.Equals("Multiplayer"))
                {
                    if (transform.parent.parent.name.Equals("Level1P1"))
                    {
                        spawner = GameObject.Find("Spawner1L1P1");
                    }
                    if (transform.parent.parent.name.Equals("Level1P2"))
                    {
                        spawner = GameObject.Find("Spawner1L1P2");
                    }

                    if (transform.parent.parent.name.Equals("Level2P1"))
                    {
                        spawner = GameObject.Find("Spawner1L2P1");
                    }
                    if (transform.parent.parent.name.Equals("Level2P2"))
                    {
                        spawner = GameObject.Find("Spawner1L2P2");
                    }

                    if (transform.parent.parent.name.Equals("Level3P1"))
                    {
                        spawner = GameObject.Find("Spawner1L3P1");
                    }
                    if (transform.parent.parent.name.Equals("Level3P2"))
                    {
                        spawner = GameObject.Find("Spawner1L3P2");
                    }

                    if (transform.parent.parent.name.Equals("Level4P1"))
                    {
                        spawner = GameObject.Find("Spawner1L4P1");
                    }
                    if (transform.parent.parent.name.Equals("Level4P2"))
                    {
                        spawner = GameObject.Find("Spawner1L4P2");
                    }
                }
                else
                {
                    spawner = GameObject.Find("Spawner1");
                }
                if (spawner.transform.childCount != 0)
                {
                    for (int ii = 0; ii < spawner.transform.childCount; ii++)
                    {
                        if (SceneManager.GetActiveScene().name.Equals("Multiplayer"))
                        {
                            spawner.transform.GetChild(ii).GetComponent<SpawnerControllerM>().GatilhoZona();
                        }
                        else
                        {
                            spawner.transform.GetChild(ii).GetComponent<SpawnerController>().GatilhoZona();
                        }
                        Invoke("Sleep", 5f);
                    }
                }
                else
                {
                    if (SceneManager.GetActiveScene().name.Equals("Multiplayer"))
                    {
                        spawner.GetComponent<SpawnerControllerM>().GatilhoZona();
                    }
                    else
                    {
                        spawner.GetComponent<SpawnerController>().GatilhoZona();
                    }
                }
            }
            else if (transform.parent.name.Equals("Zone2"))
            {
                GameObject spawner = GameObject.Find("Spawner2");
                if (spawner.transform.childCount != 0)
                {
                    for (int ii = 0; ii < spawner.transform.childCount; ii++)
                    {
                        if (SceneManager.GetActiveScene().name.Equals("Multiplayer"))
                        {
                            spawner.transform.GetChild(ii).GetComponent<SpawnerControllerM>().GatilhoZona();
                        }
                        else
                        {
                            spawner.transform.GetChild(ii).GetComponent<SpawnerController>().GatilhoZona();
                        }
                        Invoke("Sleep", 5f);
                    }
                }
                else
                {
                    if (SceneManager.GetActiveScene().name.Equals("Multiplayer"))
                    {
                        spawner.GetComponent<SpawnerControllerM>().GatilhoZona();
                    }
                    else
                    {
                        spawner.GetComponent<SpawnerController>().GatilhoZona();
                    }
                }
            }
            else
            {
                GameObject spawner = GameObject.Find("Spawner3");
                if (spawner.transform.childCount != 0)
                {
                    for (int ii = 0; ii < spawner.transform.childCount; ii++)
                    {
                        if (SceneManager.GetActiveScene().name.Equals("Multiplayer"))
                        {
                            spawner.transform.GetChild(ii).GetComponent<SpawnerControllerM>().GatilhoZona();
                        }
                        else
                        {
                            spawner.transform.GetChild(ii).GetComponent<SpawnerController>().GatilhoZona();
                        }
                        Invoke("Sleep", 5f);
                    }
                }
                else
                {
                    if (SceneManager.GetActiveScene().name.Equals("Multiplayer"))
                    {
                        spawner.GetComponent<SpawnerControllerM>().GatilhoZona();
                    }
                    else
                    {
                        spawner.GetComponent<SpawnerController>().GatilhoZona();
                    }
                }
            }
        }
    }
    public void Sleep()
    {
        return;
    }
}
