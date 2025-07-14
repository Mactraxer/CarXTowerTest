using UnityEngine;

public class AimingState : ITowerState
{
    private readonly TowerModel _model;
    private float _aimDuration;

    public AimingState(TowerModel model)
    {
        _model = model;
    }

    public void Enter()
    {
        _model.DetectedArea.OnEnemyExited += OnEnemyExitedHandler;
        _aimDuration = _model.AimDuration;
        _model.Aim();
    }

    public void Tick()
    {
        _aimDuration -= Time.deltaTime;

        if (_aimDuration <= 0f)
            _model.StateMachine.ChangeState<FiringState>();
    }

    public void Exit()
    {
        _model.DetectedArea.OnEnemyExited -= OnEnemyExitedHandler;
        _model.OnStopAim();
    }
    
    private void OnEnemyExitedHandler(ITarget target)
    {
        if (_model.Target == target) {
            _model.DetectedArea.OnEnemyExited -= OnEnemyExitedHandler;
            _model.StateMachine.ChangeState<SearchTargetState>();
        }
    }
}
