using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScEnemySpawnerManager : MonoBehaviour
{
    [SerializeField] List<ScListeVagues> _spawners; 
    ScBattleZone _zone;
    private int _nb = 0;
    private int _index = 0;
    void Start() 
    {
        _zone = GetComponent<ScBattleZone>();
    }
    public void SpawnEnemies()
    {
        Debug.Log($"{_index + 1} / {_spawners.Count}");

        _nb = 0;
        foreach (ScEnemySpawner spawner in _spawners[_index].Spawner)
        {
            if (spawner.Enemy.TryGetComponent(out ScEnemyStats stats))
            {
                GameObject enemy = Instantiate(spawner.Enemy, spawner.SpawnPosition.position, Quaternion.identity);
                enemy.transform.parent = transform;
                _nb++;
            }
            else
            {
                Debug.Log("One of the gameObject in ScEnemySpawner doesn't have a valid enemy");
            }
        }
    }

    public void RemoveFromEnemyCount()
    {

        ScEnemyStats[] stats = GetComponentsInChildren<ScEnemyStats>();

        _nb--;
        Debug.Log($"nb = {_nb}");

        if (stats.Length == 0) 
        {
            _index++;
        }

        if (stats.Length == 0 &&  _index < _spawners.Count)
        {
            StartCoroutine(Spawn());
        }
        else if (_nb <= 0) 
        {
            Debug.Log("End Of The Fight !");
            _zone.EndOfTheFight();
        }
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1);
        SpawnEnemies();
    }
}
