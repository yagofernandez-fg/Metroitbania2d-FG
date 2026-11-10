using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RebelSecret : MonoBehaviour
{

    [SerializeField] private Tilemap tilemap;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(HacerTransparente());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(HacerOpaco());
        }
    }

    IEnumerator HacerTransparente()
    {
        Color color = tilemap.color;
        float alpha = color.a;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime;
            color.a = alpha;
            tilemap.color = color;
            yield return null;
        }
    }

    IEnumerator HacerOpaco()
    {
        Color color = tilemap.color;
        float alpha = color.a;
        while (alpha < 1)
        {
            alpha += Time.deltaTime;
            color.a = alpha;
            tilemap.color = color;
            yield return null;
        }
    }


}
