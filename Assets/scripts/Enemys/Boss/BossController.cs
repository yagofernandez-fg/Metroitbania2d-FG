using System;
using System.Collections;
using UnityEngine;
using static Unity.U2D.Physics.PhysicsShape;
using Random = UnityEngine.Random;

enum BossEstate
{
    IDLE,ROAR,ROLL_ATTACK,WALK,SPIKE_ATTACK,JUMP,DEATH
}


public class BossController : EnemyController
{
    [SerializeField]private BossEstate bossEstate;
    [SerializeField]private Color hitColor;
    [SerializeField] private GameObject player;
    [Header("SPIN")]
    [SerializeField] private float rollSpeed;
    [Header("MOVE")]
    [SerializeField] Transform CenterP;
    [Header("SPIKES")]
    private bool thrownSpikes = false;
    [SerializeField] Transform[] spikesSpawnPoints;
    [SerializeField] private GameObject ProjectilePrefab;
    [Header("ROAR")]
    [SerializeField] private Transform MinRocas, MaxRocas;
    [SerializeField] private GameObject[] RocaProjectils;
    [SerializeField] private float timeRocasI;
    [SerializeField] private float RocaSpawnRate;
    [SerializeField] private ParticleSystem RoarParticule;
    [Header("JUMP")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private float JumpSpeed;
    [SerializeField] private float MinXJump,MaxXJump;
    
    private SpriteRenderer spriteRenderer;
    private bool IsRolling = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();
        ChangeState(BossEstate.IDLE);
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        
        // player = GameObject.Find("PJ");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeState(BossEstate newBossEstate)
    {
        this.bossEstate = newBossEstate;

        LoockTo(player.transform);
        
        switch (bossEstate)
        {
            case BossEstate.IDLE:
                StartCoroutine(waiting());
                break;
            case BossEstate.WALK:
                StartCoroutine(walk());
                break;
            case BossEstate.JUMP:
                StartCoroutine(Jump());
                break;
            case BossEstate.ROAR:
                StartCoroutine(Roar());
                break;
            case BossEstate.ROLL_ATTACK:
                StartCoroutine(rollAttack());
                break;
            case BossEstate.SPIKE_ATTACK:
                StartCoroutine(spikes());
                break;
            case BossEstate.DEATH:
                Dead();
                break;
        }
    }

    private void newAttack()
    {
        int newState = Random.Range(1, 6);
        
        ChangeState((BossEstate)newState);           
    }
    

    IEnumerator waiting()
    {
        yield return new WaitForSeconds(2f);
        newAttack();
    }

    IEnumerator Roar()
    {
        anim.SetBool("Roar", true);
        yield return new WaitForSeconds(.5f);
        RoarParticule.Play();
        yield return StartCoroutine(InstanciateRocas());
        RoarParticule.Stop();
        anim.SetBool("Roar", false);
        yield return new WaitForSeconds(.6f);
        ChangeState(BossEstate.IDLE);
    }

    IEnumerator InstanciateRocas()
    {
        float elapse = 0;
        while (elapse < timeRocasI)
        {
            elapse += RocaSpawnRate;
            yield return new WaitForSeconds(RocaSpawnRate);
            int RocaR =  Random.Range(0, RocaProjectils.Length);
            
            Vector2 SpawnPoint = new Vector2(Random.Range(MinRocas.position.x,MaxRocas.position.x), MinRocas.position.y);
            Quaternion RandomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

            GameObject Roca =Instantiate(RocaProjectils[RocaR], SpawnPoint, RandomRotation);
            Roca.GetComponent<ConstantForce2D>().torque = Random.Range(90f, 160f);

            Camera.main.GetComponent<CaneraController>().StartShacke(timeRocasI,20,.01f,1);


        }
    }

    IEnumerator Jump()
    {
        Vector3 initialPosition = transform.position;
        
        float targetPosX =Mathf.Clamp(player.transform.position.x,MinXJump,MaxXJump) ;
        anim.SetBool("Jump", true);
        yield return new WaitForSeconds(0.7f);
        float elapse = 0;
        
        while (elapse<1)
        {
            elapse += Time.deltaTime*JumpSpeed;
            
            float X = Mathf.Lerp(initialPosition.x, targetPosX, elapse);
            float tY = 4*elapse*(1f-elapse);
            
            float Y = Mathf.Lerp(initialPosition.y,jumpHeight,tY );
            transform.position = new Vector3(X, Y, initialPosition.z);
            yield return null;
        }
        anim.SetBool("Jump", false);
        yield return new WaitForSeconds(.6f);
        ChangeState(BossEstate.IDLE);

    }

    private void Dead()
    {
        StopAllCoroutines();
    }
    IEnumerator spikes()
    {
        thrownSpikes = false;
        yield return new WaitForSeconds(.5f);
        anim.SetBool("Spike", true);
        yield return new WaitUntil(() => thrownSpikes == true);

        for (int i = 0; i < spikesSpawnPoints.Length; i++)
        {
            GameObject Spike = Instantiate(ProjectilePrefab, spikesSpawnPoints[i].position, spikesSpawnPoints[i].rotation);
            Spike.GetComponent<EnemyProjectile>().ImpulseProjectile();
        }
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("Spike", false);
        ChangeState(BossEstate.IDLE);
    }

    public void throwspikes()
    {
        thrownSpikes = true;
    }

    IEnumerator rollAttack()
    {
        anim.SetBool("Roll", true);
        yield return new WaitForSeconds(1.29f);
        IsRolling = true;
        rb.linearVelocity = transform.right * rollSpeed;
        yield return new WaitUntil(() => IsRolling == false);
        anim.SetBool("Roll", false);
        yield return new WaitForSeconds(2f);
        ChangeState(BossEstate.IDLE);
    }

    IEnumerator walk()
    {
        LoockTo(CenterP);
        anim.SetBool("Walk", true);
        yield return null;
        print(this.transform.position.x > CenterP.position.x + distanceToAttack && this.transform.position.x < CenterP.position.x - distanceToAttack);
        print("Point T "+CenterP.position);
        
        print("Boss T "+transform.position);
        while (transform.position.x > CenterP.position.x + distanceToAttack || transform.position.x < CenterP.position.x - distanceToAttack)
        {
            rb.linearVelocity = transform.right * MoveSpeed;
            yield return null;
        }
        rb.linearVelocity = Vector3.zero;
        anim.SetBool("Walk", false);
        ChangeState(BossEstate.IDLE);
    }


    private void OnCollisionStay2D(Collision2D other)
    {
        if (!IsRolling) return;
        if (!other.gameObject.CompareTag("ground")) return;

        foreach (ContactPoint2D contact in other.contacts)
        {
            
            if (Mathf.Abs(contact.normal.x) > 0.9f)
            {
                IsRolling = false;
                break;
            }
        }
        //if (Mathf.Abs(other.contacts[0].normal.x) > 0.9f) esto no va tengo que preguntalo
        //{
        //    IsRolling = false;
        //}
    }

    public override void GetDamage(float damage)
    {
        base.GetDamage(damage);
        if (IsDead) ChangeState(BossEstate.DEATH);
        else StartCoroutine(Hit());
    }

    IEnumerator Hit()
    {
        for (int i = 0; i < 3; i++)
        {
            spriteRenderer.color = hitColor;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
    }


    private void LoockTo(Transform target)
    {
        if (target.position.x > transform.position.x) this.transform.eulerAngles = Vector3.zero;// derecha
        else this.transform.eulerAngles = new Vector3(0, 180, 0);
    }
}
