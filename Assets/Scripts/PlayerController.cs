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

    public Transform detectFloor;
    public Transform detectFloor2;
    public LayerMask isFloor;

    public float slopeCheckDistance;
    private float slopeAngle;
    private bool isOnSlope;
    private Vector2 perpendicularSpeed;

    private int combo = 0;

    public PhysicsMaterial2D noFriction;
    public PhysicsMaterial2D friction;

    private DialogueTipo2Controller dc;

    private bool jump = true;
    private float direcaoDoAtaque;

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
    }

    public void SetDanoPlayer(int valor)
    {
        danoPlayer += valor;
    }

    private void reativaPulo()
    {
        jump = true;
    }

    public void resetaInputPulo()
    {
        jump = false;
        Invoke("reativaPulo", .1f);
    }

    // Update is called once per frame
    void Update()
    {
        //print(GetDano());
        //print("ladeira: " + isOnSlope);
        float vertical = Input.GetAxisRaw("Vertical");
        GameObject dialogo = GameObject.Find("Dialogue");

        DetectSlopes();

        if (Input.GetButtonDown("Jump") && jump && dialogo==null)
        {
            if (playerInFloor || isOnSlope) {
                rb.velocity = Vector2.zero;
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                GameObject.Find("AudioController").GetComponent<AudioController>().RunningBreak();
                GameObject.Find("AudioController").GetComponent<AudioController>().Jump();
                playerInFloor = false;
                doubleJump = true;
                isOnSlope = false;
                ani.SetBool("isJumping", true);
            } else if (!playerInFloor && doubleJump == true){
                rb.velocity = Vector2.zero;
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                GameObject.Find("AudioController").GetComponent<AudioController>().RunningBreak();
                GameObject.Find("AudioController").GetComponent<AudioController>().Jump();
                playerInFloor = false;
                doubleJump = false;
                ani.SetBool("isJumping", true);
            }
            rb.sharedMaterial = noFriction;
        } else {
            //ani.SetBool("isJumping", false);
        }

        if (Input.GetButtonDown("Ataque1") && contabilizaDano == 0)
        {
            direcaoDoAtaque = transform.localScale.x;
            if (combo == 5)
            {
                ani.SetBool("isEspecial", true);
                Invoke("ReiniciaCombo", .8f);

                //SOM DE ATAQUE ESPECIAL
                GameObject.Find("AudioController").GetComponent<AudioController>().AttackCombo();
            }
            else
            {
                ani.SetBool("isAtacking", true);
                GameObject.Find("AudioController").GetComponent<AudioController>().Attack1();
            }
        }
        else
        {
            ani.SetBool("isAtacking", false);
            ani.SetBool("isEspecial", false);
        }

        if (((Input.GetButtonDown("Ataque1") || (Input.GetButtonDown("Ataque2") || Input.GetAxis("Ataque2") > 0))) && contabilizaDano != 0)
        {
            //SOM DE BLOQUEIO
            GameObject.Find("AudioController").GetComponent<AudioController>().AttackBlock();
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

    public void DetectSlopes()
    {
        //Vector2 position = new Vector2(0f, 0f);
        RaycastHit2D hitSlope = Physics2D.Raycast(detectFloor.position, Vector2.down, slopeCheckDistance, isFloor);

        if (hitSlope)
        {
            perpendicularSpeed = Vector2.Perpendicular(hitSlope.normal).normalized;

            slopeAngle = Vector2.Angle(hitSlope.normal, Vector2.up);
            isOnSlope = slopeAngle > 5;
            //print(slopeAngle);
        }

        if(isOnSlope && !Input.anyKey && Input.GetAxis("Horizontal")==0)
        {
            rb.sharedMaterial = friction;
        }
        else
        {
            rb.sharedMaterial = noFriction;
        }
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

        if (isOnSlope && playerInFloor)
        {
            //MOVIMENTACAO NA COLINA
            rb.velocity = new Vector3(speed * (-horizontal) * perpendicularSpeed.x * 0.8f, speed * (-horizontal) * perpendicularSpeed.y * 0.8f, 0f);
        }
        else
        {
            //MOVIMENTACAO PADRAO
            rb.velocity = new Vector3(speed * horizontal, rb.velocity.y, 0f);
        }
        
        if(horizontal > 0){
            ani.SetBool("isRunning", true);
            if (playerInFloor)
            {
            GameObject.Find("AudioController").GetComponent<AudioController>().RunningPlay();
            }
            Flip(false);
        }
        else if(horizontal < 0){
            ani.SetBool("isRunning", true);
            if (playerInFloor)
            {
                GameObject.Find("AudioController").GetComponent<AudioController>().RunningPlay();
            }
            Flip(true);
        }
        else{
            ani.SetBool("isRunning", false);
            GameObject.Find("AudioController").GetComponent<AudioController>().RunningBreak();
            

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
            if((horizontal <= 0 && direcaoDoAtaque < 0) || (horizontal >= 0 && direcaoDoAtaque > 0))
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0f);
            }
            else
            {
                ani.SetBool("isAtacking", false);
                ani.SetBool("isEspecial", false);
                ani.Play("idle");
            }
        }

        //old 0.16739f
        bool detect1 = Physics2D.OverlapBox(detectFloor.position, new Vector2(0.3f, 0.3f), 0f, isFloor);
        bool detect2 = Physics2D.OverlapBox(detectFloor2.position, new Vector2(0.3f, 0.3f), 0f, isFloor);
        if((detect1 && detect2) == false)
        {
            //coyoteTimeContador
            Invoke("resetCoyoteTime", 0.25f);
        }
        else {
            playerInFloor = true;
            CancelInvoke("resetCoyoteTime");
        }
        /*playerInFloor = Physics2D.OverlapCircle(detectFloor.position, 0.0887f, isFloor);*/
    }

    private void resetCoyoteTime()
    {
        playerInFloor = false;
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
            CancelInvoke("resetCoyoteTime");
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
        ReiniciaCombo();
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
        //DEFINE DANO DO COMBO
        if(combo == 5)
            return danoPlayer+2;
        else
            return danoPlayer;
    }

    private void ReiniciaCombo(){
        combo = 0;
    }

    public int  getCombo(){
        return combo;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Animator animator = GetComponent<Animator>();
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (collision.gameObject.CompareTag("Enemy") && (stateInfo.IsName("Attack") || stateInfo.IsName("especial")))
        {
            if (contabilizaDano == 0)
            {
                InputController controle = GameObject.Find("InputController").GetComponent<InputController>();
                if (stateInfo.IsName("Attack")){
                    CancelInvoke("ReiniciaCombo");
                    combo++;
                    Invoke("ReiniciaCombo", 4f);
                    controle.Vibrate(0.3f);
                }
                else{
                    controle.Vibrate(1f);
                }

                if(stateInfo.IsName("especial") && SceneManager.GetActiveScene().name.Equals("Tutorial")){
                    enemyTarget alvo = GameObject.Find("BonecoAlvo").GetComponent<enemyTarget>();
                    alvo.destroiAlvo();
                }
                GameObject.Find("AudioController").GetComponent<AudioController>().HurtEnemy();
                FindObjectOfType<GameController>().HurtEnemy(collision.gameObject);
                contabilizaDano = 1;
                Invoke("Sleep", 1.5f);
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

        if (collision.gameObject.CompareTag("Boss") && stateInfo.IsName("Spell"))
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
