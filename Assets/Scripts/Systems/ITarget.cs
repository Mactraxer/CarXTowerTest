using UnityEngine;

public interface ITarget
{
    Vector3 CurrentPosition { get; }
    bool IsAlive { get; }
    Vector3 Velocity { get; }

    void TakeDamage(float amount);
}
