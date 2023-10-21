using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator doorAnimator;
    private bool isOpen = false;

    void Start()
    {
        doorAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isOpen)
        {
            doorAnimator.SetTrigger("Open");
            isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && isOpen)
        {
            doorAnimator.SetTrigger("Close");
            isOpen = false;
        }
    }
}