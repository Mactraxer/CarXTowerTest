using UnityEngine;

public class AimingStraightTrajectoryState : AimingBaseState
{
    public AimingStraightTrajectoryState(CannonTowerModel model) : base(model)
    {
    }

    protected override void Aim()
    {
        Vector3 aimVector;

        if (_model.TargetingSystem.CalculateStraightAimDirectionVector(_model.ShootPointPosition, _model.Target.CurrentPosition, _model.Target.Velocity, _model.AimDuration, _model.ProjectileSpeed, out aimVector))
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
        _model.StateMachine.ChangeState<FiringStraightTrajectoryState>();
    }
}