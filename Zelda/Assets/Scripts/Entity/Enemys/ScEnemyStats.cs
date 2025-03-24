using System.Collections.Generic;
using UnityEngine;

public class ScEnemyStats : ScStats
{
    Animator _animator;
    Collider2D _collider2D;
    Rigidbody2D _rb;
    private bool _takeDamage = false;
    [SerializeField] List<GameObject> _objPossible;

    override public void Start()
    {
        _animator = GetComponent<Animator>();
        _collider2D = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();

        _hp = _hpMax;
        _animator.SetInteger("Hp",_hp);

    }
    public override void TakeDamage(int damage)
    {
        if (_takeDamage) return;
        _takeDamage = true;

        _hp -= damage;
        if (_hp < 0) _hp = 0;

        if (_hp <= 0) _collider2D.enabled = false;

        _animator.SetInteger("Hp",_hp);
        _animator.SetBool("TakeDamage",_takeDamage);
    }

    public void PushedBack(Transform damagePos, float power)
    {
        Vector3 direction = transform.position - damagePos.position;

        _rb.AddForce(direction.normalized * power , ForceMode2D.Impulse);
    }

    private void DropItem()
    {
        if (_objPossible.Count > 0)
        {
            int index = Random.Range(0,_objPossible.Count);

            if (_objPossible[index] != null)
            Instantiate(_objPossible[index], transform.position, Quaternion.identity);
        }
    }
    private void CanTakeDamage()
    { 
        _takeDamage = false; 
        _animator.SetBool("TakeDamage",_takeDamage);
    }
}
