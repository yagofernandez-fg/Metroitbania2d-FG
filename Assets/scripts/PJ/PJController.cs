using UnityEngine;

public class PJController : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    private Rigidbody2D rb;
    private float NormalGravity;
    private Animator animator;
    private int jumpCount, maxJump = 1;
    private bool IsFalling;
    private bool IsGrounded;
    [SerializeField]private float runMaxSpeed;
    [SerializeField]private float runAccelAmount;
    [SerializeField]private float runDeccelAmount;
    private float accelRate;
    
    
    
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
        float targetSpeed = x * runMaxSpeed;
        targetSpeed = Mathf.Lerp(rb.linearVelocityX, targetSpeed, 1);
        accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? runAccelAmount : runDeccelAmount;
        float speedDif = targetSpeed - rb.linearVelocityX;
        //Calculate force along x-axis to apply to thr player

        float movement = speedDif * accelRate;

        //Convert this to a vector and apply to rigidbody
        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
        //rb.linearVelocityX = x * speed;
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
        if (Input.GetButtonDown("Jump") && jumpCount < GameManager.instance.gameData.MaxJump)
        { 
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            
            animator.SetBool("jump", true);
            IsGrounded = false;
            jumpCount++;
        }

        if (Input.GetButtonUp("Jump") && !IsGrounded && rb.linearVelocityY > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocityY * .5f);
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
