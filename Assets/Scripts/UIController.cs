using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    private GameObject atual;
    public GameObject MainMenu;
    public GameObject ConfiguracoesMenu;

    //Cores
    Color32 corBase = new Color32(80, 80, 80, 255);
    Color32 corDeSelecao = new Color32(40, 40, 40, 255);

    void Start(){
        MainMenu.SetActive(true);
        ConfiguracoesMenu.SetActive(false);
    }

    public void selectMain(){
        GameObject button = GameObject.Find("ConfiguracoesBtn");
        EventSystem.current.SetSelectedGameObject(button);
    }
    public void selectOpcoes(){
        GameObject button = GameObject.Find("SliderVolume");
        EventSystem.current.SetSelectedGameObject(button);
    }

    public void hover(GameObject btn){
        EventSystem.current.SetSelectedGameObject(btn);
    }

    void Update(){
        GameObject btnSelected = EventSystem.current.currentSelectedGameObject;

        //Corrige clique na tela
        if(btnSelected == null && atual != null){
            btnSelected = atual;
            EventSystem.current.SetSelectedGameObject(btnSelected);
        }

        //Destaca opcao
        if (atual != btnSelected){
            //Slider e excecoes
            if(btnSelected != null && btnSelected.name == "SliderVolume"){
                btnSelected = btnSelected.transform.parent.gameObject;
            }

            TextMeshProUGUI[] btns = FindObjectsOfType<TextMeshProUGUI>();
            //Debug.Log(btns.Length);
            foreach (TextMeshProUGUI btn in btns){
                btn.color = corBase;
            }
            //Debug.Log(btnSelected + " was selected");
            TextMeshProUGUI texto = btnSelected.GetComponentInChildren<TextMeshProUGUI>();
            texto.color = corDeSelecao;

            //Espada
            Image indicador = null;
            GameObject obj = GameObject.Find("IndicadorMenu");
            if (obj != null){
                indicador = obj.GetComponent<Image>();
            }
            Vector3 posicaoBtn = new Vector3(indicador.rectTransform.position.x, btnSelected.transform.position.y, 0);
            indicador.rectTransform.position = posicaoBtn;
            atual = btnSelected;

            //Aqui vem o som de troca
        }

        //B pra voltar
        float back = Input.GetAxisRaw("Fire2");
        if (back > 0 && ConfiguracoesMenu.activeSelf){
            MainMenu.SetActive(true);
            ConfiguracoesMenu.SetActive(false);
            selectMain();
        }
    }
}
