using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ScInventoryUIManager : MonoBehaviour
{
    [SerializeField] List<NewScriptableObjectScript> _objects = new List<NewScriptableObjectScript>();
    public List<Button> ToolBarItems = new List<Button>();
    [SerializeField] private EventSystem _eventSystem;

    #region Inventory Behaviour

    void Start()
    {
        UpdateAllButtonInInventory();
    }
    void OnEnable() 
    {
        UpdateAllButtonInInventory();
        UpdateToolBarItems();
    }
    public void UpdateToolBarItems()
    {
        int index = 1;  // pour ToolBarItem1 , 2 , 3, etc

        foreach (Button item in ToolBarItems)
        {
            NewScriptableObjectScript obj = null; // L'objet est de base à null

            ScInventorySlot slot = item.GetComponent<ScInventorySlot>();    // je selectionne le script qui m'intéresse dans le boutton

            if (!PlayerPrefs.HasKey($"ToolBarItem{index}") && PlayerPrefs.GetString($"ToolBarItem{index}") != null) // je vérifie si le PlayerPrefs existe déjà
            {
                PlayerPrefs.SetString($"ToolBarItem{index}", null);
                PlayerPrefs.SetInt($"ToolBarItem{index}IsConsommable", 0);
                PlayerPrefs.SetInt($"ToolBarItem{index}Qt", 0);
                PlayerPrefs.Save();
            }

            foreach (NewScriptableObjectScript @object in _objects) // je cherche le dans ma liste d'objet possible le bon objet
            {
                if (@object.name == PlayerPrefs.GetString($"ToolBarItem{index}"))
                {   
                    obj = @object; // si je le trouve je remplace l'objet null par celui ci
                }
            }
            slot.Obj = obj; // je l'assigne au slot correspondant
            slot.UpdateValues(); // je met à jour ses valeurs

            index++; // j'incrémente pour le slot suivant
        }
    }
    void UpdateAllButtonInInventory()
    {
        int index = 0;
        Button[] buttons = GetComponentsInChildren<Button>();

        foreach (NewScriptableObjectScript @object in _objects) // parcours la liste des items possibles
        {
            ScInventorySlot slot = buttons[index].gameObject.GetComponent<ScInventorySlot>(); // select the correct button

            if (PlayerPrefs.HasKey($"{@object.name}Qt"))
            {
                slot.Obj = @object;
                index++;
            }
            else
            slot.Obj = null;

            //Debug.Log($"{slot.gameObject.name} will become {slot.Obj}");
            slot.UpdateValues();

        }
    }

    public void OnItem1()
    {
        // récup le composant ScInventorySlot du bouton actuellement selectionner quand j'appuie sur l'item1 
        ScInventorySlot slot = _eventSystem.currentSelectedGameObject.GetComponent<ScInventorySlot>();

        if (slot.Obj != null)
        {
            PlayerPrefs.SetString($"ToolBarItem{1}", slot.Obj.name);
            if (slot.Obj.IsConsommable)
            PlayerPrefs.SetInt($"ToolBarItem{1}IsConsommable", 1); 
            else
            PlayerPrefs.SetInt($"ToolBarItem{1}IsConsommable", 0);
            PlayerPrefs.SetInt($"ToolBarItem{1}Qt", PlayerPrefs.GetInt($"{slot.Obj.name}Qt"));
        }
        else
        {
            PlayerPrefs.SetString($"ToolBarItem{1}", null);
            PlayerPrefs.SetInt($"ToolBarItem{1}IsConsommable", 0); 
            PlayerPrefs.SetInt($"ToolBarItem{1}Qt", 0);
        }

        PlayerPrefs.Save();
        UpdateToolBarItems();
    }
    public void OnItem2()
    {
        // récup le composant ScInventorySlot du bouton actuellement selectionner quand j'appuie sur l'item1 
        ScInventorySlot slot = _eventSystem.currentSelectedGameObject.GetComponent<ScInventorySlot>();

        if (slot.Obj != null)
        {
            PlayerPrefs.SetString($"ToolBarItem{2}", slot.Obj.name);
            if (slot.Obj.IsConsommable)
            PlayerPrefs.SetInt($"ToolBarItem{2}IsConsommable", 1); 
            else
            PlayerPrefs.SetInt($"ToolBarItem{2}IsConsommable", 0);
            PlayerPrefs.SetInt($"ToolBarItem{2}Qt", PlayerPrefs.GetInt($"{slot.Obj.name}Qt"));
        }
        else
        {
            PlayerPrefs.SetString($"ToolBarItem{2}", null);
            PlayerPrefs.SetInt($"ToolBarItem{2}IsConsommable", 0); 
            PlayerPrefs.SetInt($"ToolBarItem{2}Qt", 0);
        }

        PlayerPrefs.Save();
        UpdateToolBarItems();
    }
    public void OnItem3()
    {
        // récup le composant ScInventorySlot du bouton actuellement selectionner quand j'appuie sur l'item1 
        ScInventorySlot slot = _eventSystem.currentSelectedGameObject.GetComponent<ScInventorySlot>();

        if (slot.Obj != null)
        {
            PlayerPrefs.SetString($"ToolBarItem{3}", slot.Obj.name);
            if (slot.Obj.IsConsommable)
            PlayerPrefs.SetInt($"ToolBarItem{3}IsConsommable", 1); 
            else
            PlayerPrefs.SetInt($"ToolBarItem{3}IsConsommable", 0);
            PlayerPrefs.SetInt($"ToolBarItem{3}Qt", PlayerPrefs.GetInt($"{slot.Obj.name}Qt"));
        }
        else
        {
            PlayerPrefs.SetString($"ToolBarItem{3}", null);
            PlayerPrefs.SetInt($"ToolBarItem{3}IsConsommable", 0); 
            PlayerPrefs.SetInt($"ToolBarItem{3}Qt", 0);
        }

        PlayerPrefs.Save();
        UpdateToolBarItems();

    }

    public List<NewScriptableObjectScript> GetObjets()
    {
        return _objects;
    }


}

#endregion


