using UnityEngine;

public interface IProjectileFactory : IService
{
    CannonProjectilePresenter CreateCannon(ProjectileType projectileType, TrajectoryMode trajectoryMode, Vector3 shootPointPosition, ITarget target);
    GuidedProjectilePresenter CreateGuided(ProjectileType projectileType, Vector3 shootPointPosition, ITarget target);
    void Dispose(ProjectilePresenter projectile);
}