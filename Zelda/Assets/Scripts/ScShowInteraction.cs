using UnityEngine;

public class ScShowInteraction : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] float _size = 1f;
    [SerializeField] LayerMask _playerLayerMask;

    void Update() 
    {
        CheckIfPlayerInRange();
    }

    void CheckIfPlayerInRange()
    {
        Collider2D[] playerCollider = Physics2D.OverlapCircleAll(transform.position, _size, _playerLayerMask);

        if (playerCollider.Length > 0)
        {
            _animator.SetBool("ShowButton", true);
        }
        else
        {
            _animator.SetBool("ShowButton", false);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _size);
    }
}
