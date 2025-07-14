using System;
using UnityEngine;

public class GuidedProjectileModel : ProjectileModel
{
    public GuidedProjectileModel(IProjectileMover projectileMover, ProjectileType type, Vector3 startPosition, float speed, float damage, ITarget target, float hitDistance)
     : base(projectileMover, type, startPosition, speed, damage, target, hitDistance)
    {
    }

    public void Init(ITarget target, Action<ProjectileModel> onHitCallbackHandler)
    {
        Init();
        _onHit = onHitCallbackHandler;
        Target = target;
        Target.OnDisposed += OnTargetDisposed;
        _projectileMover.Init(target, Vector3.zero, this, _hitDistance, Speed, () => _onHit?.Invoke(this));
    }

    public override void Tick()
    {
        _projectileMover.MoveTo();
    }
}
