using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScKillAllEnemy : ScQuest
{
    [SerializeField] List<ScEnemyStats> _enemies;    // liste d'enemy pour check leur mort (il faut qu'il soit en enfant)
    [SerializeField] bool _dontSpawnAfterWinning = false;    // bool pour si c'est un enemy unique
    private int _nb = 0;

    public override void Start()
    {
        _nb = _enemies.Count;

        if (_quest != null)
        {
            Debug.Log($"Changed the name of _questName to : {_quest.QuestName}");
            _questName = _quest.QuestName;
        }

        if (_dontSpawnAfterWinning && PlayerPrefs.HasKey($"{gameObject.name}InScene({SceneManager.GetActiveScene().name})"))
        {
            foreach (ScEnemyStats enemy in _enemies)
            {
                enemy.gameObject.SetActive(false);
            }
        }
    }

    public void RemoveFromEnemyCount()
    {
        _nb--;
        if (_nb <= 0) 
        {
            Debug.Log("You killed all enemies !");
            PlayerPrefs.SetInt($"{gameObject.name}InScene({SceneManager.GetActiveScene().name})",1);
            PlayerPrefs.Save();
            ChangeQuestProgress();
        }
    }
}
