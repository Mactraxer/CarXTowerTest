using System;
using UnityEngine;

public class EnemyModel : ITarget, ITickable, IEnemy
{
    public Vector3 StartPosition { get; }
    public Vector3 CurrentPosition { get; private set; }
    public Vector3 TargetPosition { get; }
    public float Speed { get; private set; }
    public float Health { get; private set; }

    public bool IsAlive => Health > 0;

    public bool IsMoving { get; private set; }

    public Vector3 Position => CurrentPosition;

    public Vector3 Velocity => (TargetPosition - StartPosition).normalized * Speed;

    public event Action<IEnemy> OnDeath;
    public event Action<EnemyModel> OnReachedDestination;

    public EnemyModel(Vector3 startPos, Vector3 targetPos, float speed, float health)
    {
        StartPosition = startPos;
        CurrentPosition = startPos;
        TargetPosition = targetPos;
        Speed = speed;
        Health = health;
    }

    public void Tick()
    {
        if (!IsAlive || !IsMoving) return;

        Vector3 dir = (TargetPosition - CurrentPosition).normalized;
        CurrentPosition += dir * Speed * Time.deltaTime;

        if (Vector3.Distance(CurrentPosition, TargetPosition) < 0.1f)
        {
            OnReachedDestination?.Invoke(this);
        }
    }

    public void TakeDamage(float amount)
    {
        if (!IsAlive) return;

        Health -= amount;
        if (Health <= 0)
        {
            Health = 0;
            OnDeath?.Invoke(this);
        }
    }

    public void StartMove()
    {
        CurrentPosition = StartPosition;
        IsMoving = true;
    }
}