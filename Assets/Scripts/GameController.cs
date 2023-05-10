using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private int playerLifes = 5;
    private string sceneName;

    public static GameController instance = null;
    public BoxCollider2D playerCollider;
    public GameObject player;
    public Canvas canva;

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
        Destroy(canva.transform.GetChild(playerLifes).gameObject);
        Invoke("RestartLevel", 3f);
    }

    void RestartLevel()
    {
        //sceneName = SceneManager.GetActiveScene().name;
        /*if(playerLifes > 0)
        {
            SceneManager.LoadScene(sceneName);
        }
        else*/
        if(playerLifes == 0){
            playerCollider.size = new Vector2(0.4f, 0.18f);
            Invoke("GameOver", 3f);
        }
    }

    void GameOver()
    {
        sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);

    }

    void LevelEnd()
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
