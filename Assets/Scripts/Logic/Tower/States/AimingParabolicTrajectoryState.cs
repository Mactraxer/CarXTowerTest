using UnityEngine;

public class AimingParabolicTrajectoryState : AimingBaseState
{
    public AimingParabolicTrajectoryState(CannonTowerModel model) : base(model)
    {
    }

    protected override void Aim()
    {
        if (_model.TargetingSystem.CalculateParabolicAimOffsetVector(_model.GunPosition, _model.Target.CurrentPosition, _model.Target.Velocity, _model.AimDuration, _model.ProjectileSpeed, out Vector3 aimVector))
        {
            _model.SetAimOffset(aimVector);
            _model.Aim();
        }
        else
        {
            _model.StateMachine.ChangeState<CooldownState>();
        }
    }

    protected override void OnAimingComplete()
    {
        _model.StateMachine.ChangeState<FiringParabolicTrajectoryState>();
    }
}
