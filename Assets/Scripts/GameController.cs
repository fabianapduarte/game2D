using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //Constantes de menu
    public const int play = 3;

    // player
    private int playerLifes = 5;
    private int danoPlayer = 1;
    private float maxSpeed = 11f;

    private Vector3 savePoint = Vector3.zero;
   
    // enemies
    public GameObject[] enemiesLevelOne;
    public GameObject[] enemiesLevelTwo;
    public GameObject[] enemiesLevelTree;
    public GameObject[] enemiesLevelFour;

    private string sceneName;

    //public Image[] hearts;
    public Canvas canva;

    public static GameController instance = null;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null){
            instance = this;
        } else {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += GetSavePoints;
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= GetSavePoints;
    }

    public int GetDanoPlayer()
    {
        return danoPlayer;
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
        }
        else
        {
            return enemiesLevelFour;
        }
    }
   void GetSavePoints(Scene previousScene, Scene newScene)
    {
        if (newScene.name.Equals("Derrota")){
            if (savePoint != Vector3.zero)
            {
                GameObject.Find("player").transform.position = savePoint;
            }
        }
    }

    public void HurtPlayer(GameObject enemy/*int danoEnemy*/)
    {
        GameObject.Find("player").GetComponent<PlayerController>().AnimationHurtPlayer();
        if (enemy.layer == 6)
        {
            playerLifes -= enemy.GetComponent<Enemy1Controller>().GetDano();
        }
        if (enemy.layer == 7)
        {
            playerLifes -= enemy.GetComponent<Enemy2Controller>().GetDano();
        }
        if (enemy.layer == 8)
        {
            playerLifes -= enemy.GetComponent<Enemy3Controller>().GetDano();
        }
        if(enemy.layer == 9)
        {
            playerLifes -= enemy.GetComponent<Enemy4Controller>().GetDano();
        }
        //playerLifes -= danoEnemy;
        //Destroy(FindObjectOfType<Image>().gameObject);
        //Destroy(canva.transform.GetChild(playerLifes).gameObject);
        if (playerLifes <= 0)
        {
            GameObject.Find("player").GetComponent<PlayerController>().AnimationDeadPlayer();
            Invoke("DeadPlayer", 1f);
        }
            
    }

    public void DeadPlayer()
    {
         Invoke("GameOver", 2f);
    }

    public void SafePoint(GameObject savepoint)
    {
        savePoint = savepoint.transform.position;
    }

    /*public void HurtEnemy(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            collision.gameObject.GetComponent<Enemy1Controller>().Hurt(danoPlayer);
        }

        if (collision.gameObject.layer == 7)
        {
            collision.gameObject.GetComponent<Enemy2Controller>().Hurt(danoPlayer);
        }

        if (collision.gameObject.layer == 8)
        {
            collision.gameObject.GetComponent<Enemy3Controller>().Hurt(danoPlayer);
        }
    }*/

    public void GetCollectibles(GameObject collectable)
    {
        if(collectable.layer == 10) // force
        {
            danoPlayer++;
            Debug.Log(danoPlayer);
        }
        if(collectable.layer == 11) // speed
        {
            if(GameObject.Find("player").GetComponent<PlayerController>().GetSpeed() <= maxSpeed)
            {
                GameObject.Find("player").GetComponent<PlayerController>().SetSpeed();
            }
            Debug.Log(GameObject.Find("player").GetComponent<PlayerController>().GetSpeed());
        }
    }

    void GameOver()
    {
        string derrota = SceneUtility.GetScenePathByBuildIndex(2);
        //GameObject.Find("UIController").GetComponent<UIController>().PreviousScene(SceneManager.GetActiveScene());
        SceneManager.LoadScene(derrota);
    }

    public void LevelEnd()
    {
        playerLifes = 5;
        string vitoria = SceneUtility.GetScenePathByBuildIndex(1);
        //GameObject.Find("UIController").GetComponent<UIController>().PreviousScene(SceneManager.GetActiveScene());
        SceneManager.LoadScene(vitoria);
        //Invoke("NextLevel", 3f);
    }

    void NextLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex+1);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(play);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
