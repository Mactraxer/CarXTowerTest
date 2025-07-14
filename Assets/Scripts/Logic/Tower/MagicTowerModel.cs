using UnityEngine;

public class MagicTowerModel : TowerModel
{
    public MagicTowerModel(
        Vector3 shootPointPosition,
        float fireRate,
        float projectileSpeed,
        DetectedArea detectedArea,
        ITowerTargetingSystem towerTargeting,
        IDamageSystem damageSystem,
        ProjectileType projectileType,
        IProjectileFactory projectileFactory
        ) : base(shootPointPosition, fireRate, projectileSpeed, detectedArea, towerTargeting, damageSystem, projectileType, projectileFactory)
    {
    }

    override protected void SetupStateMachine()
    {
        base.SetupStateMachine();
        StateMachine.AddState(new AimingState(this));
        StateMachine.AddState(new FiringState(this));
    }

    public override void Shoot()
    {
        var projectile = _projectileFactory.CreateGuided(ProjectileType, ShootPointPosition, Target);
        projectile.MoveToTarget(Target, OnHitCallback);
    }

    public override void DetectedTaget()
    {
        StateMachine.ChangeState<AimingState>();
    }

    protected override void OnHitCallback(ProjectilePresenter projectile)
    {
        DamageSystem.ApplyDamage(projectile.Model.Target, projectile.Model);
        _projectileFactory.Dispose(projectile);
    }
}
