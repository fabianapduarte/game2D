using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBarController : MonoBehaviour
{
    public int vidaTotal = 50;

    void Start()
    {
        GetComponent<Slider>().value = 1;
    }

    public void SetLifeBar(float value)
    {
        float dano = ((GetComponent<Slider>().value*vidaTotal) - value)/vidaTotal;
        GetComponent<Slider>().value = dano;
    }

    public float GetValueLifeBar()
    {
        return GetComponent<Slider>().value;
    }
}
