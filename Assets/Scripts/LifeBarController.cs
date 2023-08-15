using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBarController : MonoBehaviour
{
    public void InitSlider(int value)
    {
        if(value == 1)
        {
            GetComponent<Slider>().value = 10;
        }else if(value == 2)
        {
            GetComponent<Slider>().value = 20;
        }else if(value == 3)
        {
                GetComponent<Slider>().value = 30;
        } else if(value == 4)
        {
            GetComponent<Slider>().value = 35;
        }
    }

    public void SetLifeBar(int value)
    {
        GetComponent<Slider>().value -= value;
    }

    public float GetValueLifeBar()
    {
        return GetComponent<Slider>().value;
    }
}
