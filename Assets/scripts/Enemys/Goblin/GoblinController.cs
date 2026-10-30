using UnityEngine;

public class GoblinController : EnemyController
{
    [SerializeField] private GameObject lanzaPrefab;
    [SerializeField] private Transform lanzaSpawnPoint;
    
    
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public void shootLanza()
    {
        GameObject lanza = Instantiate(lanzaPrefab, lanzaSpawnPoint.position, lanzaSpawnPoint.rotation);
        lanza.GetComponent<EnemyProjectile>().ImpulseProjectile(PlayerTransform);
        
        
    }
    
    
    
    
}
