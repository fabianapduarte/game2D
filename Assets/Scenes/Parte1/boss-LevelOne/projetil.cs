using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projetil : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void dispara()
    {
        GameObject projectile = GameObject.Find("projetil");
        Animator projectileAnimator = projectile.GetComponent<Animator>();
        if (projectileAnimator != null)
        {
            projectileAnimator.SetBool("isShooting", true);
        }
        Invoke("recarrega", .9f);
    }

    void recarrega()
    {
        GameObject projectile = GameObject.Find("projetil");
        Animator projectileAnimator = projectile.GetComponent<Animator>();
        if (projectileAnimator != null)
        {
            projectileAnimator.SetBool("isShooting", false);
        }
    }
}
