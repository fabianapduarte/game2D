using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{

    private GameObject atual;
    void Update(){
        Color32 corBase = new Color32(80, 80, 80, 255);
        Color32 corDeSelecao = new Color32(40, 40, 40, 255);

        GameObject btnSelected = EventSystem.current.currentSelectedGameObject;

        if (atual != btnSelected){
            TextMeshProUGUI[] btns = FindObjectsOfType<TextMeshProUGUI>();
            //Debug.Log(btns.Length);
            foreach (TextMeshProUGUI btn in btns){
                btn.color = corBase;
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
        }
        atual = btnSelected;
    }
}
