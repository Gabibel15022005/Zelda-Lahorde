using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScInventorySlot : MonoBehaviour
{
    public NewScriptableObjectScript Obj;
    [SerializeField] Image _image;
    [SerializeField] Image _QtImage;
    int _qt;
    [SerializeField] int _isToolBarNb = 0;

    void Start()
    {
        if (Obj == null)
        {
            AbleQt(false);
        }

        if (_isToolBarNb != 0)
        {
            List<NewScriptableObjectScript> objects = 
            Camera.main.GetComponent<ScInGameUI>().GetInventoryManager().GetObjets();

            NewScriptableObjectScript obj = null; // L'objet est de base à null

            if (!PlayerPrefs.HasKey($"ToolBarItem{_isToolBarNb}") && PlayerPrefs.GetString($"ToolBarItem{_isToolBarNb}") != null) // je vérifie si le PlayerPrefs existe déjà
            {
                PlayerPrefs.SetString($"ToolBarItem{_isToolBarNb}", null);
                PlayerPrefs.SetInt($"ToolBarItem{_isToolBarNb}IsConsommable", 0);
                PlayerPrefs.SetInt($"ToolBarItem{_isToolBarNb}Qt", 0);
                PlayerPrefs.Save();
            }

            foreach (NewScriptableObjectScript @object in objects) // je cherche le dans ma liste d'objet possible le bon objet
            {
                if (@object.name == PlayerPrefs.GetString($"ToolBarItem{_isToolBarNb}"))
                {   
                    obj = @object; // si je le trouve je remplace l'objet null par celui ci
                }
            }

            Obj = obj; // je l'assigne au slot correspondant
            UpdateValues(); // je met à jour ses valeurs
        }
    }
    public void UpdateValues()
    {
        if (Obj != null)
        _image.sprite = Obj.Sprite;
        else
        {
            _image.sprite = null;
            AbleQt(false);
        }

        if (_image.sprite != null) _image.gameObject.SetActive(true);
        else _image.gameObject.SetActive(false);

        

        if (Obj == null) return;

        _qt = PlayerPrefs.GetInt(Obj.name + "Qt");
        _QtImage.GetComponentInChildren<TMP_Text>().text = _qt.ToString();

        //Debug.Log($"Checking : {Obj.name}IsConsommable = {PlayerPrefs.GetInt($"{Obj.name}IsConsommable")}");
        if (PlayerPrefs.GetInt($"{Obj.name}IsConsommable") == 0) AbleQt(false);
        else AbleQt(true);

        if (_isToolBarNb != 0)
        {
            PlayerPrefs.SetString($"ToolBarItem{_isToolBarNb}", Obj.name);
            PlayerPrefs.SetInt($"ToolBarItem{_isToolBarNb}IsConsommable", PlayerPrefs.GetInt($"{Obj.name}IsConsommable"));
            PlayerPrefs.SetInt($"ToolBarItem{_isToolBarNb}Qt", PlayerPrefs.GetInt(Obj.name + "Qt"));
            PlayerPrefs.Save();
        }

        
    }
    public void AbleQt(bool value)
    {
        _QtImage.gameObject.SetActive(value);
    }

}
