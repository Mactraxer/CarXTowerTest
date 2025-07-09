using UnityEngine;

public partial class GameInstaller : MonoBehaviour
{
    private IServiceLocator _serviceLocator;
    private LevelContoller _levelContoller;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        _serviceLocator = new ServiceLocator();

        RegisterServices();
        RegisterFactories();
        LoadLevel();
    }

    private void LoadLevel()
    {
        _serviceLocator.Resolve<ISceneLoader>().LoadScene(AssetConstants.GameSceneName, InitLevel);
    }

    private void RegisterServices()
    {
        // Сервисы
        var assetLoader = new AssetLoadService();
        var damageSystem = new DamageSystem();
        var sceneLoader = new SceneLoader();
        var timerService = new TimerService();

        _serviceLocator.Register<ISceneLoader>(sceneLoader);
        _serviceLocator.Register<IAssetLoadService>(assetLoader);
        _serviceLocator.Register<IDamageSystem>(damageSystem);

        var updateServicePrefab = assetLoader.Load<MonoBehaviourSimulationUpdateService>(AssetConstants.UpdateServicePath);
        var updateService = Instantiate(updateServicePrefab);
        updateService.Register(timerService);
        _serviceLocator.Register<ISimulationUpdateService>(updateService);
        _serviceLocator.Register<ITimerService>(timerService);
    }

    private void RegisterFactories()
    {
        var assetLoader = _serviceLocator.Resolve<IAssetLoadService>();
        var damageSystem = _serviceLocator.Resolve<IDamageSystem>();
        var updateService = _serviceLocator.Resolve<ISimulationUpdateService>();
        var timerService = _serviceLocator.Resolve<ITimerService>();

        // Загружаем конфиги
        var towerConfigs = assetLoader.Load<TowerConfigsSO>(AssetConstants.TowerConfigsPath);
        var projectileConfigs = assetLoader.Load<ProjectileConfigsSO>(AssetConstants.ProjectileConfigsPath);
        var enemyConfig = assetLoader.Load<EnemyConfigSO>(AssetConstants.EnemyConfigPath);
        var levelConfig = assetLoader.Load<LevelConfigSO>(AssetConstants.LevelConfigPath);

        // Фабрики
        var towerFactory = new TowerFactory(towerConfigs.configs, assetLoader, updateService);
        _serviceLocator.Register<ITowerFactory>(towerFactory);

        var projectileFactory = new ProjectileFactory(projectileConfigs.configs, assetLoader, updateService, damageSystem);
        _serviceLocator.Register<IProjectileFactory>(projectileFactory);

        var enemyFactory = new EnemyFactory(enemyConfig, assetLoader, updateService, levelConfig.enemySpawnPoint, levelConfig.enemyDestinationPoint);
        _serviceLocator.Register<IEnemyFactory>(enemyFactory);

        var levelFactory = new LevelFactory(assetLoader, enemyFactory, levelConfig, towerFactory, timerService);
        _serviceLocator.Register<ILevelFactory>(levelFactory);
    }

    private void InitLevel()
    {
        var levelFactory = _serviceLocator.Resolve<ILevelFactory>();
        _levelContoller = levelFactory.CreateLevel();
        _levelContoller.Start();
    }
}
