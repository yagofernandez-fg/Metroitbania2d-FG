using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem runParticle;
    [SerializeField] private ParticleSystem fallParticle;
    [Range(0, 10)]
    [SerializeField] private float accourAfterVelocity;
    [Range(0, 0.2f)]
    [SerializeField] private float dustFormation;

    float counter = 0;

    private Rigidbody2D PjRb;
    private bool IsGround;

    private void Start()
    {
        PjRb = transform.parent.GetComponent<Rigidbody2D>();
        print(PjRb == null);
    }

    private void Update()
    {
        if (IsGround && Mathf.Abs(PjRb.linearVelocity.x) > accourAfterVelocity)
        {
            counter += Time.deltaTime;

            if (counter >= dustFormation)
            {
                runParticle.Play();
                counter = 0;
            }
        }
        else
        {
            counter = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ground"))
        {
            fallParticle.Play();
            IsGround = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ground"))
        {
            IsGround = false;
        }
    }
}
