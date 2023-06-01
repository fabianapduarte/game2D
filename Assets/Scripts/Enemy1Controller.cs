using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Enemy1Controller : MonoBehaviour
{
    private Animator ani;
    private int life = 2;
    private int dano = 1;
    private int speed = 2;
    private Transform player;
    public bool facingLeft = true;

    private int contabilizaDano = 0;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        player = GameObject.Find("player").transform;
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) > 2.3f)
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
                ani.SetBool("isAtacking", true);
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
        if (collision.gameObject.tag.Equals("Player") && ani.GetBool("isAtacking"))
        {
            if (contabilizaDano == 0)
            {
                GameObject.Find("AudioController").GetComponent<AudioController>().HurtPlayer();
                FindObjectOfType<GameController>().HurtPlayer(dano);
                contabilizaDano = 1;
                InputController controle = GameObject.Find("InputController").GetComponent<InputController>();
                controle.Vibrate(2f);
                Invoke("Sleep", 2f);
            }
        }
    }
}
