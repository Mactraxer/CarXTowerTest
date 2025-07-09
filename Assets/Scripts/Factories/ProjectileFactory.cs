using System.Linq;
using UnityEngine;

public class ProjectileFactory : IProjectileFactory
{
    private readonly IDamageSystem _damageSystem;
    private ProjectileConfigSO[] _configs;
    private IAssetLoadService _assetLoader;
    private ISimulationUpdateService _updateService;

    public ProjectileFactory(ProjectileConfigSO[] configs, IAssetLoadService loader, ISimulationUpdateService updater, IDamageSystem damageSystem)
    {
        _configs = configs;
        _assetLoader = loader;
        _updateService = updater;
        _damageSystem = damageSystem;
    }

    public ProjectilePresenter Create(ProjectileType projectileType, Vector3 startPosition, ITarget target)
    {
        var config = _configs.FirstOrDefault(config => config.type == projectileType);
        if (_configs.Length == 0 || config == null)
        {
            throw new System.Exception($"No config for this type={projectileType} of projectile");
        }

        ProjectilePresenter presenter = GetPresenterByType(config, startPosition, target);

        _updateService.Register(presenter);

        return presenter;
    }

    private ProjectilePresenter GetPresenterByType(ProjectileConfigSO config, Vector3 startPosition, ITarget target)
    {
        switch (config.type)
        {
            case ProjectileType.Cannon:
                var cannonView = LoadAndInstantiateView<CannonProjectileView>(startPosition, config.prefab.Path);
                var cannonModel = new CannonProjectileModel(startPosition, config.speed, config.damage, target);
                return new CannonProjectilePresenter(cannonView, cannonModel);
            case ProjectileType.Guided:
                var guidedView = LoadAndInstantiateView<GuidedProjectileView>(startPosition, config.prefab.Path);
                var guidedModel = new GuidedProjectileModel(startPosition, config.speed, config.damage, target);
                return new GuidedProjectilePresenter(guidedView, guidedModel);
            default:
                throw new System.Exception($"No presenter for this type={config.type} of projectile");
        }
    }

    public void Dispose(ProjectileModel projectile)
    {
        throw new System.NotImplementedException();
    }

    private void OnProjectileHit(ProjectileModel model)
    {
        _damageSystem.ApplyDamage(model.Target, model.Damage);
    }

    private TTowerView LoadAndInstantiateView<TTowerView>(Vector3 position, string path) where TTowerView : ProjectileView
    {
        var prefab = _assetLoader.Load<TTowerView>(path);
        var view = Object.Instantiate(prefab, position, Quaternion.identity);
        return view;
    }
}