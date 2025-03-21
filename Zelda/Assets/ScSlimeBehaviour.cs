using System;
using System.Collections;
using UnityEngine;

public class ScSlimeBehaviour : MonoBehaviour
{
    [SerializeField] float _size = 2f;
    [SerializeField] LayerMask _targetLayerMask;
    [SerializeField] float _delayBetweenMove = 1f;
    ScEnemyStats _enemyStats;
    Rigidbody2D _rb;
    Collider2D _target;

    private bool _hasFoundTarget = false;
    private bool _isChasing = false;

    void Start()
    {
        _enemyStats = GetComponent<ScEnemyStats>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update() 
    {
        CheckAround();
        
        if (_enemyStats.GetHp() <= 0)
        {
            SlimeIsDead();
        }
    }

    private void SlimeIsDead()
    {
        enabled = false;
        StopAllCoroutines();
    }

    private void CheckAround()
    {
        Collider2D[] targetCheck = Physics2D.OverlapCircleAll(transform.position, _size, _targetLayerMask);

        _hasFoundTarget = targetCheck.Length > 0;

        if (_hasFoundTarget) _target = targetCheck[0];
        else _target = null;

        if (_hasFoundTarget && !_isChasing)
        {
            _isChasing = true;
            StartCoroutine(ChaseTarget());
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _size);
    }

    IEnumerator ChaseTarget()
    {
        while (_target != null)
        {
            Vector3 direction = _target.transform.position - transform.position;
            _rb.AddForce(direction.normalized * _enemyStats.GetSpeed(), ForceMode2D.Impulse);
            yield return new WaitForSeconds(_delayBetweenMove);
        }

        _isChasing = false;
    }
}
