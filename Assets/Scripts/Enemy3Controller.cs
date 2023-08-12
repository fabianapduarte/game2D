using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Controller : MonoBehaviour
{
    private Animator ani;
    private int life = 4;
    private int dano = 2;
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
        float limite = 2.3f;

        //Utiliza ponto medio da boxCollider
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        BoxCollider2D colliderPlayer = player.GetComponent<BoxCollider2D>();
        Vector3 pontoMedioObjetoAtual = collider.bounds.center;
        Vector3 pontoMedioPlayer = colliderPlayer.bounds.center;
        float distanciaPontosMedios = Vector3.Distance(pontoMedioObjetoAtual, pontoMedioPlayer);

        if (distanciaPontosMedios > limite)
        {
            if (playerCheck == true && Vector3.Distance(transform.position, player.position) <= 12f)
            {
                ani.SetBool("isRunning", true);
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else
            {
                ani.SetBool("isRunning", false);
            }

            if (player.position.x < transform.position.x && !facingLeft)
            {
                Flip();
            }
            else if (player.position.x > transform.position.x && facingLeft)
            {
                Flip();
            }
        }

        else
        {
            ani.SetBool("isRunning", false);
            if (player.GetComponent<Animator>().GetBool("isAtacking") == false)
            {
                //Faz com que so ataque depois de um tempo
                if (contabilizaDano == 0)
                {
                    ani.SetBool("isAtacking", true);
                }
                Invoke("TimeTransitionAttack", 0.6f);
            }
        }
    }

    public void SetPlayerCheck(bool valor)
    {
        this.playerCheck = valor;
    }

    void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }

    public void Hurt(int danoPlayer)
    {
        life -= danoPlayer;
        ani.SetBool("isHurting", true);
        Invoke("TimeTransitionHurt", 0.4f);

        //Adiciona tempo de recuperacao pro inimigo
        contabilizaDano = 1;
        Invoke("Sleep", 1f);

        if (life <= 0)
        {
            ani.SetBool("isDead", true);
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            GetComponent<BoxCollider2D>().isTrigger = true;
            Destroy(gameObject, 1f);
        }
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

    private void Sleep()
    {
        contabilizaDano = 0;
        return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && collision.GetType() == typeof(BoxCollider2D))
        {
            if (contabilizaDano == 0)
            {
                GameObject.Find("AudioController").GetComponent<AudioController>().HurtPlayer();
                FindObjectOfType<GameController>().HurtPlayer(dano);
                contabilizaDano = 1;
                InputController controle = GameObject.Find("InputController").GetComponent<InputController>();
                controle.Vibrate(1f);
                Invoke("Sleep", 4f);
            }
        }
    }
}