using UnityEngine;

public class CannonTowerModel : TowerModel
{
    public Vector3 AimOffset { get; private set; }
    public Vector3 GunPosition { get; private set; }
    public Vector3 FireOffset { get; private set; }

    private TrajectoryMode _trajectoryMode;

    public CannonTowerModel(
        TrajectoryMode trajectoryMode,
        Vector3 gunPosition,
        Vector3 shootPointPosition,
        float cooldown,
        float projectileSpeed,
        DetectedArea detectedArea,
        ITowerTargetingSystem targetingSystem,
        IDamageSystem damageSystem,
        ProjectileType projectileType,
        IProjectileFactory projectileFactory
        ) : base(shootPointPosition, cooldown, projectileSpeed, detectedArea, targetingSystem, damageSystem, projectileType, projectileFactory)
    {
        _trajectoryMode = trajectoryMode;
        GunPosition = gunPosition;
    }

    protected override void SetupStateMachine()
    {
        base.SetupStateMachine();

        switch (_trajectoryMode)
        {
            case TrajectoryMode.Straight:
                StateMachine.AddState(new AimingStraightTrajectoryState(this));
                StateMachine.AddState(new FiringStraightTrajectoryState(this));
                break;
            case TrajectoryMode.Parabolic:
                StateMachine.AddState(new AimingParabolicTrajectoryState(this));
                StateMachine.AddState(new FiringParabolicTrajectoryState(this));
                break;
            default:
                throw new System.ArgumentOutOfRangeException(nameof(_trajectoryMode), _trajectoryMode, null);
        }
    }

    public override void Shoot()
    {
        var projectile = _projectileFactory.CreateCannon(ProjectileType, _trajectoryMode, ShootPointPosition, Target);
        projectile.MoveToTargetWithOffset(Target, FireOffset, OnHitCallback);
    }

    public void SetAimOffset(Vector3 aimOffset)
    {
        AimOffset = aimOffset;
    }

    public void SetFireVelocity(Vector3 fireOffset)
    {
        FireOffset = fireOffset;
    }

    public override void DetectedTaget()
    {
        if (_trajectoryMode == TrajectoryMode.Parabolic)
        {
            StateMachine.ChangeState<AimingParabolicTrajectoryState>();
        }
        else
        {
            StateMachine.ChangeState<AimingStraightTrajectoryState>();
        }
    }

    protected override void OnHitCallback(ProjectilePresenter projectile)
    {
        DamageSystem.ApplyDamage(projectile.Model.Target, projectile.Model);
        _projectileFactory.Dispose(projectile);
    }
}
