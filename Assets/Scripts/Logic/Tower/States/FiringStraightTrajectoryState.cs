using UnityEngine;

public class FiringStraightTrajectoryState : ITowerState
{
    private readonly CannonTowerModel _model;

    public FiringStraightTrajectoryState(CannonTowerModel model)
    {
        _model = model;
    }

    public void Enter()
    {
        Vector3 fireDirection;

        if (_model.TargetingSystem.CalculateStraightFireDirectionVector(_model.ShootPointPosition, _model.Target.CurrentPosition, _model.Target.Velocity, _model.ProjectileSpeed, out fireDirection))
        {
            _model.SetFireVelocity(fireDirection);
            _model.Shoot();
        }

        _model.StateMachine.ChangeState<CooldownState>();
    }

    public void Exit() { }

    public void Tick() { }
}
