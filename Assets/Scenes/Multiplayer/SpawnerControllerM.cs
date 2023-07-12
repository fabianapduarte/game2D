using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnerControllerM : MonoBehaviour
{
    private GameObject[] enemies;
    private int qntMax;
    public Transform enemiesSlot;

    public GameObject Zone;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("MLevelOne"))
        {
            enemies = FindObjectOfType<MGameController>().GetEnemies(1);
            qntMax = Random.Range(3, 5);
        }
        else if (SceneManager.GetActiveScene().name.Equals("MLevelTwo"))
        {
            enemies = FindObjectOfType<MGameController>().GetEnemies(2);
            qntMax = Random.Range(4, 6);
        }
        else if (SceneManager.GetActiveScene().name.Equals("LevelTree"))
        {
            enemies = FindObjectOfType<MGameController>().GetEnemies(3);
            qntMax = Random.Range(5, 7);
        }
        else if (SceneManager.GetActiveScene().name.Equals("MLevelFour"))
        {
            enemies = FindObjectOfType<MGameController>().GetEnemies(4);
            qntMax = Random.Range(6, 8);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (qntMax == 0 && enemiesSlot.childCount == 0)
        {
            GameObject.Find("detectInZone").GetComponent<DetectPlayerController>().setContabilizaStart();
            GameObject.Find("AudioController").GetComponent<AudioController>().BattleSoundFinish();
            Destroy(Zone);
        }
    }

    public void GatilhoZona()
    {
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
