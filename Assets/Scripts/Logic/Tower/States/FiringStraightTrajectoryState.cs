public class FiringStraightTrajectoryState : ITowerState
{
    private readonly CannonTowerModel _model;

    public FiringStraightTrajectoryState(CannonTowerModel model)
    {
        _model = model;
    }

    public void Enter()
    {
        var fireVelocity = _model.TargetingSystem.CalculateStraightFireVelocityVector(_model.ShootPointPosition, _model.Target.CurrentPosition, _model.Target.Velocity, _model.ProjectileSpeed);
        _model.SetFireVelocity(fireVelocity);
        _model.Shoot();
        _model.StateMachine.ChangeState<CooldownState>();
    }

    public void Exit() { }

    public void Tick() { }
}
