using UnityEngine;

public class StartBoss : MonoBehaviour
{
    [SerializeField] private Transform RoomCenter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Camera.main.GetComponent<CaneraController>().enabled = false;
            Camera.main.GetComponent<Camera>().orthographicSize = 7;
            Camera.main.transform.position = RoomCenter.position;
        }
    }




}
