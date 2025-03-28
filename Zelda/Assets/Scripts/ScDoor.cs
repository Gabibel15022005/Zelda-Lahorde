using UnityEngine;

public class ScDoor : MonoBehaviour
{
    [SerializeField] Animator _animator;
    public void OpenDoor()
    {
        _animator.Play("OpenDoor");
    }
    public void CloseDoor()
    {
        _animator.Play("CloseDoor");
    }
}
