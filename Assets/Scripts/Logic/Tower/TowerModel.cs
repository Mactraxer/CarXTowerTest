using System;
using UnityEngine;

public abstract class TowerModel
{
    public event Action<Transform> OnShoot;
    public event Action<ITarget> OnAimAtTarget;

    public DetectedArea DetectedArea { get; private set; }
    public TowerTargetingSystem TargetingSystem { get; private set; }
    public TowerStateMachine StateMachine { get; private set; }
    public float FireCooldown { get; }
    public ITarget Target { get; private set; }
    public float RotateDuration { get; private set; }
    public float ProjectileSpeed { get; private set; }
    public Vector3 Position { get; private set; }
    public Vector3 LeadOffset { get; private set; }

    public TowerModel(float cooldown, DetectedArea detectedArea)
    {
        FireCooldown = cooldown;
        DetectedArea = detectedArea;
        RotateDuration = 1f;
        SetupStateMachine();
    }

    protected virtual void SetupStateMachine()
    {
        StateMachine = new TowerStateMachine();
        StateMachine.AddState(new IdleState(this, StateMachine));
        StateMachine.AddState(new SearchTargetState(this, StateMachine));
        StateMachine.AddState(new AimingState(this, StateMachine));
        StateMachine.AddState(new CooldownState(this, StateMachine));
    }

    public void Tick()
    {
        StateMachine.Tick();
    }

    public void SetTarget(ITarget newTarget) => Target = newTarget;

    public abstract void Shoot();

    public void Aim() => OnAimAtTarget?.Invoke(Target);

    public void SetLeadOffset(Vector3 leadOffset)
    {
        LeadOffset = leadOffset;
    }
}
