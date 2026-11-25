using System;
using System.Collections;
using UnityEngine;

public class Pinchos : MonoBehaviour
{
    [SerializeField] private Transform tpPoint;
    [SerializeField] private float damege;
    private GameObject PJ;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PJ = other.gameObject;
            other.gameObject.GetComponent<PJController>().GetDamage(damege);
            StartCoroutine(TepearPJ());
        }
    }

    IEnumerator TepearPJ()
    {
        SceneController.instance.MakeTransition(1);
        yield return new WaitForSeconds(0.85f);
        PJ.transform.position = tpPoint.position;
    }
}
