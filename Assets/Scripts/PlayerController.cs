using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using static UnityEngine.EventSystems.EventTrigger;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private string namePlayer = "Simétra";
    public string[] message;
    public Sprite iconPlayer;

    private float speed = 6;
    private int danoPlayer = 1;
    private float jumpForce = 8.5f;
    public bool doubleJump;

    private Rigidbody2D rb;
    private bool playerInFloor = false;
    private Animator ani;
    private int contabilizaDano = 0;
    private int contabilizaColeta = 0;

    private bool isClimbing;
    private bool isLadder;
    private int condicaoPassos = 0;

    public Transform detectFloor;
    public LayerMask isFloor;

    private DialogueTipo2Controller dc;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("Acidron2"))
        {
            dc = FindObjectOfType<DialogueTipo2Controller>();
        }

        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if ((PlayerPrefs.GetFloat("saveX") != 0) && (PlayerPrefs.GetFloat("saveY") != 0))
        {
            transform.position = FindObjectOfType<GameController>().GetSavePoint();
            FindObjectOfType<GameController>().SetContabilizaBonusForce();
            SetSpeed(1);
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
        }
        else if (SceneManager.GetActiveScene().name.Equals("LevelFour") || SceneManager.GetActiveScene().name.Equals("Acidron1"))
        {
            speed += 3;
            danoPlayer += 3;
        }
        else if (SceneManager.GetActiveScene().name.Equals("LevelFive"))
        {
            speed += 4;
            danoPlayer += 4;
        }
        if (SceneManager.GetActiveScene().name.Equals("LevelFive") || SceneManager.GetActiveScene().name.Equals("Acidron1") || SceneManager.GetActiveScene().name.Equals("Acidron2"))
        {
            GameObject.Find("HUD_Dialogue").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Animation>().Play("Arrow");
        }
    }

    public void SetDanoPlayer(int valor)
    {
        danoPlayer += valor;
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        GameObject dialogo = GameObject.Find("Dialogue");

        if (Input.GetButtonDown("Jump") && dialogo==null)
        {
            if (playerInFloor) {
                rb.velocity = Vector2.zero;
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                GameObject.Find("AudioController").GetComponent<AudioController>().RunningBreak();
                GameObject.Find("AudioController").GetComponent<AudioController>().Jump();
                playerInFloor = false;
                doubleJump = true;
            } else if (!playerInFloor && doubleJump == true){
                rb.velocity = Vector2.zero;
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                GameObject.Find("AudioController").GetComponent<AudioController>().RunningBreak();
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
            GameObject.Find("AudioController").GetComponent<AudioController>().Attack1();
        }
        else
        {
            ani.SetBool("isAtacking", false);
        }

        if ((Input.GetButtonDown("Ataque2") || Input.GetAxis("Ataque2") > 0) && SceneManager.GetActiveScene().buildIndex >= 7)
        {
            ani.SetBool("isMagicAtacking", true);
            GameObject.Find("AudioController").GetComponent<AudioController>().Attack2();
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
        if (SceneManager.GetActiveScene().name.Equals("Acidron2"))
        {
            GameObject queen = GameObject.Find("Queen");

            if(Vector3.Distance(transform.position, queen.transform.position) < 7f)
            {
                rb.bodyType = RigidbodyType2D.Static;
                if (dc.GetStart())
                {
                    if(ordem == 0)
                    {
                        dc.Speech(iconPlayer, message, namePlayer, 0, 1);
                        ordem++;
                    }
                    else if(ordem == 1)
                    {
                        FindObjectOfType<NPCTipo2>().toCallDialogue(0, 1, 2);
                        ordem++;
                    }
                }
            }
            else
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }

    private int ordem = 0;

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
        // Movimentacao
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector3(speed * horizontal, rb.velocity.y, 0f);
        if(horizontal > 0){
            ani.SetBool("isRunning", true);
            if(condicaoPassos == 0)
            {
                GameObject.Find("AudioController").GetComponent<AudioController>().RunningPlay();
                condicaoPassos = 1;
            }
            Flip(false);
        }
        else if(horizontal < 0){
            ani.SetBool("isRunning", true);
            if (condicaoPassos == 0)
            {
                GameObject.Find("AudioController").GetComponent<AudioController>().RunningPlay();
                condicaoPassos = 1;
            }
            Flip(true);
        }
        else{
            ani.SetBool("isRunning", false);
            GameObject.Find("AudioController").GetComponent<AudioController>().RunningBreak();
            Invoke("resetPassos", 0.13f);
            

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

    private void resetPassos()
    {
        condicaoPassos = 0;
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
                FindObjectOfType<GameController>().HurtEnemy(collision.gameObject);
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

        if (collision.gameObject.CompareTag("Door") && (Input.GetKeyDown(KeyCode.T) || Input.GetButtonDown("Interacao")))
        {
            FindObjectOfType<GameController>().LevelEnd();
        }

        if (collision.gameObject.CompareTag("ObjAzul") && (Input.GetKeyDown(KeyCode.T) || Input.GetButtonDown("Interacao")))
        {
            Destroy(collision.gameObject);
            GameObject.Find("HUD_Dialogue").SetActive(false);
            FindObjectOfType<GameController>().LevelEnd();
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
