using System;
using UnityEngine;

public class CannonProjectileModel : ProjectileModel
{
    public CannonProjectileModel(IProjectileMover projectileMover, ProjectileType projectileType, Vector3 startPosition, float speed, float damage, ITarget target, float hitDistance)
     : base(projectileMover, projectileType, startPosition, speed, damage, target, hitDistance)
    {
    }

    public void Init(ITarget target, Vector3 moveVelocity, Action<ProjectileModel> onHit)
    {
        Init();
        _onHit = onHit;
        Target = target;
        Target.OnDisposed += OnTargetDisposed;
        _projectileMover.Init(target, moveVelocity, this, _hitDistance, Speed, () => _onHit?.Invoke(this));
    }

    public override void Tick()
    {
        _projectileMover.MoveTo();
    }
}
