using System;
using UnityEngine;

public class ProjectileModel
{
    public Vector3 Position { get; private set; }
    public float Speed { get; }
    public float Damage { get; }
    public ITarget Target { get; }

    private Action<ProjectileModel> _onHit;

    public ProjectileModel(Vector3 startPosition, float speed, float damage, ITarget target)
    {
        Position = startPosition;
        Speed = speed;
        Damage = damage;
        Target = target;
    }

    public void Init(Action<ProjectileModel> onHit)
    {
        _onHit = onHit;
    }

    public void Tick()
    {
        if (Target == null || !Target.IsAlive) return;

        Vector3 dir = (Target.CurrentPosition - Position).normalized;
        Position += dir * Speed * Time.deltaTime;

        if (Vector3.Distance(Position, Target.CurrentPosition) < 0.2f)
        {
            _onHit?.Invoke(this);
        }
    }
}
