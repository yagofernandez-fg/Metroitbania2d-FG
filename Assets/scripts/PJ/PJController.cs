using UnityEngine;

public class PJController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private Rigidbody2D rb;
    private float NormalGravity;
    private Animator animator;
    private int jumpCount, maxJump = 1;
    private bool IsFalling;
    private bool IsGrounded;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        NormalGravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        rb.linearVelocityX = x * speed;
        if (x != 0)
        {
            animator.SetBool("run", true);
            if (x > 0)
            {
                transform.eulerAngles = Vector3.zero;
            }
            else if (x < 0)
            {
                transform.eulerAngles = new Vector3(0, 180,0);
            }
        }
        else
        {
            animator.SetBool("run", false);
        }
        bool touching = false;
        Collider2D[] temp = Physics2D.OverlapCircleAll(transform.position, 0.05f);
        for (int i=0; i<temp.Length; i++)
        {
            if (temp[i].tag == "ground")
            {
               touching = true;
                break;
            }
        }
        IsGrounded = touching && rb.linearVelocityY <= 0.01f;
        if (Input.GetButtonDown("Jump") && jumpCount < maxJump)
        { 
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            
            animator.SetBool("jump", true);
            IsGrounded = false;
            jumpCount++;
        }

        if (IsGrounded)
        {
            animator.SetBool("jump", false);
            animator.SetBool("falling", false);
            jumpCount = 0;
            
        }
        else
        {
            if (jumpCount == 0)// se a caido
            {
                jumpCount ++;
            }
            animator.SetBool("falling", true);
        }
        
    }
    
}
