using System;
using System.Collections.Generic;
using UnityEngine;

public class DetectedArea : MonoBehaviour
{
    private readonly List<IEnemy> _enemiesInArea = new();

    public event Action<IEnemy> OnEnemyEntered;
    public event Action<IEnemy> OnEnemyExited;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyProxy enemyProxy))
        {
            var enemy = enemyProxy.Enemy;
            if (!_enemiesInArea.Contains(enemy))
            {
                _enemiesInArea.Add(enemy);
                enemy.OnDeath += HandleEnemyDeath;
                OnEnemyEntered?.Invoke(enemy);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out EnemyProxy enemyProxy))
        {
            RemoveEnemy(enemyProxy.Enemy);
        }
    }

    private void HandleEnemyDeath(IEnemy enemy)
    {
        RemoveEnemy(enemy);
    }

    private void RemoveEnemy(IEnemy enemy)
    {
        if (_enemiesInArea.Remove(enemy))
        {
            enemy.OnDeath -= HandleEnemyDeath;
            OnEnemyExited?.Invoke(enemy);
        }
    }

    public IEnemy GetFirstEnemy()
    {
        return _enemiesInArea.Count > 0 ? _enemiesInArea[0] : null;
    }

    public IReadOnlyList<IEnemy> GetAllEnemies() => _enemiesInArea;
}