using System;
using System.Collections;
using UnityEngine;

public class PlataformScript : MonoBehaviour
{

    private Collider2D col;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            float y = Input.GetAxis("Vertical");
            if (y<0)//hacia abajo
            {
                col.enabled = false;
                StartCoroutine(CliderCold());
            }
        }
    }

    IEnumerator CliderCold()
    {
        yield return new WaitForSeconds(0.85f);
        col.enabled = true;
    }
}
