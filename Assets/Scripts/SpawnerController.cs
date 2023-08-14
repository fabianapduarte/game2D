using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnerController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] enemies;
    private int qntMax;
    public Transform enemiesSlot;

    public GameObject Zone;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("LevelOne"))
        {
            enemies = FindObjectOfType<GameController>().GetEnemies(1);
            qntMax = Random.Range(3, 5);
        } else if (SceneManager.GetActiveScene().name.Equals("LevelTwo"))
        {
            enemies = FindObjectOfType<GameController>().GetEnemies(2);
            qntMax = 4;
        }
        else if (SceneManager.GetActiveScene().name.Equals("LevelTree"))
        {
            enemies = FindObjectOfType<GameController>().GetEnemies(3);
            qntMax = Random.Range(4, 6);
        }
        else if(SceneManager.GetActiveScene().name.Equals("LevelFour"))
        {
            enemies = FindObjectOfType<GameController>().GetEnemies(4);
            qntMax = Random.Range(5, 7);
        }else if (SceneManager.GetActiveScene().name.Equals("LevelFive"))
        {
            enemies = FindObjectOfType<GameController>().GetEnemies(5);
            qntMax = Random.Range(6, 8);
        }
        else if (SceneManager.GetActiveScene().name.Equals("Acidron1"))
        {
            enemies = FindObjectOfType<GameController>().GetEnemies(6);
            qntMax = 3;
        }
    }

    private void Update()
    {
        if((qntMax == 0 && enemiesSlot.childCount == 0))
        {
            GameObject.Find("detectInZone").GetComponent<DetectPlayerController>().setContabilizaStart();
            if (SceneManager.GetActiveScene().name.Equals("LevelOne"))
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().ResetCoordX(1, Zone.name);
            }
            else if (SceneManager.GetActiveScene().name.Equals("LevelTwo"))
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().ResetCoordX(2, Zone.name);
            }
            else if (SceneManager.GetActiveScene().name.Equals("LevelTree"))
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().ResetCoordX(3, Zone.name);
            }
            else if (SceneManager.GetActiveScene().name.Equals("LevelFour"))
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().ResetCoordX(4, Zone.name);
            }else if (SceneManager.GetActiveScene().name.Equals("LevelFive"))
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().ResetCoordX(5, Zone.name);
            }
            else if (SceneManager.GetActiveScene().name.Equals("Acidron1"))
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().ResetCoordX(6, Zone.name);
            }
            GameObject.Find("AudioController").GetComponent<AudioController>().BattleSoundFinish();
            Destroy(Zone);
        }
        else if (enemiesSlot.childCount == 0){
            if (IsInvoking("Spawn"))
            {
                CancelInvoke("Spawn");
                Spawn();
            }
        }

    }

    public void GatilhoZona()
    {
        Invoke("Spawn", 2f);
    }

    private void Spawn()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, 0f);
        Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPosition, Quaternion.identity, enemiesSlot);
        if(qntMax > 0)
        {
            Invoke("Spawn", Random.Range(8f, 10f));
            qntMax--;
        }
    }
}
