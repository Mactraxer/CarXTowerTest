using System;
using System.Linq;
using UnityEngine;

public class TowerFactory : ITowerFactory
{
    private readonly TowerConfigSO[] _configs;
    private readonly ISimulationUpdateService _updateService;
    private readonly IAssetLoadService _assetLoader;

    public TowerFactory(TowerConfigSO[] configs, IAssetLoadService loader, ISimulationUpdateService update)
    {
        _configs = configs;
        _assetLoader = loader;
        _updateService = update;
    }

    public TowerPresenter CreateTower(Vector3 position, TowerType type)
    {
        var config = _configs.FirstOrDefault(config => config.type == type);
        if (_configs.Length == 0 || config == null)
        {
            throw new Exception($"No config for this type={type} of tower");
        }

        TowerPresenter presenter = GetPresenterByType(config, position);

        _updateService.Register(presenter);

        return presenter;
    }

    private TowerPresenter GetPresenterByType(TowerConfigSO config, Vector3 position)
    {
        switch (config.type)
        {
            case TowerType.Cannon:
                CannonTowerView cannonView = LoadAndInstantiateView<CannonTowerView>(position, config.towerPrefab.Path);
                CannonTowerModel cannonModel = new CannonTowerModel(config.fireRate, config.range);
                TowerPresenter canonnPresenter = new CannonTowerPresenter(cannonModel, cannonView);
                return canonnPresenter;

            case TowerType.Magic:
                MagicTowerView magicView = LoadAndInstantiateView<MagicTowerView>(position, config.towerPrefab.Path);
                MagicTowerModel magicModel = new MagicTowerModel(config.fireRate, config.range);
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
