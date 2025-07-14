using UnityEngine;

public class LevelFactory : ILevelFactory
{
    private readonly LevelConfigSO _levelConfig;
    private readonly IAssetLoadService _assetLoader;
    private readonly IEnemyFactory _enemyFactory;
    private readonly ITowerFactory _towerFactory;
    private readonly ITimerService _timerService;

    public LevelFactory(IAssetLoadService assetLoader, IEnemyFactory enemyFactory, LevelConfigSO levelConfig, ITowerFactory towerFactory, ITimerService timerService)
    {
        _assetLoader = assetLoader;
        _enemyFactory = enemyFactory;
        _levelConfig = levelConfig;
        _towerFactory = towerFactory;
        _timerService = timerService;
    }

    public LevelPresenter CreateLevel()
    {
        var levelViewPrefab = _assetLoader.Load<LevelView>(AssetConstants.LevelViewPrefabPath);
        var levelView = Object.Instantiate(levelViewPrefab);
        var levelModel = new LevelModel(_levelConfig.towerSlots, _levelConfig.spawnEnemyDelay);
        var levelContoller = new LevelPresenter(levelModel, levelView, _towerFactory, _timerService, _enemyFactory);
        return levelContoller;
    }
}
