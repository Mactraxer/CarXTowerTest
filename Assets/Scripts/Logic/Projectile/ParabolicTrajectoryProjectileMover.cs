using System;
using UnityEngine;

public class ParabolicTrajectoryProjectileMover : ProjectileMoverBase
{
    private float _elapsedTime;

    public override void Init(ITarget target, Vector3 startVelocity, ProjectileModel projectileModel, float hitDistance, float speed, Action onHitCallback)
    {
        base.Init(target, startVelocity, projectileModel, hitDistance, speed, onHitCallback);
        _elapsedTime = 0;
    }

    public override void MoveTo()
    {
        _elapsedTime += Time.deltaTime;

        Vector3 displacement = _moveVelocity * _elapsedTime + 0.5f * Physics.gravity * _elapsedTime * _elapsedTime;
        var newPosition = _startPosition + displacement;
        _projectileModel.SetPosition(newPosition);
        TryInvokeHitEvent();
    }
}