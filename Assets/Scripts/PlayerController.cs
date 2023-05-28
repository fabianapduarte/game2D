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
        }
        if (SceneManager.GetActiveScene().name.Equals("LevelTwo"))
        {
            speed += 1;
            FindObjectOfType<GameController>().SetDanoPlayer(1);
        }else if (SceneManager.GetActiveScene().name.Equals("LevelTree"))
        {
            speed += 2;
            FindObjectOfType<GameController>().SetDanoPlayer(2);
        }else if (SceneManager.GetActiveScene().name.Equals("LevelFour"))
        {
            speed += 3;
            FindObjectOfType<GameController>().SetDanoPlayer(3);
        }
        Debug.Log(speed);
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
        // Movimentação
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector3(speed * horizontal, rb.velocity.y, 0f);
        if(horizontal > 0){
            ani.SetBool("isRunning", true);
            //running.Play();
            //running.mute = false;
            //GameObject.Find("AudioController").GetComponent<AudioController>().RunningPlay();
            Flip(false);
        }
        else if(horizontal < 0){
            ani.SetBool("isRunning", true);
            //running.Play();
            //running.mute = false;
            //GameObject.Find("AudioController").GetComponent<AudioController>().RunningPlay();
            Flip(true);
        }
        else{
            ani.SetBool("isRunning", false);
            //running.mute = true;
            //GameObject.Find("AudioController").GetComponent<AudioController>().RunningBreak();

        }

        Animator animator = GetComponent<Animator>();
        if (isClimbing)
        {
            //ani.SetBool("isLadder", true);
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
            //congela frame caso a ela esteja parada
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
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

    public void SetSpeed()
    {
        speed++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (contabilizaDano == 0)
            {
                GameObject.Find("AudioController").GetComponent<AudioController>().HurtEnemy();
                FindObjectOfType<GameController>().HurtEnemy(collision.gameObject);
                contabilizaDano = 1;
                Invoke("Sleep", 1.5f);
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
            Debug.Log("Saveee");
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
