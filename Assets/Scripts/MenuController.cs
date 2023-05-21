using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    //Constantes de menu
    public const int play = 3;

    public static MenuController instance = null;
    private int previousSceneIndex;
    private string sceneName;

    private Button btnPlay;
    private Button btnQuit;
    private Button btn1;
    private Button btn2; 

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneName = SceneManager.GetActiveScene().name;
        if (scene.name == "Derrota" || scene.name == "Vitoria"){
            // Encontra o botão pelo nome ou através de uma busca na hierarquia
            btn1 = GameObject.Find("btn1").GetComponent<Button>();
            btn2 = GameObject.Find("btn2").GetComponent<Button>();
            btn1.onClick.AddListener(loadScene);
            btn2.onClick.AddListener(mainMenu);

            
        }

        if (scene.name == "MenuInicial"){
            btnPlay = GameObject.Find("PlayBtn").GetComponent<Button>();
            btnPlay.onClick.AddListener(PlayGame);
            btnQuit = GameObject.Find("ExitBtn").GetComponent<Button>();
            btnQuit.onClick.AddListener(QuitGame);
        }
    }

    // Update is called once per frame
    void Update(){

        if (sceneName == "Derrota" || sceneName == "Vitoria")
        {
            float A = Input.GetAxisRaw("Fire1");
            float B = Input.GetAxisRaw("Fire2");

            if (A > 0){
                //Reinicia ou avanca fase
                loadScene();
            }
            else if (B > 0){
                //Vai pro menu
                mainMenu();
            }
        }
    }
    public void loadScene()
    {
        if (SceneManager.GetActiveScene().name.Equals("Derrota"))
        {
            Debug.Log(previousSceneIndex);
            SceneManager.LoadScene(previousSceneIndex);
        }
        if (SceneManager.GetActiveScene().name.Equals("Vitoria"))
        {
            SceneManager.LoadScene(previousSceneIndex + 1);
        }

        Debug.Log("Carregando cena (repetindo/avancando)");
    }

    public void PreviousScene(Scene scene)
    {
        previousSceneIndex = scene.buildIndex;
    }

    public void mainMenu()
    {
        Debug.Log("Voltando pro menu inicial");
        SceneManager.LoadScene(0);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(play+1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
