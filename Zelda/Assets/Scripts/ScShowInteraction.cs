using System.Collections;
using UnityEngine;

public class ScShowInteraction : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] float _size = 1f;
    [SerializeField] LayerMask _playerLayerMask;
    private bool _check = true;

    void Update() 
    {
        if (_check)
        {
            CheckIfPlayerInRange();
        }
    }

    public void StopChecking()
    {
        _check = false;
        StartCoroutine(StopShowingUI());
        _animator.SetBool("ShowButton", false);
    }

    IEnumerator StopShowingUI()
    {
        yield return new WaitForSeconds(0.2f);

    }
    void CheckIfPlayerInRange()
    {
        if (_animator == null) return;

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

    public void DestroyCanva()
    {
        Destroy(_animator.gameObject);
    }
}
