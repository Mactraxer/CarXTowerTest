using System;
using System.Linq;
using UnityEngine;

public class TowerFactory : ITowerFactory
{
    private readonly TowerConfigSO[] _configs;
    private readonly ISimulationUpdateService _updateService;
    private readonly IAssetLoadService _assetLoader;
    private readonly IProjectileFactory _projectileFactory;
    private readonly ITowerTargetingSystem _targetingSystem;
    private readonly IDamageSystem _damageSystem;

    public TowerFactory(TowerConfigSO[] configs, IAssetLoadService loader, ISimulationUpdateService update, IProjectileFactory projectileFactory, ITowerTargetingSystem targetingSystem, IDamageSystem damageSystem)
    {
        _configs = configs;
        _assetLoader = loader;
        _updateService = update;
        _projectileFactory = projectileFactory;
        _targetingSystem = targetingSystem;
        _damageSystem = damageSystem;
    }

    public TowerPresenter CreateTower(Vector3 position, TowerType type, TrajectoryMode trajectoryMode)
    {
        var config = _configs.FirstOrDefault(config => config.type == type && config.trajectoryMode == trajectoryMode);
        if (_configs.Length == 0 || config == null)
        {
            throw new Exception($"No config for this type={type} of tower");
        }

        TowerPresenter presenter = GetPresenterByType(config, position);

        _updateService.Register(presenter);

        return presenter;
    }

    public void Dispose(TowerPresenter tower)
    {
        tower.Dispose();
    }

    private TowerPresenter GetPresenterByType(TowerConfigSO config, Vector3 position)
    {
        switch (config.type)
        {
            case TowerType.Cannon:
                CannonTowerView cannonView = LoadAndInstantiateView<CannonTowerView>(position, config.towerPrefab.Path);
                var detectedAreaPrefab = _assetLoader.Load<DetectedArea>(AssetConstants.Services.DetectedAreaPath);
                var detectedAreaCannon = UnityEngine.Object.Instantiate(detectedAreaPrefab);
                detectedAreaCannon.SetRange(config.range);
                detectedAreaCannon.transform.SetParent(cannonView.transform);
                detectedAreaCannon.transform.localPosition = Vector3.zero;
                
                CannonTowerModel cannonModel = new CannonTowerModel(
                    config.trajectoryMode,
                    cannonView.GunPivotPosition,
                    cannonView.ShootPointPosition,
                    config.fireCooldonw,
                    config.projectileConfig.speed,
                    detectedAreaCannon,
                    _targetingSystem,
                    _damageSystem,
                    config.projectileConfig.type,
                    _projectileFactory);
                cannonModel.Init();
                TowerPresenter canonnPresenter = new CannonTowerPresenter(cannonModel, cannonView);
                return canonnPresenter;

            case TowerType.Magic:
                MagicTowerView magicView = LoadAndInstantiateView<MagicTowerView>(position, config.towerPrefab.Path);
                var detectedAreaMagicPrefab = _assetLoader.Load<DetectedArea>(AssetConstants.Services.DetectedAreaPath);
                var detectedAreaMagic = UnityEngine.Object.Instantiate(detectedAreaMagicPrefab);
                detectedAreaMagic.SetRange(config.range);
                detectedAreaMagic.transform.SetParent(magicView.transform);
                detectedAreaMagic.transform.localPosition = Vector3.zero;

                MagicTowerModel magicModel = new MagicTowerModel(
                    magicView.ShootPointPosition,
                    config.fireCooldonw,
                    config.projectileConfig.speed,
                    detectedAreaMagic,
                    _targetingSystem,
                    _damageSystem,
                    config.projectileConfig.type,
                    _projectileFactory);
                magicModel.Init();
                MagicTowerPresenter magicPresenter = new MagicTowerPresenter(magicModel, magicView);
                return magicPresenter;
            default:
                throw new ArgumentOutOfRangeException(nameof(config.type), config.type, null);
        }
    }

    private TTowerView LoadAndInstantiateView<TTowerView>(Vector3 position, string path) where TTowerView : TowerView
    {
        var prefab = _assetLoader.Load<TTowerView>(path);
        var view = UnityEngine.Object.Instantiate(prefab, position, Quaternion.identity);
        return view;
    }
}
