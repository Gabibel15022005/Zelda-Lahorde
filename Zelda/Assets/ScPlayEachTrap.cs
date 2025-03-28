using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScPlayEachTrap : MonoBehaviour
{
    [SerializeField] List<ScDeadlyTraps> _traps;
    [SerializeField] float _delayBeetweenTraps = 0.2f;
    public bool IsFast = true;

    bool _isActive = true;

    void Start() 
    {
        StartCoroutine(WaitForTraps());
    }

    IEnumerator WaitForTraps()
    {
        yield return new WaitForSeconds(2);

        foreach (ScDeadlyTraps trap in _traps)
        {
            trap.SetIsLoop(false);
            trap.IsFast = IsFast;
        }

        StartCoroutine(PlayEachTrap());
    }

    IEnumerator PlayEachTrap()
    {
        //Debug.Log("Is Acrive");

        foreach (ScDeadlyTraps trap in _traps)
        {
            //Debug.Log($"{trap.name}");
            if (!_isActive) StopAllCoroutines();
            trap.StartAnime();
            yield return new WaitForSeconds(_delayBeetweenTraps);
        }

        if (_isActive)
        {
            StartCoroutine(PlayEachTrap());
        }
    }

}
