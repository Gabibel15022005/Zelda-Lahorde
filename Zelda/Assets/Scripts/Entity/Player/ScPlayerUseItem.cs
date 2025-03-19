using UnityEngine;
using UnityEngine.InputSystem;

public class ScPlayerUseItem : MonoBehaviour
{
    ScInventoryUIManager _inventoryManager;
    void Start()
    {
        _inventoryManager = Camera.main.GetComponent<ScInGameUI>().GetInventoryManager();
        Debug.Log(_inventoryManager);
    }

    public void OnItem1(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (Time.timeScale == 0) _inventoryManager.OnItem1();
            else UseItem(PlayerPrefs.GetString($"ToolBarItem{1}"));
        }
    }
    public void OnItem2(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (Time.timeScale == 0)
            {
                _inventoryManager.OnItem2();
            }
            else
            {
                UseItem(PlayerPrefs.GetString($"ToolBarItem{2}"));
            }

        }
    }
    public void OnItem3(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (Time.timeScale == 0)
            {
                _inventoryManager.OnItem3();
            }
            else
            {
                UseItem(PlayerPrefs.GetString($"ToolBarItem{3}"));
            }

        }
    }

    private void UseItem(string name)
    {
        if (name == "") return;
        
        if (PlayerPrefs.GetInt($"{name}Qt") <= 0) // si j'en ai pas assez
        {
            PlayerPrefs.SetInt($"{name}Qt",0);
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
            break;

            case null:
                Debug.Log("Error : the name of the item used is null");
            break;

            default:
                Debug.Log("The item name is not in the list of behaviour");
            break;
        }

        PlayerPrefs.Save();
    }

}
