using UnityEngine;

public abstract class AimingBaseState : ITowerState
{
    protected readonly CannonTowerModel _model;
    protected float _aimDuration;

    protected AimingBaseState(CannonTowerModel model)
    {
        _model = model;
    }

    public virtual void Enter()
    {
        _model.DetectedArea.OnEnemyExited += OnEnemyExitedHandler;
        _aimDuration = _model.AimDuration;
        Aim();
    }

    public virtual void Exit()
    {
        _model.DetectedArea.OnEnemyExited -= OnEnemyExitedHandler;
    }

    public virtual void Tick()
    {
        _aimDuration -= Time.deltaTime;
        if (_aimDuration <= 0f)
            OnAimingComplete();
    }

    protected abstract void Aim();
    protected abstract void OnAimingComplete();

    protected virtual void OnEnemyExitedHandler(ITarget target)
    {
        if (_model.Target == target)
        {
            _model.DetectedArea.OnEnemyExited -= OnEnemyExitedHandler;
            _model.StateMachine.ChangeState<SearchTargetState>();
        }
    }
}
