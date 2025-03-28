using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScLevier : ScActivable
{
    [SerializeField] List<ScDoor> _doors;

    void Start()
    {
        if (PlayerPrefs.HasKey($"{transform.parent.name}InScene({SceneManager.GetActiveScene().name})"))
        {
            if (PlayerPrefs.GetInt($"{transform.parent.name}InScene({SceneManager.GetActiveScene().name})") == 1)
            {
                Activate(); // porte ouverte
            }
        }
    }
    public override void Activate()
    {
        _isActivate = !_isActivate;  // active ou désactive le levier
        ChangeDoorState();
    }

    void ChangeDoorState()
    {
        if (_isActivate)
        {
            PlayerPrefs.SetInt($"{transform.parent.name}InScene({SceneManager.GetActiveScene().name})",1);
            foreach (ScDoor door in _doors)
            {
                door.OpenDoor();
            }
        }
        else 
        {
            PlayerPrefs.SetInt($"{transform.parent.name}InScene({SceneManager.GetActiveScene().name})",0);
            foreach (ScDoor door in _doors)
            {
                door.CloseDoor();
            }
        }
        
        PlayerPrefs.Save();
    }


}
