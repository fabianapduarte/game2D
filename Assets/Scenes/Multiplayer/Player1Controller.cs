using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player1Controller : MonoBehaviour
{
    private float speed = 6;
    private int danoPlayer = 1;
    public float jumpForce;
    public bool doubleJump;

    private Rigidbody2D rb;
    private bool playerInFloor = false;
    private Animator ani;
    private int contabilizaDano = 0;
    private int contabilizaColeta = 0;

    private bool isClimbing;
    private bool isLadder;

    public Transform detectFloor;
    public LayerMask isFloor;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (FindObjectOfType<MGameController>().GetSavePoint(gameObject) != Vector3.zero)
        {
            transform.position = FindObjectOfType<MGameController>().GetSavePoint(gameObject);
            FindObjectOfType<MGameController>().SetContabilizaBonusForce(gameObject);
            SetSpeed(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("JumpP1"))
        {
            if (playerInFloor)
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                GameObject.Find("AudioController").GetComponent<AudioController>().Jump();
                playerInFloor = false;
                doubleJump = true;
            }
            else if (!playerInFloor && doubleJump == true)
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                GameObject.Find("AudioController").GetComponent<AudioController>().Jump();
                playerInFloor = false;
                doubleJump = false;
            }
            ani.SetBool("isJumping", true);
        }
        else
        {
            //ani.SetBool("isJumping", false);
        }

        if (Input.GetButtonDown("AtaqueP1"))
        {
            ani.SetBool("isAtacking", true);
            GameObject.Find("AudioController").GetComponent<AudioController>().Attack1();
        }
        else
        {
            ani.SetBool("isAtacking", false);
        }

        if (playerInFloor)
        {
            ani.SetBool("isJumping", false);
        }

        if (isLadder && Math.Abs(vertical) > 0f)
        {
            ani.SetBool("isLadder", true);
            isClimbing = true;
        }
    }

    public bool GetStatusInFloor()
    {
        return playerInFloor;
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

    private void FixedUpdate()
    {
        // Movimentacao
        float horizontal = Input.GetAxisRaw("HorizontalP1");
        float vertical = Input.GetAxisRaw("VerticalP1");
        rb.velocity = new Vector3(speed * horizontal, rb.velocity.y, 0f);
        if (horizontal > 0)
        {
            ani.SetBool("isRunning", true);
            Flip(false);
        }
        else if (horizontal < 0)
        {
            ani.SetBool("isRunning", true);
            Flip(true);
        }
        else
        {
            ani.SetBool("isRunning", false);

        }

        Animator animator = GetComponent<Animator>();
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (isClimbing)
        {
            //ani.SetBool("isLadder", true);
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
            //congela frame caso a ela esteja parada

            if (vertical == 0f && stateInfo.IsName("Ladder"))
            {
                animator.speed = 0;
            }
            else
            {
                animator.speed = 1;
            }
        }
        else
        {
            animator.speed = 1;
            rb.gravityScale = 1f;
        }

        if ((stateInfo.IsName("Attack") || stateInfo.IsName("Spell")) && playerInFloor)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0f);
        }

        playerInFloor = Physics2D.OverlapBox(detectFloor.position, new Vector2(0.16739f, 0.16739f), 0f, isFloor);
        /*playerInFloor = Physics2D.OverlapCircle(detectFloor.position, 0.0887f, isFloor);*/
    }

    private void TimeTransitionHurt()
    {
        ani.SetBool("isHurting", false);
        return;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3) // floor
        {
            playerInFloor = true;
            doubleJump = false;
        }

        if (collision.gameObject.tag.Equals("Finish"))
        {
            FindObjectOfType<GameController>().LevelEnd();
        }

    }

    public void AnimationDeadPlayer()
    {
        ani.SetBool("isDead", true);
    }

    public void AnimationHurtPlayer()
    {
        ani.SetBool("isHurting", true);
        Invoke("TimeTransitionHurt", 0.4f);
    }

    private void Sleep()
    {
        contabilizaDano = 0;
        return;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(float valor)
    {
        speed += valor;
    }

    public int GetDano()
    {
        return danoPlayer;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Animator animator = GetComponent<Animator>();
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (collision.gameObject.CompareTag("Enemy") && stateInfo.IsName("Attack"))
        {
            if (contabilizaDano == 0)
            {
                GameObject.Find("AudioController").GetComponent<AudioController>().HurtEnemy();
                FindObjectOfType<MGameController>().HurtEnemy(collision.gameObject, gameObject);
                contabilizaDano = 1;
                Invoke("Sleep", 1.5f);
                InputController controle = GameObject.Find("InputController").GetComponent<InputController>();
                controle.Vibrate(0.3f);
            }
        }
        if (collision.gameObject.CompareTag("Enemy") && stateInfo.IsName("Spell"))
        {
            if (contabilizaDano == 0)
            {
                GameObject.Find("AudioController").GetComponent<AudioController>().HurtEnemy();
                FindObjectOfType<MGameController>().HurtEnemy(collision.gameObject, gameObject);
                contabilizaDano = 1;
                Invoke("Sleep", 1.5f);
                InputController controle = GameObject.Find("InputController").GetComponent<InputController>();
                controle.Vibrate(0.3f);
            }
        }

        if (collision.gameObject.CompareTag("ladder"))
        {
            isLadder = true;
        }

        if (collision.gameObject.CompareTag("water"))
        {
            ani.SetBool("isDead", true);
            FindObjectOfType<MGameController>().DeadPlayer(gameObject);
        }

        if (collision.gameObject.CompareTag("lava"))
        {
            ani.SetBool("isDead", true);
            FindObjectOfType<MGameController>().DeadPlayer(gameObject);
        }

        if (collision.gameObject.CompareTag("savepoint"))
        {
            FindObjectOfType<MGameController>().SetSafePoint(collision.gameObject, gameObject);
        }
    }
    public void SetDanoPlayer(int valor)
    {
        danoPlayer += valor;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("collectibles"))
        {
            contabilizaColeta = 0;
        }

        if (collision.gameObject.CompareTag("ladder"))
        {
            ani.SetBool("isLadder", false);
            isLadder = false;
            isClimbing = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("treasureChest"))
        {
            if (Input.GetButtonDown("Open"))
            {
                collision.GetComponent<TreasureChestController>().AnimationOpen();
            }
        }

        if (collision.gameObject.CompareTag("collectibles"))
        {
            if (Input.GetButtonDown("Coleta"))
            {
                if (SceneManager.GetActiveScene().name.Equals("Tutorial"))
                {
                    GameObject.Find("AudioController").GetComponent<AudioController>().Collect();
                    Destroy(collision.gameObject);
                }
                else
                {
                    GameObject.Find("AudioController").GetComponent<AudioController>().Collect();
                    Destroy(collision.gameObject);
                    if (contabilizaColeta == 0)
                    {
                        FindObjectOfType<MGameController>().GetCollectibles(collision.gameObject, gameObject);
                        contabilizaColeta = 1;
                    }
                }
            }
        }
    }
}
