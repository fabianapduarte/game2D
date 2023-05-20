using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4Controller : MonoBehaviour
{
    private Animator ani;
    private int life = 6;
    private int dano = 3;
    private int speed = 2;
    private Transform player;

    private int contabilizaDano = 0;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        player = GameObject.Find("player").transform;
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) > 2f)
        {
            ani.SetBool("isRunning", true);
            ani.SetBool("isAtacking", false);
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        }
        else
        {
            ani.SetBool("isRunning", false);
            ani.SetBool("isAtacking", true);
        }
    }

    private void Flip(bool isFliped)
    {
        float scaleX = Mathf.Abs(transform.localScale.x);
        if (isFliped)
        {
            scaleX *= -1;
        }
        transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
    }

    public void Hurt(int danoPlayer)
    {
        Debug.Log("Acertou");
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
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (contabilizaDano == 0)
            {
                Hurt(FindObjectOfType<GameController>().GetDanoPlayer());
                //FindObjectOfType<GameController>().HurtPlayer(dano);
                contabilizaDano = 1;
                Invoke("Sleep", 1.5f);
            }
        }
    }
}