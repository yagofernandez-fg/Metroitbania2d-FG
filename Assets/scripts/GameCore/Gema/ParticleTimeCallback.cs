using System;
using UnityEngine;

public class ParticleTimeCallback : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleSystemStopped()
    {
        
        string tmp = transform.parent.GetComponent<GemaHability>().Menssage;
        GameObject.Find("LvlManager").GetComponent<LvlManager>().SowMensage(tmp);
        Destroy(transform.parent.gameObject);
    }
}
