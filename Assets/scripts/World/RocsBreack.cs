using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RocsBreack : MonoBehaviour
{
    private SpriteRenderer sprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(HacerTransparente());
    }

    IEnumerator HacerTransparente()
    {
        Color color = sprite.color;
        float alpha = color.a;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime;
            color.a = alpha;
            sprite.color = color;
            yield return null;
        }
        Destroy(gameObject);
    }
}
