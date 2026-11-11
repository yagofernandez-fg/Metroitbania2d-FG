using System;
using UnityEngine;

public class Magic : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private GameObject ParticulasExplotion;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        Destroy(gameObject, 10);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            rb.linearVelocity = Vector2.zero;
            anim.SetTrigger("Explote");            
            Vector3 direccion = (transform.position - other.transform.position).normalized;
            other.gameObject.GetComponent<EnemyController>().GetDamage(GameManager.instance.gameData.Dmg);
            other.gameObject.GetComponent<EnemyController>().GetHit(direccion);
            Instantiate(ParticulasExplotion, other.transform.position, Quaternion.identity);
            Destroy(this.gameObject,0.2f);
        }
        else if (other.gameObject.tag == "ground")
        {
            rb.linearVelocity = Vector2.zero;
            anim.SetTrigger("Explote");   
            Instantiate(ParticulasExplotion, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject,0.2f);
        }
    }
    
    

}
