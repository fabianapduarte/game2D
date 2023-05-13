using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private int playerLifes = 5;
    private int danoPlayer = 1;
    private string sceneName;

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
    }

    public void HurtPlayer()
    {
        playerLifes--;
        Destroy(FindObjectOfType<Canvas>().transform.GetChild(playerLifes).gameObject);
        //Destroy(canva.transform.GetChild(playerLifes).gameObject);
        if(playerLifes == 0)
        {
            //playerCollider.size = new Vector2(0.4f, 0.18f);
            Invoke("GameOver", 3f);
        }
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


}
