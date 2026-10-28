using UnityEngine;

public class DetectionArea : MonoBehaviour
{
    private EnemyController EnemyController;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyController = transform.parent.GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            EnemyController.ChasePlayer(other.transform,true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            EnemyController.ChasePlayer(other.transform,false);
        }
    }
}
