using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SecretBossRebel : RebelSecret
{
    [SerializeField] private Light2D[] lights;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
