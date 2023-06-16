using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerController : MonoBehaviour
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
        //running = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        if (FindObjectOfType<GameController>().GetSavePoint() != Vector3.zero)
        {
            transform.position = FindObjectOfType<GameController>().GetSavePoint();
            GameObject.Find("Simetra").GetComponent<PlayerController>().SetSpeed(1);
        }
        if (SceneManager.GetActiveScene().name.Equals("LevelTwo"))
        {
            speed += 1;
            danoPlayer += 1;
        }
        else if (SceneManager.GetActiveScene().name.Equals("LevelTree"))
        {
            speed += 2;
            danoPlayer += 2;
        }else if (SceneManager.GetActiveScene().name.Equals("LevelFour"))
        {
            speed += 3;
            danoPlayer += 3;
        }
    }

    public int GetDanoPlayer()
    {
        return danoPlayer;
    }

    public void SetDanoPlayer(int valor)
    {
        danoPlayer += valor;
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump")){
            if (playerInFloor) {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                GameObject.Find("AudioController").GetComponent<AudioController>().Jump();
                playerInFloor = false;
                doubleJump = true;
            } else if (!playerInFloor && doubleJump == true){
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                GameObject.Find("AudioController").GetComponent<AudioController>().Jump();
                playerInFloor = false;
                doubleJump = false;
            }
            ani.SetBool("isJumping", true);
        } else {
            //ani.SetBool("isJumping", false);
        }

        if (Input.GetButtonDown("Ataque1"))
        {
            ani.SetBool("isAtacking", true);
            GameObject.Find("AudioController").GetComponent<AudioController>().Attack();
        }
        else
        {
            ani.SetBool("isAtacking", false);
        }

        if ((Input.GetButtonDown("Ataque2") || Input.GetAxis("Ataque2") > 0) && SceneManager.GetActiveScene().buildIndex >= 7)
        {
            ani.SetBool("isMagicAtacking", true);
            GameObject.Find("AudioController").GetComponent<AudioController>().Attack();
        }
        else
        {
            ani.SetBool("isMagicAtacking", false);
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

    //private AudioSource running;

    private void Flip(bool isFliped){
        float scaleX = Mathf.Abs(transform.localScale.x);
        if (isFliped){
            scaleX *= -1;
        }
        transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
    }

    private void FixedUpdate()
    {
        // Movimenta��o
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector3(speed * horizontal, rb.velocity.y, 0f);
        if(horizontal > 0){
            ani.SetBool("isRunning", true);
            Flip(false);
        }
        else if(horizontal < 0){
            ani.SetBool("isRunning", true);
            Flip(true);
        }
        else{
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

        if ((stateInfo.IsName("Attack") || stateInfo.IsName("Spell")) && playerInFloor){
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
        if(collision.gameObject.layer == 3) // floor
        {
            playerInFloor = true;
            doubleJump = false;
        }

        if (collision.gameObject.tag.Equals("Finish")) {
            Debug.Log(speed);
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

    public float GetVelocidade()
    {
        return speed;
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
                FindObjectOfType<GameController>().HurtEnemy(collision.gameObject);
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
            FindObjectOfType<GameController>().DeadPlayer();
        }

        if (collision.gameObject.CompareTag("lava"))
        {
            ani.SetBool("isDead", true);
            FindObjectOfType<GameController>().DeadPlayer();
        }

        if (collision.gameObject.CompareTag("savepoint"))
        {
            FindObjectOfType<GameController>().SetSafePoint(collision.gameObject);
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
                        FindObjectOfType<GameController>().GetCollectibles(collision.gameObject);
                        contabilizaColeta = 1;
                    }
                }
            }
        }
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
}
