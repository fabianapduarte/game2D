using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyControllerP1 : MonoBehaviour
{
    private Animator ani;
    private int life = 0;
    private int dano = 0;
    private int speed = 2;
    private Transform player;
    public bool facingLeft = true;
    private int contabilizaDano = 0;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        player = GameObject.Find("Jogador1").transform;
        if (gameObject.layer == 6)
        {
            life = 2; dano = 1;
        }
        if (gameObject.layer == 7)
        {
            life = 3; dano = 1;
        }
        if (gameObject.layer == 8)
        {
            life = 4; dano = 2;
        }
        if (gameObject.layer == 9)
        {
            life = 6; dano = 3;
        }
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
            ani.SetBool("isRunning", true);
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

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
                FindObjectOfType<MGameController>().HurtPlayer(dano, collision.gameObject);
                contabilizaDano = 1;
                InputController controle = GameObject.Find("InputController").GetComponent<InputController>();
                controle.Vibrate(2f);
                Invoke("Sleep", 2f);
            }
        }
    }
}
