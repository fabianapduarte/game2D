using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Controller : MonoBehaviour
{
    private Animator ani;
    private int enemyLife = 5;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Hurt(int dano)
    {
        enemyLife -= dano;
        if (enemyLife == 0)
        {
            ani.SetBool("isDead", true);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("player"))
        {
            if (ani.GetBool("isAtacking"))
            {
                FindObjectOfType<GameController>().HurtPlayer();
            }
        }
    }
}
