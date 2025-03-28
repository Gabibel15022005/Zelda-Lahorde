using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScBattleZone : MonoBehaviour
{
    bool _isInBattle = false; // check if in battle
    bool _hasAlreadyWon = false; // check prefab
    [SerializeField] List<ScDoor> _doors;
    ScEnemySpawnerManager _enemySpawner; // object contenant le script de spawn et de décompte
    [SerializeField] float _delayBeforeSpawn = 1f;

    void Start() 
    {
        _enemySpawner = GetComponent<ScEnemySpawnerManager>();

        if (PlayerPrefs.HasKey($"{gameObject.name}InScene({SceneManager.GetActiveScene().name})"))
        {
            _hasAlreadyWon = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_hasAlreadyWon || _isInBattle) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            _isInBattle = true;
            StartCoroutine(StartBattle());
        }
    }

    IEnumerator StartBattle()
    {
        CloseAllDoor();
        yield return new WaitForSeconds(_delayBeforeSpawn);
        _enemySpawner.SpawnEnemies();
    }

    public void EndOfTheFight()
    {
        PlayerPrefs.SetInt($"{gameObject.name}InScene({SceneManager.GetActiveScene().name})",1);
        PlayerPrefs.Save();
        OpenAllDoor();
    }

    private void CloseAllDoor()
    {
        foreach (ScDoor door in _doors)
        {
            door.CloseDoor();
        }
    }

    private void OpenAllDoor()
    {
        foreach (ScDoor door in _doors)
        {
            door.OpenDoor();
        }
    }
}
