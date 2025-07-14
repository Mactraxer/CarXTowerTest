using UnityEngine;

public partial class GameInstaller : MonoBehaviour
{
    private IServiceLocator _serviceLocator;
    private LevelPresenter _levelContoller;

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
        var towerTargetingSystem = new TowerTargetingSystem();

        _serviceLocator.Register<ISceneLoader>(sceneLoader);
        _serviceLocator.Register<IAssetLoadService>(assetLoader);
        _serviceLocator.Register<IDamageSystem>(damageSystem);
        _serviceLocator.Register<ITowerTargetingSystem>(towerTargetingSystem);

        var updateServicePrefab = assetLoader.Load<MonoBehaviourSimulationUpdateService>(AssetConstants.Services.UpdateServicePath);
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
        var towerTargetingSystem = _serviceLocator.Resolve<ITowerTargetingSystem>();

        // Загружаем конфиги
        var towerConfigs = assetLoader.Load<TowerConfigsSO>(AssetConstants.TowerConfigsPath);
        var projectileConfigs = assetLoader.Load<ProjectileConfigsSO>(AssetConstants.ProjectileConfigsPath);
        var enemyConfig = assetLoader.Load<EnemyConfigSO>(AssetConstants.EnemyConfigPath);
        var levelConfig = assetLoader.Load<LevelConfigSO>(AssetConstants.LevelConfigPath);

        // Фабрики
        var projectileFactory = new ProjectileFactory(projectileConfigs.configs, assetLoader, updateService);
        _serviceLocator.Register<IProjectileFactory>(projectileFactory);

        var towerFactory = new TowerFactory(towerConfigs.configs, assetLoader, updateService, projectileFactory, towerTargetingSystem, damageSystem);
        _serviceLocator.Register<ITowerFactory>(towerFactory);

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
