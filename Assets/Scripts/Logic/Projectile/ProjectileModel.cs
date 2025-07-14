using System;
using UnityEngine;

public abstract class ProjectileModel : IDisposable
{
    public ProjectileType ProjectileType { get; private set; }
    public Vector3 Position { get; protected set; }
    public float Speed { get; }
    public float Damage { get; }
    public ITarget Target { get; protected set; }
    public bool IsMoveToLastTarget { get; private set; }
    public bool IsActive { get; protected set; }
    protected readonly float _hitDistance;
    protected Action<ProjectileModel> _onHit;
    protected Vector3 _lastTargetDirection;
    protected IProjectileMover _projectileMover;

    private Vector3 _lastTargetPosition;

    public ProjectileModel(IProjectileMover projectileMover, ProjectileType type, Vector3 startPosition, float speed, float damage, ITarget target, float hitDistance)
    {
        _projectileMover = projectileMover;
        ProjectileType = type;
        Position = startPosition;
        Speed = speed;
        Damage = damage;
        Target = target;
        _hitDistance = hitDistance;
    }
    
    protected void Init()
    {
        IsActive = true;
    }

    public abstract void Tick();
    
    public virtual void Dispose()
    {
        _onHit = null;
        Target = null;
        IsMoveToLastTarget = false;
        IsActive = false;
    }

    public void SetPosition(Vector3 startPosition)
    {
        Position = startPosition;
    }

    protected void OnTargetDisposed()
    {
        if (Target != null)
        {
            Target.OnDisposed -= OnTargetDisposed;
            SetupMoveToLastTargetPosition();
            Target = null;
            _onHit = null;
        }
    }

    protected void MoveToLastTarget()
    {
        Position += Speed * Time.deltaTime * _lastTargetDirection;
    }

    private void SetupMoveToLastTargetPosition()
    {
        IsMoveToLastTarget = true;
        _lastTargetPosition = Target.CurrentPosition;
        _lastTargetDirection = (_lastTargetPosition - Position).normalized;
    }
}
