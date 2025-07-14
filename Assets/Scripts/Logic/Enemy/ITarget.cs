using System;
using UnityEngine;

public interface ITarget
{
    Action OnDisposed { get; set; }
    Vector3 CurrentPosition { get; }
    bool IsAlive { get; }
    Vector3 Velocity { get; }
    void TakeDamage(float amount);
}
