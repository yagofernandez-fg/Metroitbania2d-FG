using System;
using UnityEngine;

public class PJController : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    private Rigidbody2D rb;
    private float NormalGravity;
    private Animator animator;
    private int jumpCount;
    private bool IsFalling;
    private bool IsGrounded;
    [SerializeField]private float runMaxSpeed;
    [SerializeField]private float runAccelAmount;
    [SerializeField]private float runDeccelAmount;
    private float accelRate;
    private int comboCount=0;

    [SerializeField] private float attackRate = 0.3f;
    [SerializeField] private GameObject MagicPrefab;
    [SerializeField] private float ManaMagicCost=1;
    
    private LvlManager lvlManager;

    private float TimerPas = 10;
    bool canAttack = true;
    bool canMove = true;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        NormalGravity = rb.gravityScale;
        
        lvlManager = GameObject.Find("LvlManager").GetComponent<LvlManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameData.IsDeath || GameManager.instance.Intransition) return;

        TimerPas += Time.deltaTime;
        move();
        jump();
        if (TimerPas >= attackRate)
        {
            canAttack = true;
        }
        if (canAttack || comboCount==1)
        {
            Attack();
            canAttack = false;
        }

        if (IsGrounded)
        {
            hardAttack();
        }

        Magic();
    }

    private void Magic()
    {
        
            if (Input.GetButtonDown("Magic"))
            {
                if (GameManager.instance.gameData.Mana > 0)
                {
                    animator.SetTrigger("Magic");
                    Vector2 dir = transform.position;
                    Vector2 dirProjectil = Vector2.zero;
                    if (transform.eulerAngles == Vector3.zero) dir.x += 1;
                    else dir.x -= 1;
                    dir.y += 1;

                    GameObject BolaF = Instantiate(MagicPrefab, dir, transform.rotation);
                    BolaF.GetComponent<Rigidbody2D>().AddForce(transform.right * 10, ForceMode2D.Impulse);
                    
                    GameManager.instance.gameData.Mana -= ManaMagicCost;
                    lvlManager.UpdateManaBar();
                }
            }
    }

    private void hardAttack()
    {
        if (Input.GetButtonDown("HardAttck"))
        {
            rb.linearVelocityX /= 4;
            animator.SetTrigger("HardAttack");
            canMove =  false;
        }
    }

    private void move()
    {
        if(!canMove) return;
        float x = Input.GetAxis("Horizontal");
        if (comboCount > 0)
        {
            x /=4;
        }
            
        float targetSpeed = x * runMaxSpeed;
        targetSpeed = Mathf.Lerp(rb.linearVelocityX, targetSpeed, 1);
        accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? runAccelAmount : runDeccelAmount;
        float speedDif = targetSpeed - rb.linearVelocityX;

        float movement = speedDif * accelRate;

        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
        //rb.linearVelocityX = x * speed;
        if (x != 0)
        {
            animator.SetBool("run", true);
            if (x > 0 && comboCount <= 0)
            {
                transform.eulerAngles = Vector3.zero;
            }
            else if (x < 0 && comboCount <= 0)
            {
                transform.eulerAngles = new Vector3(0, 180,0);
            }
        }
        else
        {
            animator.SetBool("run", false);
        }
    }

    private void jump()
    {
        if (!canMove) return;
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
        if (Input.GetButtonDown("Jump") && jumpCount < GameManager.instance.gameData.MaxJump&& comboCount == 0)
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

    private void Attack()
    {
        if (Input.GetButtonDown("Attack1")&& IsGrounded)
        {

            TimerPas = 0;
            comboCount = Mathf.Clamp(comboCount + 1, 0, 2);
            
            
            animator.SetInteger("Combo", comboCount);
        }
    }

    public void finishAttack1()
    {
        if (comboCount <= 1)
        {
            comboCount = 0;
            animator.SetInteger("Combo", comboCount);
        }
    }    
    public void finishAttack2()
    {
        comboCount = 0;
        animator.SetInteger("Combo", comboCount);
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Vector3 direccion = (transform.position - other.transform.position).normalized;
            other.gameObject.GetComponent<EnemyController>().GetDamage(GameManager.instance.gameData.Dmg);
            other.gameObject.GetComponent<EnemyController>().GetHit(direccion);
        }
    }

    public void GetDamage(float damage)
    {
            GameManager.instance.gameData.Life -= damage;
            lvlManager.UpdateLifeBar();
            print(damage+" le queda : "+GameManager.instance.gameData.Life);
            if (GameManager.instance.gameData.Life <= 0 && !GameManager.instance.gameData.IsDeath)
            {
                print("GameOver");
                animator.SetTrigger("Die");
                GameManager.instance.gameData.IsDeath = true;
                rb.linearVelocityX = 0;
                //Destroy(gameObject);
            }
            else if(GameManager.instance.gameData.Life > 0)
            {
                animator.SetTrigger("Hit");
                comboCount = 0;
                animator.SetInteger("Combo", comboCount);
            }
    }

    public void CanMove(bool _canMove = true)
    {
        canMove = _canMove;
    }
}
