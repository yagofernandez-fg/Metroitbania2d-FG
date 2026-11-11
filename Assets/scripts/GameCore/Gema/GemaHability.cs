using System;
using Unity.VisualScripting;
using UnityEngine;

enum GemaType
{
    DOBLE_JUMP,
    FIREBALL,
}

public class GemaHability : MonoBehaviour
{
    [SerializeField] private GemaType GemaType;
    public String Menssage;
    private ParticleSystem psIdle;
    private ParticleSystem psGet;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        psIdle = transform.GetChild(0).GetComponent<ParticleSystem>();
        psGet = transform.GetChild(1).GetComponent<ParticleSystem>();
        
        switch (GemaType)
        {
            case GemaType.DOBLE_JUMP:
                if(GameManager.instance.gameData.MaxJump >1) Destroy(gameObject);
                break;
            default:
                Debug.Log("Not TypeGema Assigned or not defined");
                return;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            switch (GemaType)
            {
                case GemaType.DOBLE_JUMP:
                    GameManager.instance.gameData.MaxJump++;
                    psGet.gameObject.SetActive(true);
                    psGet.Play();
                    break;
                case GemaType.FIREBALL:
                    break;
                default:
                    Debug.Log("GemaHability Triggered but not set or ucknown");
                    return;
                    break;
            }
            Time.timeScale = 0;
            psIdle.gameObject.SetActive(false);
        }
    }
}
