using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    private GameObject botaoAtual;
    public GameObject MainMenu;
    public GameObject ConfiguracoesMenu;
    public TextMeshProUGUI dica;
    public bool telaDeEstadoFinal;
    
    private string[] dicas = new string[]
    {
        "Espere a plataforma se aproximar para pular nela, o pulo duplo não vai salvar sua vida.",
        "Tente atacar e se afastar do inimigo antes de dar o próximo golpe para não perder vidas.",
        "Os baús são importantes, não os deixe para traz."
    };

    //Cores
    Color corBase;
    Color corDeSelecao;
    InputController entrada = null;

    private Color colorRGB(int r, int g, int b){
        return new Color(r / 255f, g / 255f, b / 255f);
    }

    void Start(){
        corBase = colorRGB(99, 90, 86);
        corDeSelecao = colorRGB(53, 47, 44);
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

    public void menu(){
        MainMenu.SetActive(true);
        ConfiguracoesMenu.SetActive(false);
    }

    public void selectMain(){
        //GameObject button = GameObject.Find("ConfiguracoesBtn");
        GameObject button = GameObject.Find("PlayBtn");
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
                Vector3 posicaoBtn = new Vector3(btnSelected.transform.position.x-70, btnSelected.transform.position.y, 0);
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

    void Update(){
        if(entrada == null){
            //entrada = GetComponent<InputController>();
            entrada = FindObjectOfType<InputController>();
        }

        //Debug.Log(tela.name);
        select();
    }
}
