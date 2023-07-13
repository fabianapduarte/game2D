using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    // player
    private int playerLifes = 5;
    private float maxSpeed = 11f;
    private int contabilizaColeta = 0;
    private int contabilizaMorte = 0;
    private int contabilizaBonusSpeed = 0;
    private int contabilizaBonusForce = 0;

    //private Vector3 savePoint = Vector3.zero;
   
    // enemies
    public GameObject[] enemiesLevelOne;
    public GameObject[] enemiesLevelTwo;
    public GameObject[] enemiesLevelTree;
    public GameObject[] enemiesLevelFour;
    public GameObject[] enemiesLevelFive;
    public GameObject[] enemiesAcidron1;

    public static GameController instance = null;

    // Start is called before the first frame update
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

        if (SceneManager.GetActiveScene().name.Equals("LevelTree") ||
            SceneManager.GetActiveScene().name.Equals("LevelFour") || SceneManager.GetActiveScene().name.Equals("LevelFive"))
        {
            Destroy(GameObject.Find("BG"));
            GameObject.Find("numInfos").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("numInfos").transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void SetContabilizaBonusForce()
    {
        contabilizaBonusForce = 0;
    } 

    public GameObject[] GetEnemies(int level)
    {
        if(level == 1)
        {
            return enemiesLevelOne;
        }else if (level == 2)
        {
            return enemiesLevelTwo;
        } else if (level == 3)
        {
            return enemiesLevelTree;
        } else if(level == 4)
        {
            return enemiesLevelFour;
        }else if (level == 5)
        {
            return enemiesLevelFive;
        }
        else
        {
            return enemiesAcidron1;
        }
    }

    public void HurtPlayer(int danoEnemy)
    {
        GameObject.Find("Simetra").GetComponent<PlayerController>().AnimationHurtPlayer();
        if(danoEnemy > 1)
        {
            while(danoEnemy > 0)
            {
                playerLifes--;
                if (playerLifes >= 0)
                {
                    Destroy(GameObject.Find("LifePlayer").transform.GetChild(playerLifes).gameObject);
                }
                danoEnemy--;
            }
        }
        else
        {
            playerLifes--;
            if(playerLifes >= 0)
            {
                Destroy(GameObject.Find("LifePlayer").transform.GetChild(playerLifes).gameObject);
            }

        }
        if (playerLifes <= 0)
        {
            GameObject.Find("Simetra").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            GameObject.Find("Simetra").GetComponent<PlayerController>().AnimationDeadPlayer();
            Invoke("DeadPlayer", 1f);
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

    public void SetSafePoint(GameObject savepoint)
    {
        PlayerPrefs.SetFloat("saveX", savepoint.transform.position.x);
        PlayerPrefs.SetFloat("saveY", savepoint.transform.position.y);
    }

    public Vector3 GetSavePoint()
    {
        Vector3 savePointVector = new Vector3(PlayerPrefs.GetFloat("saveX"), PlayerPrefs.GetFloat("saveY"), -10f);
        return savePointVector;
    }

    public void HurtEnemy(GameObject enemy)
    {
        if (enemy.layer == 6)
        {
            enemy.GetComponent<Enemy1Controller>().Hurt(GameObject.Find("Simetra").GetComponent<PlayerController>().GetDano());
        }

        if (enemy.layer == 7)
        {
            enemy.GetComponent<Enemy2Controller>().Hurt(GameObject.Find("Simetra").GetComponent<PlayerController>().GetDano());
        }

        if (enemy.layer == 8)
        {
            enemy.GetComponent<Enemy3Controller>().Hurt(GameObject.Find("Simetra").GetComponent<PlayerController>().GetDano());
        }

        if (enemy.layer == 9)
        {
            enemy.GetComponent<Enemy4Controller>().Hurt(GameObject.Find("Simetra").GetComponent<PlayerController>().GetDano());
        }
    }

    public void GetCollectibles(GameObject collectable)
    {
        if(collectable.layer == 10) // force
        {
            if (contabilizaBonusForce == 0)
            {
                GameObject.Find("Simetra").GetComponent<PlayerController>().SetDanoPlayer(1);
                contabilizaBonusForce = 1;
            }
        }
        if(collectable.layer == 11) // speed
        {
            if(GameObject.Find("Simetra").GetComponent<PlayerController>().GetSpeed() <= maxSpeed)
            {
                if(contabilizaBonusSpeed == 0)
                {
                    GameObject.Find("Simetra").GetComponent<PlayerController>().SetSpeed(1);
                    contabilizaBonusSpeed = 1;
                }
                
            }
            
        }
    }

    void GameOver()
    {
        playerLifes = 5;
        if ((PlayerPrefs.GetFloat("saveX") != 0) && (PlayerPrefs.GetFloat("saveY") != 0))
        {
            if(contabilizaBonusForce != 0)
            {
                contabilizaBonusForce = 0;
            }  
        }
        else
        {
            contabilizaBonusSpeed = 0;
        }
        string derrota = SceneUtility.GetScenePathByBuildIndex(2);
        GameObject.Find("MenuController").GetComponent<MenuController>().PreviousScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(derrota);
        contabilizaMorte = 0;
    }

    public void LevelEnd()
    {
        playerLifes = 5;
        string vitoria = SceneUtility.GetScenePathByBuildIndex(1);
        string final = SceneUtility.GetScenePathByBuildIndex(13);

        PlayerPrefs.SetFloat("saveX", 0f);
        PlayerPrefs.SetFloat("saveY", 0f);

        GameObject.Find("MenuController").GetComponent<MenuController>().PreviousScene(SceneManager.GetActiveScene().buildIndex);

        //Index da ultima fase
        if (SceneManager.GetActiveScene().name.Equals("Tutorial") || SceneManager.GetActiveScene().name.Equals("Acidron1") || SceneManager.GetActiveScene().name.Equals("Acidron2"))
        {
            if(SceneManager.GetActiveScene().buildIndex == 12){
                SceneManager.LoadScene(final);
            }
            else{
                SceneManager.LoadScene(vitoria);
            }
        }
        else
        {
            if(contabilizaBonusForce == 1 && contabilizaBonusSpeed == 1)
            {
                if (SceneManager.GetActiveScene().buildIndex == 12)
                {
                    SceneManager.LoadScene(final);
                }
                else
                {
                    SceneManager.LoadScene(vitoria);
                }
                contabilizaBonusSpeed = 0;
                contabilizaBonusForce = 0;
            }
        }
    }
}
