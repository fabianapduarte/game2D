using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnerControllerPlayer1 : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] enemies;
    private int qntMax;
    public Transform enemiesSlot;
    public Transform player1;

    public GameObject Zone;
    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("Multiplayer"))
        {
            enemies = FindObjectOfType<GameController>().GetEnemies(1);
            qntMax = Random.Range(3, 5);
        }
    }

    private void Update()
    {
        if (qntMax == 0 && enemiesSlot.childCount == 0)
        {
            GameObject.Find("detectInZone").GetComponent<DetectPlayer1Controller>().setContabilizaStart();
            if (SceneManager.GetActiveScene().name.Equals("Multiplayer"))
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().ResetCoordX(1);
            }

            GameObject.Find("AudioController").GetComponent<AudioController>().BattleSoundFinish();
            Destroy(Zone);
        }
    }

    public void GatilhoZona()
    {
        player1 = GameObject.Find("Jogador1").transform;
        if(player1.position.x > 305 && player1.position.x < 445)
        {
            enemies = FindObjectOfType<GameController>().GetEnemies(2);
            qntMax = Random.Range(4, 6);
        }
        else if (player1.position.x > 450 && player1.position.x < 750)
        {
            enemies = FindObjectOfType<GameController>().GetEnemies(3);
            qntMax = Random.Range(5, 7);
        }
        else if (player1.position.x > 900 && player1.position.x < 1200)
        {
            enemies = FindObjectOfType<GameController>().GetEnemies(4);
            qntMax = Random.Range(6, 8);
        }

        Invoke("Spawn", 2f);
    }

    private void Spawn()
    {
        Instantiate(enemies[Random.Range(0, enemies.Length)], transform.position, Quaternion.identity, enemiesSlot);
        if (qntMax > 0)
        {
            Invoke("Spawn", Random.Range(8f, 14f));
            qntMax--;
        }
    }
}
