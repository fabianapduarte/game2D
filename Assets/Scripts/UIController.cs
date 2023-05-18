using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    private GameObject botaoAtual;
    public GameObject MainMenu;
    public GameObject ConfiguracoesMenu;
    public TextMeshProUGUI dica;
    public bool telaDeEstadoFinal;
    
    private string[] dicas = new string[]
    {
        "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
        "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
        "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
        "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
    };

    //Cores
    Color32 corBase = new Color32(80, 80, 80, 255);
    Color32 corDeSelecao = new Color32(40, 40, 40, 255);
    InputController entrada = null;

    void Start(){
        if (MainMenu == null){
            if (dica != null){
                setarDica();
            }
            return;
        }
        menu();
    }

    private void setarDica()
    {
        int numero = Random.Range(0, dicas.Length);
        dica.text = dicas[numero];
        //Debug.Log(dica);
    }

    private void menu(){
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

    private void select(){
        GameObject btnSelected = EventSystem.current.currentSelectedGameObject;

        //Corrige clique na tela
        if (btnSelected == null && botaoAtual != null){
            btnSelected = botaoAtual;
            EventSystem.current.SetSelectedGameObject(btnSelected);
        }

        //Destaca opcao
        if (botaoAtual != btnSelected)
        {
            //Slider e excecoes
            if (btnSelected != null && btnSelected.name == "SliderVolume"){
                btnSelected = btnSelected.transform.parent.gameObject;
            }

            Button[] btns = FindObjectsOfType<Button>();
            //TextMeshProUGUI[] btns = FindObjectsOfType<TextMeshProUGUI>();
            //Debug.Log(btns.Length);

            //Icone de espada
            if (!telaDeEstadoFinal){
                foreach (Button btn in btns){
                    TextMeshProUGUI btnText = btn.GetComponentInChildren<TextMeshProUGUI>();
                    btnText.color = corBase;
                }
                //Debug.Log(btnSelected + " was selected");
                TextMeshProUGUI texto = btnSelected.GetComponentInChildren<TextMeshProUGUI>();
                texto.color = corDeSelecao;

                Image indicador = null;
                GameObject obj = GameObject.Find("IndicadorMenu");
                if (obj != null){
                    indicador = obj.GetComponent<Image>();
                }
                Vector3 posicaoBtn = new Vector3(indicador.rectTransform.position.x, btnSelected.transform.position.y, 0);
                indicador.rectTransform.position = posicaoBtn;
                botaoAtual = btnSelected;
            }else{
                if (entrada.gamepadOn == false){
                    foreach (Button btn in btns){
                        TextMeshProUGUI btnText = btn.GetComponentInChildren<TextMeshProUGUI>();
                        btnText.color = corBase;
                    }
                    //Debug.Log(btnSelected + " was selected");
                    TextMeshProUGUI texto = btnSelected.GetComponentInChildren<TextMeshProUGUI>();
                    texto.color = corDeSelecao;
                }else{
                    foreach (Button btn in btns){
                        TextMeshProUGUI btnText = btn.GetComponentInChildren<TextMeshProUGUI>();
                        btnText.color = corDeSelecao;
                    }
                }
            }

            //Aqui vem o som de troca
        }
    }

    public void loadScene(){
        Debug.Log("Carregando cena (repetindo/avancando)");
    }

    public void mainMenu(){
        Debug.Log("Voltando pro menu inicial");
    }

    void Update(){
        if(entrada == null){
            //entrada = GetComponent<InputController>();
            entrada = FindObjectOfType<InputController>();
        }

        //Debug.Log(tela.name);
        select();

        //icones dinamicos
        //dinamicIcons();

        //Tela de EstadoFinal (vitoria/derrota)
        if (telaDeEstadoFinal){
            if (Input.GetKey(KeyCode.LeftArrow)){
                GameObject button = GameObject.Find("btn1");
                EventSystem.current.SetSelectedGameObject(button);
            } else if (Input.GetKey(KeyCode.RightArrow)){
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
        else{
            //B pra voltar
            float back = Input.GetAxisRaw("Fire2");
            if (back > 0 && ConfiguracoesMenu.activeSelf){
                menu();
                selectMain();
            }
        }
    }
}
