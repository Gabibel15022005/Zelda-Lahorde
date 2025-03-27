using UnityEngine;

public class ScDoor : MonoBehaviour
{
    Animator _animator;
    void Start() 
    {
        _animator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        _animator.Play("OpenDoor");
    }
    public void CloseDoor()
    {
        _animator.Play("CloseDoor");
    }
}
