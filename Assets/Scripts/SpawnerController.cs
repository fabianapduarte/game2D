using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnerController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] enemies;
    private int qntMax;
    public Transform enemys;

    public GameObject Zone;
    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("LevelOne"))
        {
            enemies = FindObjectOfType<GameController>().GetEnemies(1);
            qntMax = Random.Range(3, 5);
        } else if (SceneManager.GetActiveScene().name.Equals("LevelTwo"))
        {
            enemies = FindObjectOfType<GameController>().GetEnemies(2);
            qntMax = Random.Range(4, 6);
        }
        else if (SceneManager.GetActiveScene().name.Equals("LevelTree"))
        {
            enemies = FindObjectOfType<GameController>().GetEnemies(3);
            qntMax = Random.Range(5, 7);
        }
        else if(SceneManager.GetActiveScene().name.Equals("LevelFour"))
        {
            enemies = FindObjectOfType<GameController>().GetEnemies(4);
            qntMax = Random.Range(6, 8);
        }
    }

    private void Update()
    {
        if(qntMax == 0 && enemys.childCount == 0)
        {
            Destroy(Zone);
        }
    }

    public void GatilhoZona()
    {
        Invoke("Spawn", 2f);
    }

    private void Spawn()
    {
        Instantiate(enemies[Random.Range(0, enemies.Length)], transform.position, Quaternion.identity, enemys);
        if(qntMax > 0)
        {
            Invoke("Spawn", Random.Range(10f, 14f));
            qntMax--;
        }
    }
}
