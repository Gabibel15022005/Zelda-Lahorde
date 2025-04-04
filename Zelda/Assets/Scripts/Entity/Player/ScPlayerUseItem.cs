using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScPlayerUseItem : MonoBehaviour
{
    ScInventoryUIManager _inventoryManager;
    ScPlayerMovement _player;
    Animator _animator;
    ScPlayerStats _stats;
    private bool _isUsingItem = false;
    private bool _isAttacking = false;
    private int _combo = 0;
    private bool _canUseItem = true;
    bool _canBeUsed = true;

    [Header("Bombe")]
    [SerializeField] Transform _throwDirection;
    [SerializeField] GameObject _bombe;
    [SerializeField] float _throwPower = 20;

    [Space(20)]

    [Header("Keys")]
    [SerializeField] float _size = 1f;
    [SerializeField] LayerMask _cadnaLayerMask;

    void Start()
    {
        _player = GetComponent<ScPlayerMovement>();
        _stats = GetComponent<ScPlayerStats>();
        _animator = GetComponent<Animator>();
        _inventoryManager = Camera.main.GetComponent<ScInGameUI>().GetInventoryManager();
    }

    public void OnItem1(InputAction.CallbackContext context)
    {
        if (_isUsingItem) return;
        if (context.started)
        {
            if (Time.timeScale == 0) _inventoryManager.OnItem1();
            else UseItem(PlayerPrefs.GetString($"ToolBarItem{1}"));
        }
    }
    public void OnItem2(InputAction.CallbackContext context)
    {
        if (_isUsingItem) return;
        if (context.started)
        {
            if (Time.timeScale == 0) _inventoryManager.OnItem2();
            else UseItem(PlayerPrefs.GetString($"ToolBarItem{2}"));

        }
    }
    public void OnItem3(InputAction.CallbackContext context)
    {
        if (_isUsingItem) return;
        if (context.started)
        {
            if (Time.timeScale == 0) _inventoryManager.OnItem3();
            else UseItem(PlayerPrefs.GetString($"ToolBarItem{3}"));

        }
    }
    public void CanUseItem()
    {
        _canUseItem = true;
    }
    public void CantUseItem()
    {
        _canUseItem = false;
    }
    private void UseItem(string name)
    {
        if (name == "") return;
        if (!_canUseItem) return;

        _canBeUsed = true;

        SetIsUsingItem();

        if (PlayerPrefs.GetInt($"{name}Qt") <= 0) // si j'en ai pas assez
        {
            PlayerPrefs.SetInt($"{name}Qt",0);
            PlayerPrefs.Save();
            SetIsUsingItem();
            return;
        }

        switch (name)
        {
            case "Sword":
                Debug.Log(name);
                UseSword();
            break;

            case "Bombe":
                Debug.Log(name);
                UseBomb();
            break;

            case "Magic Staff":
                Debug.Log(name);
                UseMagicStaff();
            break;

            case "Red Potion":
                Debug.Log(name);
                UseRedPotion();
            break;

            case "Pink Key":
                Debug.Log(name);
                UsePinkKey();
            break;

            default:
                Debug.Log("The item name is not in the list of behaviour");
                SetIsUsingItem();
            break;
        }

        if (PlayerPrefs.GetInt($"{name}IsConsommable") == 1 && _canBeUsed) // si consommable 
        {
            PlayerPrefs.SetInt($"{name}Qt",PlayerPrefs.GetInt($"{name}Qt") - 1);
            
            _inventoryManager.UpdateToolBarItems();
        }

    }

    private void UseRedPotion()
    {
        _stats.Heal(1);
        SetIsUsingItem();
    }
    private void UseBomb()
    {
        Vector3 direction = _throwDirection.position - transform.position;

        GameObject bombe = Instantiate(_bombe,_throwDirection.position, Quaternion.identity);
        Rigidbody2D rb = bombe.GetComponent<Rigidbody2D>();

        rb.AddForce(direction.normalized * _throwPower, ForceMode2D.Impulse);

        SetIsUsingItem();
    }
    private void UsePinkKey()
    {
        // check around 
        Collider2D[] checkCadna = Physics2D.OverlapCircleAll(transform.position, _size, _cadnaLayerMask);

        // if cadna a porté et bonne couleur
        if (checkCadna.Length > 0 )
        {
            Debug.Log("UseKey");
            checkCadna[0].GetComponent<ScCadna>().Unlock();
        }
        else
        {
            _canBeUsed = false;
        }

        SetIsUsingItem();
    }
    private void UseMagicStaff()
    {
        // trouve un sort pour chaque staff
        SetIsUsingItem();
    }
    private void UseSword()
    {
        _player.CantMove(); // arrete le joueur avec cantmove
        SetIsAttacking(); // met _isAttacking à true
    }
    public void ComboLv()
    {
        Debug.Log("ComboLv is called");
        _combo++;
        if (_combo == 3) _combo = 0;
        _animator.SetInteger("ComboLv",_combo);
    }
    public void SetIsAttacking()
    {
        _isAttacking = !_isAttacking;
        _animator.SetBool("IsAttacking",_isAttacking);
    }
    public void SetIsUsingItem()
    {
        _isUsingItem = !_isUsingItem;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position,_size);
    }

}
