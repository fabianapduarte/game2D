using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MGameController : MonoBehaviour
{
    // player
    private int player1Lifes = 5;
    private int player2Lifes = 5;
    private float maxSpeed = 10f;
    private int contabilizaColeta = 0;
    private int contabilizaMorte = 0;
    private int contabilizaBonusSpeed = 0;
    private int contabilizaBonusForce = 0;

    private Vector3 savePointP1 = Vector3.zero;
    private Vector3 savePointP2 = Vector3.zero;

    // enemies
    public GameObject[] enemiesLevelOne;
    public GameObject[] enemiesLevelTwo;
    public GameObject[] enemiesLevelTree;
    public GameObject[] enemiesLevelFour;
    public GameObject[] enemiesLevelFive;
    public GameObject[] enemiesAcidron1;

    public static MGameController instance = null;
    public GameObject hud;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        if (SceneManager.GetActiveScene().name.Equals("LevelTree") || SceneManager.GetActiveScene().name.Equals("LevelFour"))
        {
            Destroy(GameObject.Find("BG"));
            hud.transform.GetChild(0).gameObject.SetActive(true);
            hud.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
    public void SetContabilizaBonusForce()
    {
        contabilizaBonusForce = 0;
    }

    public GameObject[] GetEnemies(int level)
    {
        if (level == 1)
        {
            return enemiesLevelOne;
        }
        else if (level == 2)
        {
            return enemiesLevelTwo;
        }
        else if (level == 3)
        {
            return enemiesLevelTree;
        }
        else
        {
            return enemiesLevelFour;
        }
    }

    public void HurtPlayer(int danoEnemy, GameObject player)
    {
        if (player.name.Equals("Jogador1")){
            GameObject.Find(player.name).GetComponent<Player1Controller>().AnimationHurtPlayer();
            if (danoEnemy > 1)
            {
                while (danoEnemy > 0)
                {
                    player1Lifes--;
                    if (player1Lifes >= 0)
                    {
                        Destroy(GameObject.Find("LifePlayer").transform.GetChild(player1Lifes).gameObject);
                    }
                    danoEnemy--;
                }
            }
            else
            {
                player1Lifes--;
                if (player1Lifes >= 0)
                {
                    Destroy(GameObject.Find("LifePlayer").transform.GetChild(player1Lifes).gameObject);
                }

            }
            if (player1Lifes <= 0)
            {
                GameObject.Find(player.name).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
                GameObject.Find(player.name).GetComponent<Player1Controller>().AnimationDeadPlayer();
                Invoke("DeadPlayer", 1f);
            }
        }
        else
        {
            GameObject.Find(player.name).GetComponent<Player2Controller>().AnimationHurtPlayer();
            if (danoEnemy > 1)
            {
                while (danoEnemy > 0)
                {
                    player2Lifes--;
                    if (player2Lifes >= 0)
                    {
                        Destroy(GameObject.Find("LifePlayer").transform.GetChild(player2Lifes).gameObject);
                    }
                    danoEnemy--;
                }
            }
            else
            {
                player2Lifes--;
                if (player2Lifes >= 0)
                {
                    Destroy(GameObject.Find("LifePlayer").transform.GetChild(player2Lifes).gameObject);
                }

            }
            if (player2Lifes <= 0)
            {
                GameObject.Find(player.name).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
                GameObject.Find(player.name).GetComponent<Player2Controller>().AnimationDeadPlayer();
                Invoke("DeadPlayer", 1f);
            }
        }
        
    }

    public void DeadPlayer()
    {
        if (contabilizaMorte == 0)
        {
            Invoke("GameOver", 2f);
            contabilizaMorte = 1;
        }
    }

    public void SetSafePoint(GameObject savepoint, GameObject player)
    {
        if (player.name.Equals("Jogador1"))
        {
            savePointP1 = savepoint.transform.position;
        }
        else
        {
            savePointP2 = savepoint.transform.position;
        }
    }

    public Vector3 GetSavePoint(GameObject player)
    {
        if (player.name.Equals("Jogador1"))
        {
            return savePointP1;
        }
        else
        {
            return savePointP2;
        }
        
    }


    public void HurtEnemy(GameObject enemy, GameObject player)
    {
        if (player.name.Equals("Jogador1"))
        {
            enemy.GetComponent<EnemyControllerP1>().Hurt(GameObject.Find(player.name).GetComponent<Player1Controller>().GetDano());
        }
        else
        {
            enemy.GetComponent<EnemyControllerP2>().Hurt(GameObject.Find(player.name).GetComponent<Player2Controller>().GetDano());
        }
    }

    public void GetCollectibles(GameObject collectable, GameObject player)
    {
        if (collectable.layer == 10) // force
        {
            if (contabilizaBonusForce == 0)
            {
                if (player.name.Equals("Jogador1"))
                {
                    GameObject.Find(player.name).GetComponent<Player1Controller>().SetDanoPlayer(1);
                }
                else
                {
                    GameObject.Find(player.name).GetComponent<Player2Controller>().SetDanoPlayer(1);
                }
                
                contabilizaBonusForce = 1;
            }
        }
        if (collectable.layer == 11) // speed
        {
            if (GameObject.Find("Simetra").GetComponent<PlayerController>().GetSpeed() <= maxSpeed)
            {
                if (contabilizaBonusSpeed == 0)
                {
                    GameObject.Find("Simetra").GetComponent<PlayerController>().SetSpeed(1);
                    contabilizaBonusSpeed = 1;
                }

            }

        }
    }
}
