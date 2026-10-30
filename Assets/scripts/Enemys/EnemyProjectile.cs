using System;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float ImpulseForce;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float dmg;
    private Collider2D collider;
    private bool IsInAir = false;
    
    private Transform player;
    private float Maxy =0;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        collider = gameObject.GetComponent<Collider2D>();
        Destroy(this.gameObject, 3);
    }

    private void FixedUpdate()
    {
        if (!IsInAir)return;
        float newY=0;
        int derecha = 1;
        if (transform.position.x <= player.position.x)
        {
           derecha = -1;
        }
        if (Maxy>=newY) // vajando
        {
            newY = transform.position.y;
            transform.eulerAngles += new Vector3(0, 0, -derecha);
        }
        else
        {
            transform.eulerAngles += new Vector3(0, 0, derecha);
        }
        
        Maxy = transform.position.y;
        
        
    }

    public void ImpulseProjectile(Transform PlayerTransform)
    {
        IsInAir = true;
        player = PlayerTransform;
         PlayerTransform.position = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y+1, transform.position.z);
        Vector3 dir = (PlayerTransform.position - transform.position).normalized;
        
        rb.AddForce(dir * ImpulseForce);
        
        
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle-180);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PJController>().GetDamage(dmg);
            Destroy(gameObject);
        }
        IsInAir = false;
        rb.linearVelocity = Vector2.zero;
        collider.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
    }
}
