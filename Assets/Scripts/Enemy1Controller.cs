using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : MonoBehaviour
{
    //private Animator ani;
    private int enemyLife = 2;

    private int dano = 0;
    // Start is called before the first frame update
    void Start()
    {
        //ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hurt(int dano)
    {
        enemyLife -= dano;
        Invoke("Sleep", 1.5f);
        if (enemyLife == 0)
        {
            //ani.SetBool("isDead", true);
            Destroy(gameObject, 1.5f);
        }
    }

    private void Sleep()
    {
        dano = 0;
        return;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            /*if (ani.GetBool("isAtacking"))
            {
                FindObjectOfType<GameController>().HurtPlayer();
            }*/
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            //ani.SetBool("isHurting", true);
            if(dano == 0)
            {
                Debug.Log("Acertou!!");
                Hurt(FindObjectOfType<GameController>().GetDanoPlayer());
                dano = 1;
            }
            //ani.SetBool("isHurting", false);
        }
    }
}
