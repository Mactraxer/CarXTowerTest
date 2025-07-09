using UnityEngine;

public interface IProjectileFactory : IService
{
    public ProjectilePresenter Create(ProjectileType projectileType, Vector3 startPosition, ITarget target);
    public void Dispose(ProjectileModel projectile);
}