using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : MonoBehaviour
{
    private Animator ani;
    private int enemyLife = 3;
    private int dano = 1;

    private int contabilizaDano = 0;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hurt(int danoPlayer)
    {
        Debug.Log("Acertou");
        enemyLife -= danoPlayer;
        ani.SetBool("isHurting", true);
        Invoke("TimeTransitionHurt", 0.4f);
        if (enemyLife <= 0)
        {
            ani.SetBool("isDead", true);
            Destroy(gameObject, 1f);
        }
    }

    private void TimeTransitionHurt()
    {
        ani.SetBool("isHurting", false);
        return;
    }

    private void Sleep()
    {
        contabilizaDano = 0;
        return;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.tag.Equals("Player"))
        {
            if (contabilizaDano == 0)
            {
                FindObjectOfType<GameController>().HurtPlayer(dano);
                dano = 1;
                Invoke("Sleep", 1.5f);
            }
        }*/
    }
}
