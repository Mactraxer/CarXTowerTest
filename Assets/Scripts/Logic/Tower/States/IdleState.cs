using System;

public class IdleState : ITowerState
{
    private readonly TowerModel _model;
    private readonly TowerStateMachine _stateMachine;

    public IdleState(TowerModel model, TowerStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _model = model;
    }

    public void Enter()
    {
        _model.DetectedArea.OnEnemyEntered += OnTargetDetected;

        if (_model.DetectedArea.GetFirstEnemy() != null) {
            _stateMachine.ChangeState<SearchTargetState>();
        }
    }

    public void Tick() { }

    public void Exit()
    {
        _model.DetectedArea.OnEnemyEntered -= OnTargetDetected;
    }

    private void OnTargetDetected(IEnemy enemy)
    {
        _stateMachine.ChangeState<SearchTargetState>();
    }
}
