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

    private int contabilizaColetaP1 = 0;
    private int contabilizaColetaP2 = 0;

    private int contabilizaMorteP1 = 0;
    private int contabilizaMorteP2 = 0;

    private int contabilizaBonusSpeedP1 = 0;
    private int contabilizaBonusForceP1 = 0;

    private int contabilizaBonusSpeedP2 = 0;
    private int contabilizaBonusForceP2 = 0;

    private Vector3 savePointP1 = Vector3.zero;
    private Vector3 savePointP2 = Vector3.zero;

    // enemies
    public GameObject[] enemiesLevelOne;
    public GameObject[] enemiesLevelTwo;
    public GameObject[] enemiesLevelTree;
    public GameObject[] enemiesLevelFour;

    public static MGameController instance = null;
    public GameObject hudP1;
    public GameObject hudP2;

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

        if (SceneManager.GetActiveScene().name.Equals("MLevelTree") || SceneManager.GetActiveScene().name.Equals("MLevelFour"))
        {
            Destroy(GameObject.Find("BGP1"));
            Destroy(GameObject.Find("BGP2"));
            hudP1.transform.GetChild(0).gameObject.SetActive(true);
            hudP1.transform.GetChild(1).gameObject.SetActive(true);
            hudP2.transform.GetChild(0).gameObject.SetActive(true);
            hudP2.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
    public void SetContabilizaBonusForce(GameObject player)
    {
        if (player.name.Equals("Jogador1"))
        {
            contabilizaBonusForceP1 = 0;
        }
        else
        {
            contabilizaBonusForceP2 = 0;
        }
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

    public void DeadPlayer(GameObject player)
    {
        if (player.name.Equals("Jogador1"))
        {
            if (contabilizaMorteP1 == 0)
            {
                GameOver(player);
                //Invoke("GameOver", 2f);
                contabilizaMorteP1 = 1;
            }
        }
        else
        {
            if (contabilizaMorteP2 == 0)
            {
                GameOver(player);
                //Invoke("GameOver", 2f);
                contabilizaMorteP2 = 1;
            }        
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
            if (player.name.Equals("Jogador1"))
            {
                if (contabilizaBonusForceP1 == 0)
                {
                    GameObject.Find(player.name).GetComponent<Player1Controller>().SetDanoPlayer(1);
                    contabilizaBonusForceP1 = 1;
                }
            }
            else
            {
                if (contabilizaBonusForceP2 == 0)
                {
                    GameObject.Find(player.name).GetComponent<Player2Controller>().SetDanoPlayer(1);
                    contabilizaBonusForceP2 = 1;
                }
            }
        }
        if (collectable.layer == 11) // speed
        {
            if (player.name.Equals("Jogador1"))
            {
                if (GameObject.Find(player.name).GetComponent<Player1Controller>().GetSpeed() <= maxSpeed)
                {
                    if (contabilizaBonusSpeedP1 == 0)
                    {
                    GameObject.Find(player.name).GetComponent<Player1Controller>().SetSpeed(1);
                    contabilizaBonusSpeedP1 = 1;
                    }
                }
            }
            else
            {
                if (GameObject.Find(player.name).GetComponent<Player2Controller>().GetSpeed() <= maxSpeed)
                {
                    if (contabilizaBonusSpeedP2 == 0)
                    {
                        GameObject.Find(player.name).GetComponent<Player2Controller>().SetSpeed(1);
                        contabilizaBonusSpeedP2 = 1;
                    }
                }
            }
        }
    }

    void GameOver(GameObject player)
    {
        if (player.name.Equals("Jogador1"))
        {
            player1Lifes = 5;
            if (savePointP1 != Vector3.zero)
            {
                if (contabilizaBonusForceP1 != 0)
                {
                    contabilizaBonusForceP1 = 0;
                }
            }
            else
            {
                contabilizaBonusSpeedP1 = 0;
            }
            string derrota = SceneUtility.GetScenePathByBuildIndex(2);
            GameObject.Find("MenuController").GetComponent<MenuController>().PreviousScene(SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene(derrota);
            contabilizaMorteP1 = 0;
        }
        else
        {
            player2Lifes = 5;
            if (savePointP2 != Vector3.zero)
            {
                if (contabilizaBonusForceP2 != 0)
                {
                    contabilizaBonusForceP2 = 0;
                }
            }
            else
            {
                contabilizaBonusSpeedP2 = 0;
            }
            string derrota = SceneUtility.GetScenePathByBuildIndex(2);
            GameObject.Find("MenuController").GetComponent<MenuController>().PreviousScene(SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene(derrota);
            contabilizaMorteP2 = 0;
        }
    }
}
