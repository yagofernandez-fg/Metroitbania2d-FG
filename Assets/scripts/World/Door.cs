using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private int id;
    private Animator DoorAnimator;
    private bool IsClosed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DoorAnimator = GetComponent<Animator>();
        if (GameManager.instance.scenarioData.DoorOpen)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    public void DoorStates(bool open)
    {
        if (open) OpenDoor();
        else CloseDoor();
    }

    private void OpenDoor()
    {
        IsClosed = false;
        DoorAnimator.SetBool("Open",true);
        GameManager.instance.scenarioData.DoorOpen = true;
    }

    private void CloseDoor()
    {
        IsClosed = true;
        DoorAnimator.SetBool("Open", false);
        GameManager.instance.scenarioData.DoorOpen = false;
    }
}
