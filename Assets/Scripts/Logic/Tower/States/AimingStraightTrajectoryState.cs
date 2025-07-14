public class AimingStraightTrajectoryState : AimingBaseState
{
    public AimingStraightTrajectoryState(CannonTowerModel model) : base(model)
    {
    }

    protected override void Aim()
    {
        var aimVector = _model.TargetingSystem.CalculateStraightAimOffsetVector(_model.GunPosition, _model.Target.CurrentPosition, _model.Target.Velocity, _model.AimDuration, _model.ProjectileSpeed);
        _model.SetAimOffset(aimVector);
        _model.Aim();
    }

    protected override void OnAimingComplete()
    {
        _model.StateMachine.ChangeState<FiringStraightTrajectoryState>();
    }
}