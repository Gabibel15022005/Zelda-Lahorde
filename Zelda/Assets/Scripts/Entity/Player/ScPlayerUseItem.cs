using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScPlayerUseItem : MonoBehaviour
{
    ScInventoryUIManager _inventoryManager;
    ScPlayerMovement _player;
    Animator _animator;
    private bool _isUsingItem = false;
    private bool _isAttacking = false;
    private int _combo = 0;
    private bool _canUseItem = true;
    void Start()
    {
        _player = GetComponent<ScPlayerMovement>();
        _animator = GetComponent<Animator>();
        _inventoryManager = Camera.main.GetComponent<ScInGameUI>().GetInventoryManager();
        Debug.Log(_inventoryManager);
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

        SetIsUsingItem();

        if (PlayerPrefs.GetInt($"{name}Qt") <= 0) // si j'en ai pas assez
        {
            PlayerPrefs.SetInt($"{name}Qt",0);
            PlayerPrefs.Save();
            return;
        }
        if (PlayerPrefs.GetInt($"{name}IsConsommable") == 1) // si consommable 
        {
            PlayerPrefs.SetInt($"{name}Qt",PlayerPrefs.GetInt($"{name}Qt") - 1);
            
            _inventoryManager.UpdateToolBarItems();
        }

        switch (name)
        {
            case "Item Test":
                Debug.Log(name);
            break;

            case "Sword":
                Debug.Log(name);
                UseSword();
            break;

            default:
                Debug.Log("The item name is not in the list of behaviour");
            break;
        }
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
}
