using System.Collections;
using UnityEngine;

public class DestructibleWalls : MonoBehaviour
{
    [SerializeField]private int health;
    [SerializeField] private GameObject BitsCon;
    private ParticleSystem particleSystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        particleSystem = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.gameObject.CompareTag("Weapon"))
        {
            health--;
            particleSystem.Play();
            StartCoroutine(ShackeWall());
            if (health <= 0)
            {
                BitsCon.transform.SetParent(null);
                BitsCon.SetActive(true);
                AddForcesToRocs();
                Destroy(gameObject);
            }
        }
    }


    IEnumerator ShackeWall()
    {
        Vector3 originalPosition = transform.position;

        float i = 0f;
        while (i < 0.2f)//0.2 s
        {
            float x = Random.Range(-1f, 1f) * 0.1f;
            float y = Random.Range(-1f, 1f) * 0.1f;
            transform.position = originalPosition + new Vector3(x, y, 0);
            i += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPosition;
    }


    private void AddForcesToRocs()
    {
        foreach (Transform roc in BitsCon.transform)
        {
            Rigidbody2D rb = roc.GetComponent<Rigidbody2D>();

            Vector2 Dir = new Vector2(1, Random.Range(-1f, 1f));
            float force = Random.Range(2f, 3f);
            float torque = Random.Range(-1.5f, 1.5f);

            rb.AddForce(Dir * force, ForceMode2D.Impulse);

            rb.AddTorque(torque, ForceMode2D.Impulse);
        }
    }

}
