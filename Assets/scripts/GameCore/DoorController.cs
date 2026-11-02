using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [SerializeField] private string SceneTrabel;
    [SerializeField] private int DoorNum;
    


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.doorToGo = DoorNum;
            SceneManager.LoadScene(SceneTrabel);
        }
    }
}
