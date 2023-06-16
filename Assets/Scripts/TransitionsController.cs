using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionsController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void FadeOut()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("isFadeOut", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
