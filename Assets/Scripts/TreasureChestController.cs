using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChestController : MonoBehaviour
{
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    public void AnimationOpen()
    {
        ani.SetBool("isOpen", true);
        if(transform.childCount == 2)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }

    }
}
