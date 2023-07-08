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
    private Button buttonContinuarGm, prevLvl, nextLvl, zonesBtn;

    // Start is called before the first frame update
    void Start()
    {
        buttonContinuarSelecao = GameObject.Find("ContinuarGMBtn");
        buttonContinuarGm = GameObject.Find("ContinuarGMBtn").GetComponent<Button>();
        zonesBtn = GameObject.Find("ZonaBtn").GetComponent<Button>();
        prevLvl = GameObject.Find("pvFaseBtn").GetComponent<Button>();
        nextLvl = GameObject.Find("nxFaseBtn").GetComponent<Button>();
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
            Debug.Log("aaa");
            buttonContinuarGm.onClick.AddListener(resumeGM);
            zonesBtn.onClick.AddListener(DisableZones);
            prevLvl.onClick.AddListener(prevLvlFunc);
            nextLvl.onClick.AddListener(nextLvllFunc);
            GodMode.SetActive(false);
        }
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
            float gm = Input.GetAxisRaw("GodMode");
            if (gm > 0 && !Pause.activeSelf)
            {
                GodMode.SetActive(true);
                EventSystem.current.SetSelectedGameObject(buttonContinuarSelecao);
                Time.timeScale = 0f;
                //menuPause();
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
        GodMode.SetActive(false);
        Time.timeScale = 1f;
    }

    public void prevLvlFunc()
    {
        resumeGM();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void nextLvllFunc()
    {
        resumeGM();
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
        GameObject.Find("AudioController").GetComponent<AudioController>().BattleSoundFinish();
        resumeGM();
    }
}
