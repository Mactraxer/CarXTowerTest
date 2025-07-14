using System;
using UnityEngine;

public abstract class TowerModel
{
    public event Action OnStopAimTarget;
    public event Action<Transform> OnShoot;
    public event Action<ITarget> OnAimAtTarget;
    public DetectedArea DetectedArea { get; private set; }
    public IDamageSystem DamageSystem { get; private set; }
    public ITowerTargetingSystem TargetingSystem { get; private set; }
    public TowerStateMachine StateMachine { get; private set; }
    public float FireCooldown { get; }
    public ITarget Target { get; private set; }
    public ProjectileType ProjectileType { get; private set; }
    public float ProjectileSpeed { get; private set; }
    public Vector3 Position { get; private set; }
    public float AimDuration { get; private set; }
    public Vector3 ShootPointPosition { get; private set; }

    protected readonly IProjectileFactory _projectileFactory;

    public TowerModel(
        Vector3 shootPointPosition,
        float cooldown,
        float projectileSpeed,
        DetectedArea detectedArea,
        ITowerTargetingSystem targetingSystem,
        IDamageSystem damageSystem,
        ProjectileType projectileType,
        IProjectileFactory projectileFactory
        )
    {
        ShootPointPosition = shootPointPosition;
        FireCooldown = cooldown;
        ProjectileSpeed = projectileSpeed;
        DetectedArea = detectedArea;
        AimDuration = 2f;
        ProjectileType = projectileType;
        TargetingSystem = targetingSystem;
        DamageSystem = damageSystem;
        _projectileFactory = projectileFactory;
    }

    protected virtual void SetupStateMachine()
    {
        StateMachine = new TowerStateMachine();
        StateMachine.AddState(new IdleState(this));
        StateMachine.AddState(new SearchTargetState(this));
        StateMachine.AddState(new CooldownState(this));
    }

    public void Init()
    {
        SetupStateMachine();
    }

    public void Tick()
    {
        StateMachine.Tick();
    }

    public void SetTarget(ITarget newTarget) => Target = newTarget;

    public abstract void DetectedTaget();
    public abstract void Shoot();
    public void Aim() => OnAimAtTarget?.Invoke(Target);

    public void OnStopAim() => OnStopAimTarget?.Invoke();

    public void Start()
    {
        StateMachine.ChangeState<IdleState>();
    }

    protected abstract void OnHitCallback(ProjectilePresenter presenter);
}
