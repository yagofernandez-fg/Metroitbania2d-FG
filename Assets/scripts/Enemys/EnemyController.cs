using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]protected float MoveSpeed;
    [Header("life")]
    [SerializeField]protected float Life;
    [Header("Attack")]
    [SerializeField]protected float Damage;
    [SerializeField] protected float distanceToAttack;
    [SerializeField]protected float attackRate;
    protected float TimerPas;
    protected bool attacking;
    protected Animator anim;
    protected Transform PlayerTransform;
    protected bool CanChase = false;
    protected Rigidbody2D rb;
    protected bool IsDead = false;
    protected Collider2D collider;
    
    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    protected void Update()
    {
        TimerPas += Time.deltaTime;
        if (CanChase == true &&  IsDead == false)
        {
            ChasePlayer();
        }
        else
        {
            anim.SetBool("run", false);
            attacking = false;

        }

        if (attacking)
        {
            if (TimerPas>= attackRate)
            {
                Attack();
                TimerPas = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
          other.GetComponent<PJController>().GetDamage(Damage);
        }
    }

    private void ChasePlayer()
    {
        if(IsDead) return;
        anim.SetBool("run", true);
        Vector3 moveDir = PlayerTransform.position - transform.position;// Posicion final - Posicion Inicial
        if (moveDir.x < 0)// izquierda negativo
        {
            transform.eulerAngles = Vector3.zero;
            rb.linearVelocityX = -MoveSpeed;
        }
        else// derecha positivo
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            rb.linearVelocityX = MoveSpeed;
        }

        if (Mathf.Abs(moveDir.x) <= distanceToAttack)
        {
            rb.linearVelocityX = 0;
            anim.SetBool("run", false);
            //Invoke("Attack",1);
            // InvokeRepeating("Attack",0.4f,2);
            // CancelInvoke();
            //Attack();
            attacking = true;
                
        }
        else
        {
            attacking = false;
        }
    }

    

    public void GetDamage(float damage)
    {
        if (IsDead) return;
        Life -= damage;
        if (Life <= 0)
        {
            anim.SetTrigger("Die");
            IsDead = true;
            rb.bodyType = RigidbodyType2D.Static;
            collider.enabled = false;
        }
        else
        {
            anim.SetTrigger("hit");
        }
        
        print("hit moster de "+ damage + " damage; le ha dejado a " + Life);
    }

    protected void Attack()
    {
        if (!GameManager.instance.gameData.IsDeath)
        {
            anim.SetTrigger("attack");
        }
        else
        {
            anim.SetTrigger("Win");
        }
        
    }



    public void ChasePlayer(Transform PJTransform,bool Chase)
    {
        PlayerTransform = PJTransform;
        CanChase = Chase;
    }
}
