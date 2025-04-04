using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ScPlayerStats : ScStats
{
    Gamepad gamepad;
    Animator _animator;
    Rigidbody2D _rb;
    ScPlayerUseItem _playerUseItem;
    ScPlayerMovement _playerMovement;
    private bool _isTakingDamage = false;
    ScInGameUI _uiManager;
    ScStartTransition _cameraScript;

    int _baseHpMax;
    override public void Start()
    {
        _baseHpMax = _hpMax;
        gamepad = Gamepad.current;

        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();

        _cameraScript = Camera.main.GetComponent<ScStartTransition>();
        _uiManager = Camera.main.GetComponent<ScInGameUI>();

        _playerUseItem = GetComponent<ScPlayerUseItem>();
        _playerMovement = GetComponent<ScPlayerMovement>();

        if (PlayerPrefs.HasKey("HeartQt"))
        {
            _hpMax = _baseHpMax + PlayerPrefs.GetInt("HeartQt");
        }

        _hp = _hpMax;

        UpdateHpUi();

    }

    public void UpdateHpUi()
    {
        _uiManager.UpdateHp(_hp);
    }
    override public void TakeDamage(int damage)
    {
        if (_isTakingDamage) return;

        _hp -= damage;
        if (_hp < 0) _hp = 0;

        if (gamepad != null && _hp != 0)
        gamepad.SetMotorSpeeds(1,1);

        UpdateHpUi();
    }

    override public void Heal(int heal)
    {
        _hpMax = _baseHpMax + PlayerPrefs.GetInt("HeartQt");
        _hp += heal;
        if (_hp > _hpMax) _hp = _hpMax;

        UpdateHpUi();
    }

    public void PushedBack(Transform damagePos, float power)
    {
        if (_isTakingDamage) return;
        IsTakingDamage();

        _playerUseItem.CantUseItem();
        _playerMovement.CantMove();
        Vector3 direction = transform.position - damagePos.position;
        _rb.AddForce(direction.normalized * power , ForceMode2D.Impulse);
    }

    public void IsTakingDamage()
    {
        _animator.SetInteger("Hp",_hp);
        _isTakingDamage = true;
        _animator.SetBool("IsTakingDamage", _isTakingDamage);
        _animator.Play("Hurts");
    }
    public void IsntTakingDamage()
    {
        if (gamepad != null)
        gamepad.SetMotorSpeeds(0,0);

        _isTakingDamage = false;
        _animator.SetBool("IsTakingDamage", _isTakingDamage);
        _playerUseItem.CanUseItem();
        _playerMovement.CanMove();
    }

    public void OnDeath() // a appeler a la fin de l'anim de mort
    {
        _cameraScript.StartTransition(SceneManager.GetActiveScene().name);
    }
}
