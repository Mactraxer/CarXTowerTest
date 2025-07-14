public class IdleState : ITowerState
{
    private readonly TowerModel _model;

    public IdleState(TowerModel model)
    {
        _model = model;
    }

    public void Enter()
    {
        _model.DetectedArea.OnEnemyEntered += OnTargetDetected;

        if (_model.DetectedArea.GetFirstEnemy() != null) {
            _model.StateMachine.ChangeState<SearchTargetState>();
        }
    }

    public void Exit()
    {
        _model.DetectedArea.OnEnemyEntered -= OnTargetDetected;
    }

    public void Tick() { }

    private void OnTargetDetected(ITarget _)
    {
        _model.StateMachine.ChangeState<SearchTargetState>();
    }
}