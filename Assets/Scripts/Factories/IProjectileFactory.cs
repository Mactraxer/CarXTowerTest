using UnityEngine;

public interface IProjectileFactory : IService
{
    ProjectilePresenter Create(ProjectileType projectileType, Vector3 startPosition, ITarget target);
    CannonProjectilePresenter CreateCannon(ProjectileType projectileType, Vector3 shootPointPosition, ITarget target);
    GuidedProjectilePresenter CreateGuided(ProjectileType projectileType, Vector3 shootPointPosition, ITarget target);
    void Dispose(ProjectilePresenter projectile);
}