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
        Debug.Log(transform.parent.name);
        if (transform.parent.parent.name.Equals("Level1P1") || transform.parent.parent.name.Equals("Level1P2"))
        {
            enemies = FindObjectOfType<GameController>().GetEnemies(1);
            qntMax = Random.Range(3, 5);
        }
        else if (transform.parent.name.Equals("Level2P1") || transform.parent.name.Equals("Level2P2"))
        {
            enemies = FindObjectOfType<GameController>().GetEnemies(2);
            qntMax = Random.Range(4, 6);
        }
        else if (transform.parent.name.Equals("Level3P1") || transform.parent.name.Equals("Level3P2"))
        {
            enemies = FindObjectOfType<GameController>().GetEnemies(3);
            qntMax = Random.Range(5, 7);
        }
        else if (transform.parent.name.Equals("Level4P1") || transform.parent.name.Equals("Level4P2"))
        {
            enemies = FindObjectOfType<GameController>().GetEnemies(4);
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
