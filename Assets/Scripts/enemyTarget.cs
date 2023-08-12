using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTarget : MonoBehaviour
{
    private Animator ani;
    private int life = 2;
    private int dano = 1;
    private int speed = 2;
    private Transform player;
    public bool facingLeft = true;
    private bool playerCheck = true;

    private int contabilizaDano = 0;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        player = GameObject.Find("Simetra").transform;
    }
    private void Update()
    {
        if (life <= 0)
        {
            ani.SetBool("isDead", true);
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            GetComponent<BoxCollider2D>().isTrigger = true;
            Destroy(gameObject, 1f);
        }
    }

    public void Hurt(int danoPlayer)
    {
        // life -= danoPlayer;
        ani.SetBool("isHurting", true);
        Invoke("TimeTransitionHurt", 0.4f);

        //Adiciona tempo de recuperacao pro inimigo
        contabilizaDano = 1;
        Invoke("Sleep", 1f);


    }

    public void destroiAlvo()
    {
        life = 0;
    }

    private void TimeTransitionHurt()
    {
        ani.SetBool("isHurting", false);
        return;
    }

    private void TimeTransitionAttack()
    {
        ani.SetBool("isAtacking", false);
        return;
    }

    public int GetDano()
    {
        return dano;
    }
}