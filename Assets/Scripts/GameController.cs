using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //Constantes de menu
    public const int play = 1;


    // player
    private int playerLifes = 5;
    private int danoPlayer = 1;
    private Vector3 savePoint = Vector3.zero;

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

    public int GetDanoPlayer() {
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
        if (newScene.name.Equals(previousScene.name)){
            if (savePoint != Vector3.zero)
            {
                GameObject.Find("player").transform.position = savePoint;
            }
        }
    }

    public void HurtPlayer()
    {
        playerLifes--;
        //Destroy(FindObjectOfType<Image>().gameObject);
        Destroy(canva.transform.GetChild(playerLifes).gameObject);
        if (playerLifes == 0)
        {
            //playerCollider.size = new Vector2(0.4f, 0.18f);
            Invoke("DeadPlayer", 1f);
        }
            
    }

    public void DeadPlayer()
    {
         Invoke("GameOver", 3f);
    }

    public void SafePoint(Collider2D collider)
    {
        savePoint = collider.transform.position;
    }

    public void HurtEnemy(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy1"))
        {
            collision.gameObject.GetComponent<Enemy1Controller>().Hurt(danoPlayer);
        }

        if (collision.gameObject.tag.Equals("Enemy2"))
        {
            collision.gameObject.GetComponent<Enemy2Controller>().Hurt(danoPlayer);
        }

        if (collision.gameObject.tag.Equals("Enemy3"))
        {
            collision.gameObject.GetComponent<Enemy3Controller>().Hurt(danoPlayer);
        }
    }

    void GameOver()
    {
        sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    public void LevelEnd()
    {
        playerLifes = 5;
        Invoke("NextLevel", 3f);
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
