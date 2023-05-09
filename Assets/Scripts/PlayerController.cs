using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public Transform detectFloor;
    public LayerMask isFloor;


    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        rbSprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (playerInFloor)
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                playerInFloor = false;
                doubleJump = true;
            }else if (!playerInFloor && doubleJump == true){
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                playerInFloor = false;
                doubleJump = false;
            }
        }

    }

    private void FixedUpdate()
    {
        // Movimentação
        float horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector3(speed * horizontal, rb.velocity.y, 0f);
        if(horizontal > 0)
        {
            ani.SetBool("idle", true);
            rbSprite.flipX = false;
        }
        else if(horizontal < 0)
        {
            ani.SetBool("idle", true);
            rbSprite.flipX = true;
        }
        else
        {
            ani.SetBool("idle", false);
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("collectibles")){
            Destroy(collision.gameObject);
        }
    }
}
