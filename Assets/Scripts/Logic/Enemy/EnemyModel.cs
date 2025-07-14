using System;
using UnityEngine;

public class EnemyModel : ITarget, ITickable, IEnemy, IDisposable
{
    public event Action OnTakeDamage;
    public Action OnDisposed { get; set; }
    public Vector3 StartPosition { get; }
    public Vector3 CurrentPosition { get; private set; }
    public Vector3 TargetPosition { get; }
    public float Speed { get; private set; }
    public float Health { get; private set; }
    public float MaxHealht { get; }
    public bool IsAlive => Health > 0;
    public bool IsMoving { get; private set; }
    public Vector3 Position => CurrentPosition;
    public Vector3 Velocity => (TargetPosition - StartPosition).normalized * Speed;
    public event Action<IEnemy> OnDeath;
    public event Action<EnemyModel> OnReachedDestination;
    private Vector3 _direction;
    private readonly float _arrivedDistance;

    public EnemyModel(Vector3 startPos, Vector3 targetPos, float speed, float health, float arrivedDistance)
    {
        StartPosition = startPos;
        CurrentPosition = startPos;
        TargetPosition = targetPos;
        Speed = speed;
        Health = health;
        MaxHealht = health;
        _direction = (TargetPosition - CurrentPosition).normalized;
        _arrivedDistance = arrivedDistance;
    }

    public void Tick()
    {
        if (!IsAlive || !IsMoving) return;

        CurrentPosition += _direction * Speed * Time.deltaTime;

        if (Vector3.Distance(CurrentPosition, TargetPosition) < _arrivedDistance)
        {
            OnReachedDestination?.Invoke(this);
        }
    }

    public void TakeDamage(float amount)
    {
        if (!IsAlive) return;

        Health -= amount;
        OnTakeDamage?.Invoke();
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

    public void Init()
    {
        Health = MaxHealht;
    }

    public void Dispose()
    {
        OnDisposed?.Invoke();
    }
}