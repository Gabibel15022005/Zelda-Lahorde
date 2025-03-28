using UnityEngine;

public class ScEnemySpawnerManager : MonoBehaviour
{
    [SerializeField] ScEnemySpawner[] _spawners; 
    ScBattleZone _zone;
    private int _nb = 0;
    void Start() 
    {
        _zone = GetComponent<ScBattleZone>();
    }
    public void SpawnEnemies()
    {
        _nb = 0;
        foreach (ScEnemySpawner spawner in _spawners)
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
        _nb--;

        if (_nb <= 0) 
        {
            _zone.EndOfTheFight();
        }
    }

}
