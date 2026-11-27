using System.Collections;
using UnityEngine;

public class openChest : MonoBehaviour
{
    [SerializeField] private int totalCurrency;
    [SerializeField] private GameObject currencyPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            // Open the chest
            Animator animator = GetComponent<Animator>();
            if (animator != null)
            {
                gameObject.GetComponent<Collider2D>().enabled = false;
                animator.SetTrigger("open");
                StartCoroutine(OpenChestCoroutine());
            }
            // Optionally, you can add logic to give the player items or rewards here
        }
    }

    IEnumerator OpenChestCoroutine()
    {
        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < totalCurrency; i++)
        {
            float randomX = Random.Range(-1f, 1f);
            float randomY = Random.Range(-.5f, .5f);

            Vector3 spawnPosition = transform.position + new Vector3(randomX, randomY, 0);

            Instantiate(currencyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}

