using System.Collections.Generic;
using UnityEngine;

public class ScLevier : ScActivable
{
    [SerializeField] List<ScDoor> _doors;

    void Start()
    {
        // check player pref pour l'état de la porte
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
            foreach (ScDoor door in _doors)
            {
                door.OpenDoor();
            }
        }
        else 
        {
            foreach (ScDoor door in _doors)
            {
                door.CloseDoor();
            }
        }
    }


}
