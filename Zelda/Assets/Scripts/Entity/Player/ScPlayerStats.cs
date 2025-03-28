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

    override public void Start()
    {
        gamepad = Gamepad.current;
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();

        _uiManager =Camera.main.GetComponent<ScInGameUI>();

        _playerUseItem = GetComponent<ScPlayerUseItem>();
        _playerMovement = GetComponent<ScPlayerMovement>();

        if (!PlayerPrefs.HasKey("Player_hpMax"))
        {
            PlayerPrefs.SetInt("Player_hpMax",_hpMax);
            PlayerPrefs.Save();
            Debug.Log("Didn't have Player_hpMax");
        }

        _hpMax = PlayerPrefs.GetInt("Player_hpMax");
        _hp = _hpMax;

        //Debug.Log($"_hp : {_hp}");
        _uiManager.UpdateHp(_hp);

    }
    override public void TakeDamage(int damage)
    {
        if (_isTakingDamage) return;

        _hp -= damage;
        if (_hp < 0) _hp = 0;

        if (gamepad != null && _hp != 0)
        gamepad.SetMotorSpeeds(1,1);

        _uiManager.UpdateHp(_hp);
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
        // faire la transition a l'écran noir 
        // reload la scene a la fin

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
