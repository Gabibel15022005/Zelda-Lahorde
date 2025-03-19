using UnityEngine;

public class ScStats : MonoBehaviour
{
    [SerializeField] protected int _hpMax;
    protected int _hp;
    [SerializeField] protected float _speed;
    virtual public void Start()
    {
        _hp = _hpMax;
    }
    virtual public void TakeDamage(int damage)
    {
        _hp -= damage;
        if (_hp < 0) _hp = 0;
    }
    public void Heal(int heal)
    {
        _hp += heal;
        if (_hp > _hpMax) _hp = _hpMax;
    }
    public void FullHeal()
    {
        _hp = _hpMax;
    }
    public float GetSpeed() {return _speed;}
    public float GetHp() {return _hp;}
    public float GetHpMax() {return _hpMax;}


}
