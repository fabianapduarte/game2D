using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MaxSpeed;
    public float JumpForce;

    private Rigidbody2D rb;
    private SpriteRenderer rbSprite;
    private bool PlayerInFloor = false;

    public Transform detectFloor;
    public LayerMask isFloor;
    // Start is called before the first frame update
    void Start()
    {
        rbSprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && PlayerInFloor == true)
        {
            rb.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        }

    }

    private void FixedUpdate()
    {
        // Movimentação
        float horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector3(MaxSpeed * horizontal, rb.velocity.y, 0f);
        if(horizontal > 0)
        {
            rbSprite.flipX = false;
        }
        if(horizontal < 0)
        {
            rbSprite.flipX = true;
        }

        //Pulo Simples
        PlayerInFloor = Physics2D.OverlapBox(detectFloor.position, new Vector2(0.16739f, 0.16739f), 0f, isFloor);
        /*PlayerInFloor = Physics2D.OverlapCircle(detectFloor.position, 0.0887f, isFloor);*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer.Equals("Floor"))
        {
            PlayerInFloor = true;
        }
    }
}
