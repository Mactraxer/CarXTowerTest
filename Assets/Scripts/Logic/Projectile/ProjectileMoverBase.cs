using System;
using UnityEngine;

public abstract class ProjectileMoverBase : IProjectileMover
{
    protected Vector3 _startPosition;
    protected Vector3 _moveVelocity;
    protected ProjectileModel _projectileModel;
    protected float _hitDistance;
    protected Action _onHitCallback;
    protected ITarget _target;
    protected float _speed;

    public virtual void Init(ITarget target, Vector3 startVelocity, ProjectileModel projectileModel, float hitDistance, float speed, Action onHitCallback)
    {
        _startPosition = projectileModel.Position;
        _moveVelocity = startVelocity;
        _projectileModel = projectileModel;
        _hitDistance = hitDistance;
        _onHitCallback = onHitCallback;
        _target = target;
        _speed = speed;
    }

    public abstract void MoveTo();

    protected void TryInvokeHitEvent()
    {
        if (_target == null) return;

        if (Vector3.Distance(_projectileModel.Position, _target.CurrentPosition) < _hitDistance)
        {
            _onHitCallback?.Invoke();
        }
    }
}
