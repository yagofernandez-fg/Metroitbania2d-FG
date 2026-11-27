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

    [SerializeField] protected GameObject BloodParticule;
    [SerializeField] protected Color BloodColor;
    [SerializeField] protected float KnockbackForce;
    protected float TimerPas;
    protected bool attacking;
    protected Animator anim;
    protected Transform PlayerTransform;
    protected bool CanChase = false;
    protected Rigidbody2D rb;
    protected bool IsDead = false;
    protected Collider2D collider;
    protected bool IsKnockback = false;

    [SerializeField] protected GameObject CurrencyPrefab;
    [SerializeField] protected int CurrencyAmount;
    [SerializeField] protected bool canPatrol;
    [SerializeField] protected Transform[] patrolPoints;
    protected int currentPatrolIndex = 0;
    protected float patrolWaitTimer = 0f;
    //[Header("Platform limits")]
    //[SerializeField] protected bool useLimits;
    //[SerializeField] protected Transform limitLeft;
    //[SerializeField] protected Transform limitRight;
    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    protected void Update()
    {
        TimerPas += Time.deltaTime;

        if (IsDead)
        {
            return;
        }

        if (CanChase)
        {
            ChasePlayer();
        }
        else if (canPatrol && patrolPoints != null && patrolPoints.Length > 0)
        {
            attacking = false;
            Patrol();
        }
        else
        {
            anim.SetBool("run", false);
            attacking = false;
        }

        if (attacking)
        {
            if (TimerPas >= attackRate)
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
        if (IsDead || IsKnockback) return;
        anim.SetBool("run", true);

        Vector3 moveDir = PlayerTransform.position - transform.position;
        float dir = moveDir.x < 0 ? -1f : 1f;

        transform.eulerAngles = dir < 0 ? Vector3.zero : new Vector3(0, 180, 0);
        rb.linearVelocityX = dir * MoveSpeed;

        if (Mathf.Abs(moveDir.x) <= distanceToAttack)
        {
            rb.linearVelocityX = 0;
            anim.SetBool("run", false);
            attacking = true;
        }
        //else
        //{
        //    attacking = false;

        //    if (IsAtLimit(dir))
        //    {
        //        rb.linearVelocityX = 0;
        //        anim.SetBool("run", false);
        //    }
        //}
    }



    public virtual void GetDamage(float damage)
    {
        if (IsDead) return;
        Life -= damage;
        if (Life <= 0)
        {
            anim.SetTrigger("Die");
            IsDead = true;
            rb.bodyType = RigidbodyType2D.Static;
            collider.enabled = false;
            spawnCurrency();
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

    public void GetHit(Vector3 dirHit)
    {
        if (IsDead) return;

        IsKnockback = true;

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(dirHit * KnockbackForce, ForceMode2D.Impulse);
        Invoke(nameof(EndKnockback), 0.25f);
        //rb.linearVelocity = Vector2.zero;
        Vector2 pos = new Vector2(transform.position.x+(dirHit.x*0.5f), transform.position.y + 0.5f);
        Quaternion rot;
        if (dirHit.x < 0) rot = Quaternion.Euler(0, 270, 0);
        else rot = Quaternion.Euler(0, 90, 0);
        GameObject blood = Instantiate(BloodParticule, pos, rot);
        ParticleSystem ps = blood.GetComponent<ParticleSystem>();

        var main = ps.main;
        main.startColor = BloodColor;

        ps.Play();
    }

    private void spawnCurrency()
    {
        for (int i = 0; i < CurrencyAmount; i++)
        {
            float x = transform.position.x + UnityEngine.Random.Range(-0.5f, 0.5f);
            float y = 0.5f + transform.position.y + UnityEngine.Random.Range(0f, 0.5f);
            Vector2 pos = new Vector2(x, y);
            Instantiate(CurrencyPrefab, pos, Quaternion.identity);
        }
    }


    private void EndKnockback()
    {
        IsKnockback = false;
    }



    public void ChasePlayer(Transform PJTransform, bool Chase)
    {
        PlayerTransform = PJTransform;
        CanChase = Chase;

        if (!Chase) currentPatrolIndex = GetClosestPatrolIndex();
    }

    protected int GetClosestPatrolIndex()
    {
        int best = 0;
        float bestDist = 1000;
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            float d = Mathf.Abs(patrolPoints[i].position.x - transform.position.x);
            if (d < bestDist) { bestDist = d; best = i; }
        }
        return best;
    }
    protected void Patrol()
    {
        if (IsKnockback) return;

        Transform target = patrolPoints[currentPatrolIndex];
        float distX = target.position.x - transform.position.x;

        // Ha llegado al punto: esperar y pasar al siguiente
        if (Mathf.Abs(distX) <= 0.2f)
        {
            rb.linearVelocityX = 0;
            anim.SetBool("run", false);

            patrolWaitTimer += Time.deltaTime;
            if (patrolWaitTimer >= 0.75f)// esperamos 0.75 s
            {
                patrolWaitTimer = 0f;
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            }
            return;
        }

        // Caminar hacia el punto
        anim.SetBool("run", true);
        if (distX < 0)
        {
            transform.eulerAngles = Vector3.zero;
            rb.linearVelocityX = -MoveSpeed;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            rb.linearVelocityX = MoveSpeed;
        }
    }

    //protected bool IsAtLimit(float dir)
    //{
    //    if (!useLimits || limitLeft == null || limitRight == null) return false;

    //    if (dir < 0 && transform.position.x <= limitLeft.position.x) return true;
    //    if (dir > 0 && transform.position.x >= limitRight.position.x) return true;

    //    return false;
    //}


}
