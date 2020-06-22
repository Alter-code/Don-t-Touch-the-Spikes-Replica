using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    private float jumpHeight = 100;

    private Rigidbody2D rb;

    private SpriteRenderer spriteRenderer;

    private Animator animator;

    private bool playerDie = false;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float rotation = 0f;

    [SerializeField] private GameObject jumpParticle;

    [SerializeField] private PhysicsMaterial2D physicsMaterial;

    public static int scoreAmount = 0;

    private AudioManager audioManager;

    private GameManager gameManager;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        physicsMaterial.bounciness = 0f;
        audioManager = AudioManager.instance;
        gameManager = GameManager.instance;
        scoreAmount = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !playerDie) 
        {
            jumpHeight = rb.position.y + 1.5f;
            animator.SetBool("isJumping", true);
            audioManager.Play("Jump");
            Instantiate(jumpParticle, transform.position , Quaternion.Euler(90f, 0f, 0f));
        }  
    }

    private void FixedUpdate()
    {
        if(!playerDie)
        {
            Jump();
            Move();
        }
        else if (playerDie && rotation<50)
        {
            rotation += 0.2f;
            rb.rotation += rotation;
        }
    }
    
    private void Move()
    { 
        rb.position = new Vector2(rb.position.x + moveSpeed * Time.fixedDeltaTime ,rb.position.y);
    }

    private void Jump()
    {
        if (jumpHeight != 100)
        {
            rb.gravityScale = -1f;
            rb.velocity = Vector2.up * jumpForce * Time.fixedDeltaTime;
        }
        
        if(rb.position.y >= jumpHeight)
        {
            if(rb.gravityScale <= 1f)
            {
                rb.gravityScale = 1f;
            }
            else
            {
                rb.gravityScale += 0.1f * Time.fixedDeltaTime;
            }
            jumpHeight = 100;
            animator.SetBool("isJumping", false);
        }
    }

    private void Die()
    {
        if(!playerDie)
        {
            physicsMaterial.bounciness = 4f;
            rb.AddForce(Vector2.right * -moveSpeed * Time.fixedDeltaTime * 50f, ForceMode2D.Impulse);
        }
        playerDie = true;
        rb.gravityScale = 1f;
        audioManager.Play("Die");
        gameManager.EndGame();
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall")
        {
            rb.velocity = Vector2.zero;
            if (!playerDie){
                scoreAmount++;
                audioManager.Play("Score");
            }
            moveSpeed *= -1;
            spriteRenderer.flipX = !spriteRenderer.flipX;

            rb.velocity = Vector3.zero;
        }
        if(rb.velocity.y >= 0.1f)
        {
            Jump();
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Damage")
        {
            jumpHeight = 100;
            Die();
        }
        if (playerDie)
        {
            physicsMaterial.bounciness = 0f;
            moveSpeed = 0f;
        }
    }
}
