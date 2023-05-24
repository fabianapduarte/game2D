using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    //Constantes de menu
    public const int play = 3;

    public static MenuController instance = null;
    private int previousSceneIndex;
    private string sceneName;
    private bool isPause = false; 

    private Button btnPlay;
    private Button btnQuit;

    private Button avancar;
    private Button sair;

    private static Button continuarBtn;
    private static Button configuracoesBtn;
    private static Button menuInicialBtn;
    private static Button menuDePauseBtn;

    private static GameObject configMenuPausa;
    private static GameObject menuPausa;
    private GameObject buttonContinuar;

    public GameObject pause;

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

        menuDePauseBtn = GameObject.Find("VoltarBtn").GetComponent<Button>();
        continuarBtn = GameObject.Find("ContinuarBtn").GetComponent<Button>();
        configuracoesBtn = GameObject.Find("ConfigBtn").GetComponent<Button>();
        menuInicialBtn = GameObject.Find("MenuBtn").GetComponent<Button>();

        configMenuPausa = GameObject.Find("configMenu");
        menuPausa = GameObject.Find("pauseMenu");
        buttonContinuar = GameObject.Find("ContinuarBtn");
    }

    private void OnEnable(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //Define coisas para cenas especificas quando estas sao carregadas - util demais
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneName = SceneManager.GetActiveScene().name;
        if (scene.name == "Derrota" || scene.name == "Vitoria"){
            // Encontra o botão pelo nome ou através de uma busca na hierarquia
            avancar = GameObject.Find("btn1").GetComponent<Button>();
            sair = GameObject.Find("btn2").GetComponent<Button>();
            avancar.onClick.AddListener(loadScene);
            sair.onClick.AddListener(mainMenu);
        }

        if (scene.name == "MenuInicial"){
            btnPlay = GameObject.Find("PlayBtn").GetComponent<Button>();
            btnPlay.onClick.AddListener(PlayGame);
            btnQuit = GameObject.Find("ExitBtn").GetComponent<Button>();
            btnQuit.onClick.AddListener(QuitGame);
        }


        //setar menu de pause
        if (sceneName != "MenuInicial" && sceneName != "Derrota" && sceneName != "Vitoria"){
            pause.SetActive(true);
            configMenuPausa.SetActive(true);
            //StartCoroutine(WaitForPauseMenuActive());

            while (!pause.activeSelf && !buttonContinuar.activeSelf)
            { // Aguarda até que o menu de pause esteja ativo
                
            }
            menuPause(buttonContinuar);
            continuarBtn.onClick.AddListener(resume);
            configuracoesBtn.onClick.AddListener(configPause);
            menuInicialBtn.onClick.AddListener(mainMenu);
            menuDePauseBtn.onClick.AddListener(menuPause);
            configMenuPausa.SetActive(false);
            pause.SetActive(false);
        }
    }

    IEnumerator WaitForPauseMenuActive()
    {
        while (!pause.activeSelf && !buttonContinuar.activeSelf){ // Aguarda até que o menu de pause esteja ativo
            yield return null;
        }
        Debug.Log("Menu de pause está ativo!");
    }

    

    // Update is called once per frame
    void Update(){
        //Coloca o jogo em pause
        if (sceneName != "MenuInicial" && sceneName != "Derrota" && sceneName != "Vitoria"){
            float pausar = Input.GetAxisRaw("Pause");
            if (pausar>0){
                pause.SetActive(true);
                EventSystem.current.SetSelectedGameObject(buttonContinuar);
                Time.timeScale = 0f;
                isPause = true;
                menuPause();
            }
            //B pra voltar
            if(pause.activeSelf == true){
                float back = Input.GetAxisRaw("Fire2");
                if (back > 0){
                    GameObject configDePausa = GameObject.Find("configMenu");
                    if(configDePausa != null){
                        configMenuPausa.SetActive(false);
                        menuPausa.SetActive(true);
                    }
                    resume();
                }
            }
            
        }

        if (sceneName == "MenuInicial"){
            //B pra voltar
            float back = Input.GetAxisRaw("Fire2");
            if (back > 0){
                // && ConfiguracoesMenu.activeSelf
                GameObject menuPrincipal = GameObject.Find("UIController");
                UIController uiController = menuPrincipal.GetComponent<UIController>();
                uiController.menu();
                uiController.selectMain();
            }
        }

        if (sceneName == "Derrota" || sceneName == "Vitoria"){
            if (Input.GetKey(KeyCode.LeftArrow)){
                GameObject button = GameObject.Find("btn1");
                EventSystem.current.SetSelectedGameObject(button);
            }
            else if (Input.GetKey(KeyCode.RightArrow)){
                GameObject button = GameObject.Find("btn2");
                EventSystem.current.SetSelectedGameObject(button);
            }

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
            string nameScene = SceneUtility.GetScenePathByBuildIndex(previousSceneIndex);
            Debug.Log(nameScene);
            SceneManager.LoadScene(nameScene);
        }
        if (SceneManager.GetActiveScene().name.Equals("Vitoria"))
        {
            SceneManager.LoadScene(previousSceneIndex + 1);
        }

        //Debug.Log("Carregando cena (repetindo/avancando)");
    }

    public void PreviousScene(int index)
    {
        previousSceneIndex = index;
    }

    public void mainMenu()
    {
        Debug.Log("Voltando pro menu inicial");
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void resume()
    {
        pause.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
    }

    public void configPause(){
        GameObject button = GameObject.Find("VolumeBtn");
        EventSystem.current.SetSelectedGameObject(button);
    }

    public void menuPause(GameObject buttonContinuar)
    {
        GameObject button = GameObject.Find("ContinuarBtn");
        EventSystem.current.SetSelectedGameObject(buttonContinuar);
    }

    public void menuPause()
    {
        GameObject button = GameObject.Find("ContinuarBtn");
        EventSystem.current.SetSelectedGameObject(button);
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
