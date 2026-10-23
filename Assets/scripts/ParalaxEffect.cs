using System;
using Unity.VisualScripting;
using UnityEngine;

public class ParalaxEffect : MonoBehaviour
{
    private Transform cameraTransform;
    [SerializeField] private float porcentaje;
    private Vector3 previosPosition;
    

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    public void UpdateParalaxEffect()
    {
        Vector3 deltaPosition = cameraTransform.position - previosPosition;
        this.transform.Translate(deltaPosition*porcentaje, Space.World);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        previosPosition = cameraTransform.position;
    }
}
