using System;
using UnityEngine;

public interface IProjectileMover
{
    void Init(ITarget target, Vector3 startVelocity, ProjectileModel projectileModel, float hitDistance, float speed, Action onHitCallback);
    abstract void MoveTo();
}
