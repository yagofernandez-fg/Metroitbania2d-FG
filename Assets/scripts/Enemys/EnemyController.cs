using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]protected float MoveSpeed;
    [SerializeField]protected float Life;
    [SerializeField]protected float Damage;
    [SerializeField] protected float distanceToAttack;
    protected Animator anim;
    protected Transform PlayerTransform;
    protected bool CanChase = false;
    protected Rigidbody2D rb;
    
    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (CanChase == true)
        {
            
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
                Attack();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerTransform = other.transform;
            CanChase = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        CanChase = false;
        anim.SetBool("run", false);
    }
    

    public void GetDamage(float damage)
    {
        Life -= damage;
        if (Life <= 0)
        {
            Destroy(gameObject);
            print("Ha muerto");
        }
        anim.SetTrigger("hit");
        print("hit moster de "+ damage + " damage; le ha dejado a " + Life);
    }

    protected void Attack()
    {
        anim.SetTrigger("attack");
    }
}
