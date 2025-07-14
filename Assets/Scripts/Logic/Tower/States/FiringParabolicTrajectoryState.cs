using UnityEngine;

public class FiringParabolicTrajectoryState : ITowerState
{
    private readonly CannonTowerModel _model;

    public FiringParabolicTrajectoryState(CannonTowerModel cannonTowerModel)
    {
        _model = cannonTowerModel;
    }

    public void Enter()
    {
        if (_model.TargetingSystem.CalculateParabolicFireVelocity(_model.ShootPointPosition, _model.Target.CurrentPosition, _model.Target.Velocity, _model.ProjectileSpeed, out Vector3 initialFireVelocity))
        {
            _model.SetFireVelocity(initialFireVelocity);
            _model.Shoot();
        }

        _model.StateMachine.ChangeState<CooldownState>();
    }

    public void Exit() { }

    public void Tick() { }
}
