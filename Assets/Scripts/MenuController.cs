using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.IO;
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

    private Button continuarBtn;
    private Button configuracoesBtn;
    private Button menuInicialBtn;
    private Button menuDePauseBtn;

    private GameObject configMenuPausa;
    private GameObject menuPausa;
    private GameObject buttonContinuar;

    public GameObject pause;
    private AudioController controleDeAudio;
    private int indexCena;

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

        pause.SetActive(false);

        indexCena = 2;
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
            controleDeAudio = GameObject.Find("AudioController").GetComponent<AudioController>();
            pause.SetActive(true);
            configMenuPausa.SetActive(true);
            GameObject volume = GameObject.Find("SliderVolume");
            Slider slider = volume.GetComponent<Slider>();
            slider.value = controleDeAudio.GetVolume();

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
    

    // Update is called once per frame
    void Update(){
        //Coloca o jogo em pause
        if (sceneName != "MenuInicial" && sceneName != "Derrota" && sceneName != "Vitoria" && sceneName != "Cutscene1"){
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

        if (sceneName == "Cutscene1"){
            GameObject videoPanel = GameObject.Find("VideoPanel");
            VideoPlayer videoPlayer = videoPanel.GetComponent<VideoPlayer>();
            //Debug.Log(videoPlayer.clip.name);

            //A/ENTER pra avancar
            bool forward1 = Input.GetButtonDown("Fire1");
            bool forward2 = Input.GetKeyDown(KeyCode.Return);
            if (forward1 || forward2){
                if(indexCena == 10){
                    SceneManager.LoadScene(play+1);
                }

                string cena = "Cena" + (indexCena++);
                videoPlayer.clip = (VideoClip)Resources.Load(cena);
                videoPlayer.isLooping = true;
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
            string nameScene = SceneUtility.GetScenePathByBuildIndex(previousSceneIndex);
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
        resume();
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
        SceneManager.LoadScene(play);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
