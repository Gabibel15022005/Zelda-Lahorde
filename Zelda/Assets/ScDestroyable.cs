using System.Collections.Generic;
using UnityEngine;

public class ScDestroyable : ScStats
{
    Animator _animator;
    Collider2D _collider2D;
    [SerializeField] List<GameObject> _objPossible;
    override public void Start()
    {
        _animator = GetComponent<Animator>();
        _collider2D = GetComponent<Collider2D>();

        _hp = _hpMax;
        _animator.SetInteger("Hp",_hp);

    }
    public override void TakeDamage(int damage)
    {
        _hp -= damage;
        if (_hp < 0) _hp = 0;
        
        if (_hp <= 0) _collider2D.enabled = false;

        _animator.SetInteger("Hp",_hp);
        _animator.SetBool("TakeDamage",true);
    }

    private void DropItem()
    {
        if (_objPossible.Count > 0)
        {
            Instantiate(_objPossible[Random.Range(0,_objPossible.Count)], transform.position, Quaternion.identity);
        }
    }
}
