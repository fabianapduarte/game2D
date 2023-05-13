using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public bool doubleJump;

    private Rigidbody2D rb;
    private SpriteRenderer rbSprite;
    private bool playerInFloor = false;
    private float maxSpeed = 10f;
    private Animator ani;
    private BoxCollider2D playerCollider;
    private Vector3 safePoint;

    public Transform detectFloor;
    public LayerMask isFloor;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        playerCollider = GetComponent<BoxCollider2D>();
        rbSprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")){
            if (playerInFloor) {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                playerInFloor = false;
                doubleJump = true;
            } else if (!playerInFloor && doubleJump == true){
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
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
        }
        else
        {
            ani.SetBool("isAtacking", false);
        }

        if (playerInFloor)
        {
            ani.SetBool("isJumping", false);
        }
    }

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
        //bool flip = false;
        if(horizontal > 0){
            ani.SetBool("isRunning", true);
            Flip(false);
            //rbSprite.flipX = false;
        }
        else if(horizontal < 0){
            ani.SetBool("isRunning", true);
            Flip(true);
            //rbSprite.flipX = true;
        }
        else{
            ani.SetBool("isRunning", false);
        }

        // teste da morte de Sim�tra
        if(vertical < 0){
            ani.SetBool("isDead", true);
            playerCollider.offset = new Vector2(-0.145f, -0.16f);
            playerCollider.size = new Vector2(0.4f, 0.1f);
        } else if (vertical > 0){
            ani.SetBool("isJumping", true);
        }
        

        //Pulo Simples
        playerInFloor = Physics2D.OverlapBox(detectFloor.position, new Vector2(0.16739f, 0.16739f), 0f, isFloor);
        /*playerInFloor = Physics2D.OverlapCircle(detectFloor.position, 0.0887f, isFloor);*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer.Equals("Floor"))
        {
            playerInFloor = true;
            doubleJump = false;
        }

        if (collision.gameObject.layer.Equals("Enemy"))
        {
            if (ani.GetBool("isAtacking"))
            {
                Debug.Log(1);
                FindObjectOfType<GameController>().HurtEnemy(collision);
            }
        }

        if (collision.gameObject.tag.Equals("LevelEnd")) {
            FindObjectOfType<GameController>().LevelEnd();
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("collectibles")){
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("savepoint"))
        {
            safePoint = collision.transform.position;
        }
    }
}
