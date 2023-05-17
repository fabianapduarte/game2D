using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnerController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] enemies;
    private int qntMax;
    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("LevelOne"))
        {
            enemies = FindObjectOfType<GameController>().GetEnemies(1);
        } else if (SceneManager.GetActiveScene().name.Equals("LevelTwo"))
        {
            enemies = FindObjectOfType<GameController>().GetEnemies(2);
        }else if (SceneManager.GetActiveScene().name.Equals("LevelTree"))
        {
            enemies = FindObjectOfType<GameController>().GetEnemies(3);
        }
        else if(SceneManager.GetActiveScene().name.Equals("LevelFour"))
        {
            enemies = FindObjectOfType<GameController>().GetEnemies(4);
        }
        qntMax = Random.Range(4, 10);

        
        while (qntMax > 0)
        {
            Invoke("InstanciaInimigos", 150f);
            qntMax--;
        }
    }

    private void InstanciaInimigos()
    {
        Instantiate(enemies[Random.Range(0, enemies.Length)], transform.position, Quaternion.identity);
    }



}
