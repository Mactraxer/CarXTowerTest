using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : IEnemyFactory
{
    private readonly EnemyConfigSO _config;
    private readonly IAssetLoadService _assetLoader;
    private readonly ISimulationUpdateService _updater;
    private readonly Vector3 _spawnPoint;
    private readonly Vector3 _destinationPoint;
    private List<EnemyPresenter> _enemiesPool;

    public EnemyFactory(EnemyConfigSO config, IAssetLoadService loader, ISimulationUpdateService updater, Vector3 spawnPoint, Vector3 destinationPoint)
    {
        _config = config;
        _assetLoader = loader;
        _updater = updater;
        _spawnPoint = spawnPoint;
        _destinationPoint = destinationPoint;

        _enemiesPool = new List<EnemyPresenter>();
    }

    public EnemyPresenter Create()
    {
        if (_enemiesPool.Count > 0)
        {
            var enemy = _enemiesPool[_enemiesPool.Count - 1];
            _enemiesPool.Remove(enemy);
            enemy.Init();
            _updater.Register(enemy);
            return enemy;
        }
        else
        {
            var prefab = _assetLoader.Load<EnemyView>(_config.enemyPrefab.Path);
            var enemyView = Object.Instantiate(prefab, _spawnPoint, Quaternion.identity);

            var model = new EnemyModel(_spawnPoint, _destinationPoint, _config.speed, _config.health);
            var presenter = new EnemyPresenter(model, enemyView);
            _updater.Register(presenter);

            return presenter;   
        }
    }

    public void Dispose(EnemyPresenter presenter)
    {
        presenter.Dispose();
        _updater.Unregister(presenter);
        _enemiesPool.Add(presenter);
    }
}