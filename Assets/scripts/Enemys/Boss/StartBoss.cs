using System;
using Unity.VisualScripting;
using UnityEngine;

public class StartBoss : MonoBehaviour
{
    public static Action OnCombatStart;

    private void Start()
    {
        if (GameManager.instance.gameData.IsBoss1Defeted)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnCombatStart?.Invoke();
            Destroy(gameObject);
        }
    }




}
