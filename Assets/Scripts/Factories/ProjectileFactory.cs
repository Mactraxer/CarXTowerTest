using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectileFactory : IProjectileFactory
{
    private readonly ProjectileConfigSO[] _configs;
    private readonly IAssetLoadService _assetLoader;
    private readonly ISimulationUpdateService _updateService;
    private readonly Dictionary<ProjectileType, Queue<ProjectilePresenter>> _pools;

    public ProjectileFactory(ProjectileConfigSO[] configs, IAssetLoadService loader, ISimulationUpdateService updater)
    {
        _configs = configs;
        _assetLoader = loader;
        _updateService = updater;
        _pools = new Dictionary<ProjectileType, Queue<ProjectilePresenter>>();
    }

    public ProjectilePresenter Create(ProjectileType projectileType, Vector3 startPosition, ITarget target)
    {
        var config = _configs.FirstOrDefault(config => config.type == projectileType);
        if (_configs.Length == 0 || config == null)
        {
            throw new System.Exception($"No config for this type={projectileType} of projectile");
        }

        if (_pools.ContainsKey(projectileType) && _pools[projectileType].Count > 0)
        {
            var presenter = _pools[projectileType].Dequeue();
            presenter.OnLifeTimeEnded += OnEndedPresenterLifeTimeHandler;
            presenter.Init(startPosition);
            return presenter;
        }
        else
        {
            ProjectilePresenter presenter = GetPresenterByType(config, startPosition, target);
            presenter.OnLifeTimeEnded += OnEndedPresenterLifeTimeHandler;
            _updateService.Register(presenter);

            return presenter;
        }
    }

    public void Dispose(ProjectilePresenter projectile)
    {
        projectile.Dispose();
        projectile.OnLifeTimeEnded -= OnEndedPresenterLifeTimeHandler;
        if (_pools.ContainsKey(projectile.Model.ProjectileType))
        {
            _pools[projectile.Model.ProjectileType].Enqueue(projectile);
        }
        else
        {
            _pools.Add(projectile.Model.ProjectileType, new Queue<ProjectilePresenter>());
        }
    }

    public CannonProjectilePresenter CreateCannon(ProjectileType projectileType, Vector3 shootPointPosition, ITarget target)
    {
        return Create(projectileType, shootPointPosition, target) as CannonProjectilePresenter;
    }

    public GuidedProjectilePresenter CreateGuided(ProjectileType projectileType, Vector3 shootPointPosition, ITarget target)
    {
        return Create(projectileType, shootPointPosition, target) as GuidedProjectilePresenter;
    }

    private void OnEndedPresenterLifeTimeHandler(ProjectilePresenter presenter)
    {
        Dispose(presenter);
    }

    private ProjectilePresenter GetPresenterByType(ProjectileConfigSO config, Vector3 startPosition, ITarget target)
    {
        switch (config.type)
        {
            case ProjectileType.Cannon:
                var cannonView = LoadAndInstantiateView<CannonProjectileView>(startPosition, config.prefab.Path);
                IProjectileMover cannonMover = config.trajectoryMode switch
                {
                    TrajectoryMode.Parabolic => new ParabolicTrajectoryProjectileMover(),
                    TrajectoryMode.Straight => new StraightTrajectoryProjectileMover(),
                    _ => throw new System.Exception($"Unsupported trajectory mode: {config.trajectoryMode}")
                };
                
                var cannonModel = new CannonProjectileModel(cannonMover, config.type, startPosition, config.speed, config.damage, target, config.hitDistance);
                return new CannonProjectilePresenter(cannonModel, cannonView, config.lifeTime);
            case ProjectileType.Guided:
                var guidedView = LoadAndInstantiateView<GuidedProjectileView>(startPosition, config.prefab.Path);
                var guidedMover = new GuidedProjectileMover();
                var guidedModel = new GuidedProjectileModel(guidedMover, config.type, startPosition, config.speed, config.damage, target, config.hitDistance);
                return new GuidedProjectilePresenter(guidedModel, guidedView, config.lifeTime);
            default:
                throw new System.Exception($"No presenter for this type={config.type} of projectile");
        }
    }
    
    private TTowerView LoadAndInstantiateView<TTowerView>(Vector3 position, string path) where TTowerView : ProjectileView
    {
        var prefab = _assetLoader.Load<TTowerView>(path);
        var view = Object.Instantiate(prefab, position, Quaternion.identity);
        return view;
    }
}