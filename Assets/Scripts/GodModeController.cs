using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.IO;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;

public class GodModeController : MonoBehaviour
{
    private Button menuDePauseBtn;
    public GameObject GodMode;
    public GameObject Pause;
    private string sceneName;
    private GameObject buttonContinuarSelecao;
    private Button buttonContinuarGm, prevLvl, nextLvl, zonesBtn, limparSaveBtn;

    private int gmCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        buttonContinuarSelecao = GameObject.Find("ContinuarGMBtn");
        buttonContinuarGm = GameObject.Find("ContinuarGMBtn").GetComponent<Button>();
        zonesBtn = GameObject.Find("ZonaBtn").GetComponent<Button>();
        prevLvl = GameObject.Find("pvFaseBtn").GetComponent<Button>();
        nextLvl = GameObject.Find("nxFaseBtn").GetComponent<Button>();
        limparSaveBtn = GameObject.Find("limparSaveBtn").GetComponent<Button>();
        GodMode.SetActive(false);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneName = SceneManager.GetActiveScene().name;
        //setar menu de pause
        if (sceneName != "MenuInicial" && sceneName != "Derrota" && sceneName != "Vitoria")
        {
            GodMode.SetActive(true);
            while (!GodMode.activeSelf)
            { // Aguarda ate que o menu de pause esteja ativo

            }
            buttonContinuarGm.onClick.AddListener(resumeGM);
            zonesBtn.onClick.AddListener(DisableZones);
            prevLvl.onClick.AddListener(prevLvlFunc);
            nextLvl.onClick.AddListener(nextLvllFunc);
            limparSaveBtn.onClick.AddListener(deletePlayerPrefs);
            GodMode.SetActive(false);
        }
    }

    public void cancelaGM(){
        gmCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //FindObjectOfType<GameController>().LevelEnd();
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "MenuInicial" && GodMode.activeSelf)
        {
            GodMode.SetActive(false);
        }

        //Coloca o jogo em pause
        if (sceneName != "MenuInicial" && sceneName != "Derrota" && sceneName != "Vitoria" && sceneName != "Cutscene1" && sceneName != "Cutscene2")
        {
            if (Input.GetButtonDown("GodMode") && !Pause.activeSelf)
            {
                CancelInvoke("cancelaGM");
                gmCount++;
                Invoke("cancelaGM", 1f);
                if(gmCount == 2){
                    GodMode.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(buttonContinuarSelecao);
                    Time.timeScale = 0f;
                }
                
            }
            //B pra voltar
            if (GodMode.activeSelf == true)
            {
                float back = Input.GetAxisRaw("Fire2");
                if (back > 0)
                {
                    resumeGM();
                }
            }

        }
    }

    public void resumeGM()
    {
        PlayerController player = GameObject.Find("Simetra").GetComponent<PlayerController>();
        player.resetaInputPulo();
        gmCount = 0;
        GodMode.SetActive(false);
        Time.timeScale = 1f;
    }

    public void prevLvlFunc()
    {
        resumeGM();
        PlayerPrefs.SetFloat("saveX", 0f);
        PlayerPrefs.SetFloat("saveY", 0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void nextLvllFunc()
    {
        resumeGM();
        PlayerPrefs.SetFloat("saveX", 0f);
        PlayerPrefs.SetFloat("saveY", 0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void DisableZones()
    {
        GameObject[] zones;
        GameObject[] zoneFinish;
        GameObject[] zoneInit;
        zones = GameObject.FindGameObjectsWithTag("zone");
        zoneFinish = GameObject.FindGameObjectsWithTag("zoneFinish");
        zoneInit = GameObject.FindGameObjectsWithTag("zoneInit");
        foreach (GameObject zone in zones)
        {
            zone.SetActive(false);
        }

        foreach (GameObject zone in zoneFinish)
        {
            zone.SetActive(false);
        }

        foreach (GameObject zone in zoneInit)
        {
            zone.SetActive(false);
        }

        if (SceneManager.GetActiveScene().name.Equals("LevelOne"))
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().ResetCoordX(1);
        }
        else if (SceneManager.GetActiveScene().name.Equals("LevelTwo"))
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().ResetCoordX(2);
        }
        else if (SceneManager.GetActiveScene().name.Equals("LevelTree"))
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().ResetCoordX(3);
        }
        else if (SceneManager.GetActiveScene().name.Equals("LevelFour"))
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().ResetCoordX(4);
        }
        else if (SceneManager.GetActiveScene().name.Equals("LevelFive"))
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().ResetCoordX(5);
        }
        else if (SceneManager.GetActiveScene().name.Equals("Acidron1"))
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().ResetCoordX(6);
        }
        GameObject.Find("AudioController").GetComponent<AudioController>().BattleSoundFinish();
        resumeGM();
    }

    public void deletePlayerPrefs()
    {
        PlayerPrefs.SetInt("FaseAtual", 0);
        PlayerPrefs.SetFloat("saveX", 0f);
        PlayerPrefs.SetFloat("saveY", 0f);

        MenuController menuController = FindObjectOfType<MenuController>();
        menuController.indexCena = 2;
        menuController.play = 3;
        resumeGM();
    }
}