using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class MGameController : MonoBehaviour
{
    // player
    private int player1Lifes = 5;
    private int player2Lifes = 5;
    private float maxSpeed = 10f;
    private int scoreP1;
    private int scoreP2;
    private bool verifVitoriaP1 = false;
    private bool verifVitoriaP2 = false;
    

    private int countMagicP1 = 0;
    private int countMagicP2 = 0;

    private int contabilizaMorteP1 = 0;
    private int contabilizaMorteP2 = 0;

    private int countSpeedP1 = 0;
    private int countForceP1 = 0;

    private int countSpeedP2 = 0;
    private int countForceP2 = 0;


    // enemies
    public GameObject[] enemiesLevelOne;
    public GameObject[] enemiesLevelTwo;
    public GameObject[] enemiesLevelTree;
    public GameObject[] enemiesLevelFour;

    public static MGameController instance = null;
    private GameObject hudP1;
    private GameObject hudP2;

    private string sceneName;

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

        hudP1 = GameObject.Find("numInfosP1");
        hudP2 = GameObject.Find("numInfosP2");
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("Placar"))
        {
            if(verifVitoriaP1 == true && verifVitoriaP2 == true)
            {
                GameObject.Find("pointP1").GetComponent<TextMeshProUGUI>().text = scoreP1 + "";
                GameObject.Find("pointP2").GetComponent<TextMeshProUGUI>().text = scoreP2 + "";
                GameObject.Find("Empate1").SetActive(true);
                GameObject.Find("Empate2").SetActive(true);
                GameObject.Find("Vencedor1").SetActive(false);
                GameObject.Find("Vencedor2").SetActive(false);
                GameObject.Find("Perdedor1").SetActive(false);
                GameObject.Find("Perdedor2").SetActive(false);

            }
            else if(verifVitoriaP1 == true)
            {
                GameObject.Find("pointP1").GetComponent<TextMeshProUGUI>().text = scoreP1 + "";
                GameObject.Find("pointP2").GetComponent<TextMeshProUGUI>().text = scoreP2 + "";
                GameObject.Find("Vencedor1").SetActive(true);
                GameObject.Find("Perdedor2").SetActive(true);
                GameObject.Find("Empate1").SetActive(false);
                GameObject.Find("Empate2").SetActive(false);
                GameObject.Find("Vencedor2").SetActive(false);
                GameObject.Find("Perdedor1").SetActive(false);
            }
            else
            {
                GameObject.Find("pointP1").GetComponent<TextMeshProUGUI>().text = scoreP1 + "";
                GameObject.Find("pointP2").GetComponent<TextMeshProUGUI>().text = scoreP2 + "";
                GameObject.Find("Vencedor2").SetActive(true);
                GameObject.Find("Perdedor1").SetActive(true);
                GameObject.Find("Vencedor1").SetActive(false);
                GameObject.Find("Perdedor2").SetActive(false);
                GameObject.Find("Empate1").SetActive(false);
                GameObject.Find("Empate2").SetActive(false);
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

    public void GetCollectibles(GameObject collectable, GameObject player)
    {
        if (collectable.layer == 10) // force
        {
            if (player.name.Equals("Jogador1"))
            {
                scoreP1 += 2;
                countForceP1++;
                hudP1.transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = "x" + countForceP1;
            }
            else
            {
                scoreP2 += 2;
                countForceP2++;
                hudP2.transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = "x" + countForceP2;
            }
        }
        if (collectable.layer == 11) // speed
        {
            if (player.name.Equals("Jogador1"))
            {
                scoreP1++;
                countSpeedP1++;
                hudP1.transform.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>().text = "x" + countSpeedP1;
            }
            else
            {
                scoreP2++;
                countSpeedP2++;
                hudP2.transform.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>().text = "x" + countSpeedP2;
            }
        }
        if (collectable.layer == 14) // magic
        {
            if (player.name.Equals("Jogador1"))
            {
                scoreP1 += 3;
                countMagicP1++;
                hudP1.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = "x" + countMagicP1;
            }
            else
            {
                scoreP2 += 3;
                countMagicP2++;
                hudP2.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = "x" + countMagicP2;
            }
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //Define coisas para cenas especificas quando estas sao carregadas - util demais
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "MLevelOne" || sceneName == "MLevelTwo" || sceneName == "MLevelTree" || sceneName == "MLevelFour")
        {
            Invoke("GameOver", 90f);
        }
    }

        void GameOver()
    {
        if(scoreP1 > scoreP2)
        {
            verifVitoriaP1 = true;
            verifVitoriaP2 = false;
        }
        else if(scoreP1 < scoreP2)
        {
            verifVitoriaP1 = false;
            verifVitoriaP2 = true;
        }
        else
        {
            verifVitoriaP1 = true;
            verifVitoriaP2 = true;
        }

    }

    void GameOver(GameObject player)
    {
        player1Lifes = 5;
        player2Lifes = 5;
        if (player.name.Equals("Jogador1"))
        {
            scoreP1 = 0;
            verifVitoriaP1 = false;
            verifVitoriaP2 = true;
        }
        else
        {
            scoreP2 = 0;
            verifVitoriaP1 = true;
            verifVitoriaP2 = false;
        }
        string placar = SceneUtility.GetScenePathByBuildIndex(18);
        GameObject.Find("MenuController").GetComponent<MenuController>().PreviousScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(placar);
        contabilizaMorteP1 = 0;
        contabilizaMorteP2 = 0;
    }
}
