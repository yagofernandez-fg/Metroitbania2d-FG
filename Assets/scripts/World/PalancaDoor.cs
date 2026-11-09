using System;
using UnityEngine;

public class PalancaDoor : MonoBehaviour
{
    [SerializeField] private Door DoorAssigned;
    private Animator PalancaAnimator;
    private bool IsActivated = false;

    private void Start()
    {
        PalancaAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("PalancaDoor");
        if (other.tag == "Weapon")
        {
            Debug.Log("PalancaDoor Triggered");
            IsActivated = !IsActivated;
            PalancaAnimator.SetBool("Activate", IsActivated);
            DoorAssigned.DoorStates(IsActivated);
        }
    }
}
