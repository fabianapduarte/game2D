using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBarController : MonoBehaviour
{
    void Start()
    {
        GetComponent<Slider>().value = 1;
    }

    public void SetLifeBar(float value)
    {
        GetComponent<Slider>().value -= value;
    }

    public float GetValueLifeBar()
    {
        return GetComponent<Slider>().value;
    }
}
