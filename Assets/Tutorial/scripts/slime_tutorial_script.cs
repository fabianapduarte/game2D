using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slime_tutorial_script : MonoBehaviour
{
    private Animator ani;
    private int life = 1;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        player = GameObject.Find("Simetra").transform;
    }

    // Update is called once per frame
    void Update()
    {
    
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
             Hurt(FindObjectOfType<GameController>().GetDanoPlayer());
             Invoke("Sleep", 1.5f);
        }
    }
}
