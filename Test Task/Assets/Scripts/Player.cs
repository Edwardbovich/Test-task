using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    
    
    public Transform groundCheck;
    public float groundCheckRadius = 0.3f;
    public LayerMask whatIsGround;

    Rigidbody2D rb;

   private int jump;
   public int jumpUp;
    private int jumpDown;

    public int health;
    public float jumpForce;

    public GameObject effect;

    public GameObject GameOverScreen;

    private bool isGrounded;

    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.gravityScale = 4f;
        jump = jumpUp;
    }

    private void Update()
    {
        if(isGrounded == true)
         {
            jumpUp = 0;
         }
        if (jump == 0)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                rb.velocity = Vector2.up * jumpForce;

                Instantiate(effect, transform.position, Quaternion.identity);
                jumpUp++;   
            }
            if (jumpUp == 2)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    rb.gravityScale *= -1;
                    transform.Rotate(0, 180, 180);
                    jumpUp = 0;
                    jumpDown = 1;
                    
                }
            }
        }
        if (jumpDown == 1)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                rb.gravityScale *= -1;
                transform.Rotate(0, 180, 180);
                jumpDown = 0;
                Instantiate(effect, transform.position, Quaternion.identity);
            }
        }
        
        

        if (health <= 0)
        {
            GameOverScreen.SetActive(true);
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    
    
  
    void OnCollisionEnter2D(Collision2D co)
    {
        
        if (co.collider.name == "V")
        {
            // Reset Rotation, Gravity, Velocity and go to last Checkpoint
            transform.rotation = Quaternion.identity;
            rb.gravityScale = Mathf.Abs(rb.gravityScale);
            rb.velocity = Vector2.zero;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
