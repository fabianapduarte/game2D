using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemyController : MonoBehaviour
{
    private float speed = 2;
    public bool wall = false;

    public Transform wallCheck;
    public LayerMask wallLayer;

    public bool facingLeft = true;

    private Animator ani;
    private int life = 3;
    private int dano = 1;
    private Transform player;

    private int contabilizaDano = 0;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        player = GameObject.Find("Simetra").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (CheckWallCollision())
        {
            speed *= -1;
        }

        if (player.position.x < transform.position.x && !facingLeft)
        {
            Flip();
        }
        else if (player.position.x > transform.position.x && facingLeft)
        {
            Flip();
        }

        if (speed > 0 && !facingLeft)
        {
            Flip();
        }
        else if (speed < 0 && facingLeft)
        {
            Flip();
        }

        if (Vector3.Distance(transform.position, player.position) > 2.3f)
        {
            ani.SetBool("isRunning", true);
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
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

    bool CheckWallCollision()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(wallCheck.position, new Vector2(0.1f, 0.1f), 0f, wallLayer);
        return colliders.Length > 0;
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
        //Debug.Log("Acertou");
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
                FindObjectOfType<GameController>().HurtPlayer(dano);
                contabilizaDano = 1;
                InputController controle = GameObject.Find("InputController").GetComponent<InputController>();
                controle.Vibrate(2f);
                Invoke("Sleep", 2f);
            }
        }
    }
}
