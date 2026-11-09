using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator DoorAnimator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DoorAnimator = GetComponent<Animator>();
    }

    public void DoorStates(bool open)
    {
        if (open) OpenDoor();
        else CloseDoor();
    }

    private void OpenDoor()
    {
        DoorAnimator.SetBool("Open",true);
    }

    private void CloseDoor()
    {
        DoorAnimator.SetBool("Open", false);
    }
}
